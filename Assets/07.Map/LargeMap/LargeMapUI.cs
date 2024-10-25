using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LargeMapUI : BaseUI
{
    enum RawImages
    {
        MapDisplay
    }

    enum Images
    {
        Mask
    }

    private Camera _largeMapCamera;
    private RawImage _mapDisplay;
    private Material _fogMaterial;
    private static Texture2D _fogTexture;
    public float _worldSize = 300;
    public int _textureSize = 1024;
    public float _exploredRadius = 15f;

    // 지도 줌아웃을 위한 변수
    public float _minZoom = 1f;
    public float _maxZoom = 20f;
    public float _zoomSpeed = 0.5f;
    private float _currentZoom = 1f;
    [SerializeField] private RectTransform _maskRectTransform;

    // 지도 이동을 위한 변수
    private bool _isDragging = false;
    private Vector2 _lastMousePosition;

    private void Awake()
    {
        _currentZoom = 1f;
    }

    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;
    }

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        Managers.Game._cantInputKey = true;

        Bind<RawImage>(typeof(RawImages));
        _mapDisplay = GetRawImage((int)RawImages.MapDisplay);
        _mapDisplay.rectTransform.localScale = Vector3.one * _currentZoom;

        // 카메라가 할당되지 않았다면 찾기
        if (_largeMapCamera == null)
        {
            _largeMapCamera = GameObject.Find("LargeMapCamera")?.GetComponent<Camera>();
            if (_largeMapCamera == null)
                Logger.LogError("LargeMapCamera not found!");
        }

        InitializeFogTexture();
        SetupMaterial();
        CenterMapOnPlayer();

        Managers.Input.KeyAction -= UpdateMap;
        Managers.Input.KeyAction += UpdateMap;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            HandleMapDrag();
            HandleMapZoom();
        }
    }

    private void HandleMapDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            Vector2 deltaMouse = (Vector2)Input.mousePosition - _lastMousePosition;
            Vector2 newPosition = _mapDisplay.rectTransform.anchoredPosition + deltaMouse;

            // 새로운 위치를 마스크 영역 내로 제한
            newPosition = ClampPositionToMask(newPosition);

            _mapDisplay.rectTransform.anchoredPosition = newPosition;
            _lastMousePosition = Input.mousePosition;
        }
    }

    private void HandleMapZoom()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            ZoomMap(scrollDelta);
        }
    }

    private void ZoomMap(float zoomDelta)
    {
        float previousZoom = _currentZoom;
        _currentZoom += zoomDelta * _zoomSpeed;
        _currentZoom = Mathf.Clamp(_currentZoom, _minZoom, _maxZoom);

        Vector3 newScale = Vector3.one * _currentZoom;
        _mapDisplay.rectTransform.localScale = newScale;

        // 줌 중심점 조정
        Vector2 mousePositionOnMap = Input.mousePosition - _mapDisplay.rectTransform.position;
        Vector2 newPosition = _mapDisplay.rectTransform.anchoredPosition - (mousePositionOnMap * (_currentZoom / previousZoom - 1));

        // 새로운 위치를 마스크 영역 내로 제한
        newPosition = ClampPositionToMask(newPosition);

        _mapDisplay.rectTransform.anchoredPosition = newPosition;
    }

    private Vector2 ClampPositionToMask(Vector2 position)
    {
        if (_maskRectTransform == null) return position;

        Rect maskRect = _maskRectTransform.rect;
        Rect mapRect = _mapDisplay.rectTransform.rect;

        float halfWidthDiff = (mapRect.width * _currentZoom - maskRect.width) * 0.5f;
        float halfHeightDiff = (mapRect.height * _currentZoom - maskRect.height) * 0.5f;

        position.x = Mathf.Clamp(position.x, -halfWidthDiff, halfWidthDiff);
        position.y = Mathf.Clamp(position.y, -halfHeightDiff, halfHeightDiff);

        return position;
    }

    private void SetupMaterial()
    {
        _fogMaterial = new Material(Shader.Find("Custom/FogOfWar"));
        if (_fogMaterial != null)
        {
            _fogMaterial.SetTexture("_FogTex", _fogTexture);

            // UI 마스킹을 위한 프로퍼티 설정
            _fogMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Always);
            _fogMaterial.SetInt("_Stencil", 0);
            _fogMaterial.SetInt("_StencilOp", (int)UnityEngine.Rendering.StencilOp.Keep);
            _fogMaterial.SetInt("_StencilWriteMask", 255);
            _fogMaterial.SetInt("_StencilReadMask", 255);
            _fogMaterial.SetInt("_ColorMask", 15);

            _mapDisplay.material = _fogMaterial;
        }
        else
        {
            Logger.LogError("Failed to find or create Custom/FogOfWar shader.");
        }
    }

    private void InitializeFogTexture()
    {
        if (_fogTexture == null)
        {
            _fogTexture = new Texture2D(_textureSize, _textureSize);
            _fogTexture.filterMode = FilterMode.Bilinear;
            Color[] colors = new Color[_textureSize * _textureSize];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            _fogTexture.SetPixels(colors);
            _fogTexture.Apply();
        }
    }

    public void UpdateMap()
    {
        //if (!gameObject.activeSelf || largeMapCamera == null) return;

        Transform playerTransform = Managers.Game._player._playerModel.transform;

        // 플레이어 월드 위치를 카메라의 뷰포트 좌표로 변환
        Vector3 viewportPoint = _largeMapCamera.WorldToViewportPoint(playerTransform.position);

        // 뷰포트 좌표가 0~1 범위 내에 있는지 확인
        if (viewportPoint.z > 0f && viewportPoint.x >= 0f && viewportPoint.x <= 1f &&
            viewportPoint.y >= 0f && viewportPoint.y <= 1f)
        {
            // 뷰포트 좌표를 텍스처 좌표로 변환
            Vector2 texturePoint = new Vector2(
                viewportPoint.x * _textureSize,
                viewportPoint.y * _textureSize
            );

            RevealArea(texturePoint, _exploredRadius);
        }
    }

    private void RevealArea(Vector2 texturePoint, float radius)
    {
        int texRadius = Mathf.RoundToInt(radius);

        for (int y = -texRadius; y <= texRadius; y++)
        {
            for (int x = -texRadius; x <= texRadius; x++)
            {
                float distance = Mathf.Sqrt(x * x + y * y);
                if (distance <= texRadius)
                {
                    int pixelX = Mathf.Clamp(Mathf.RoundToInt(texturePoint.x + x), 0, _textureSize - 1);
                    int pixelY = Mathf.Clamp(Mathf.RoundToInt(texturePoint.y + y), 0, _textureSize - 1);

                    float alpha = Mathf.Clamp01(1 - (distance / texRadius));
                    Color currentColor = _fogTexture.GetPixel(pixelX, pixelY);
                    Color newColor = Color.Lerp(currentColor, Color.white, alpha);
                    _fogTexture.SetPixel(pixelX, pixelY, newColor);
                }
            }
        }
        _fogTexture.Apply();
    }

    private void CenterMapOnPlayer()
    {
        if (_largeMapCamera == null || _mapDisplay == null) return;

        Transform playerTransform = Managers.Game._player._playerModel.transform;
        Vector3 viewportPoint = _largeMapCamera.WorldToViewportPoint(playerTransform.position);

        // 뷰포트 좌표를 UI 좌표로 변환
        Rect maskRect = _maskRectTransform.rect;
        Vector2 targetPosition = new Vector2(
            (0.5f - viewportPoint.x) * maskRect.width,
            (0.5f - viewportPoint.y) * maskRect.height
        );

        // 줌 레벨을 고려하여 위치 조정
        targetPosition *= _currentZoom;

        // 맵 위치를 제한된 범위 내로 조정
        targetPosition = ClampPositionToMask(targetPosition);

        // 맵 이동
        _mapDisplay.rectTransform.anchoredPosition = targetPosition;
    }
}