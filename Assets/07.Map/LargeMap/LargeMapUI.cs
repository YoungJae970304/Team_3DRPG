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

    [SerializeField] private Camera largeMapCamera;  // Inspector에서 LargeMapCamera 할당
    private RawImage mapDisplay;
    private Material fogMaterial;
    private static Texture2D fogTexture;
    public float worldSize = 300;
    public int textureSize = 1024;
    public float exploredRadius = 15f;

    // 지도 줌아웃을 위한 변수
    public float minZoom = 1f;
    public float maxZoom = 20f;
    public float zoomSpeed = 0.5f;
    private float currentZoom = 1f;
    [SerializeField] private RectTransform maskRectTransform;

    // 지도 이동을 위한 변수
    private bool isDragging = false;
    private Vector2 lastMousePosition;

    private void OnEnable()
    {
        Managers.Game._cantInputKey = true;

        mapDisplay.rectTransform.localScale = Vector3.one * currentZoom;
        CenterMapOnPlayer();
    }

    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;
    }

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        Bind<RawImage>(typeof(RawImages));
        mapDisplay = GetRawImage((int)RawImages.MapDisplay);

        // 카메라가 할당되지 않았다면 찾기
        if (largeMapCamera == null)
        {
            largeMapCamera = GameObject.Find("LargeMapCamera")?.GetComponent<Camera>();
            if (largeMapCamera == null)
                Logger.LogError("LargeMapCamera not found!");
        }

        InitializeFogTexture();
        SetupMaterial();

        currentZoom = 3f;

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
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 deltaMouse = (Vector2)Input.mousePosition - lastMousePosition;
            Vector2 newPosition = mapDisplay.rectTransform.anchoredPosition + deltaMouse;

            // 새로운 위치를 마스크 영역 내로 제한
            newPosition = ClampPositionToMask(newPosition);

            mapDisplay.rectTransform.anchoredPosition = newPosition;
            lastMousePosition = Input.mousePosition;
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
        float previousZoom = currentZoom;
        currentZoom += zoomDelta * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 newScale = Vector3.one * currentZoom;
        mapDisplay.rectTransform.localScale = newScale;

        // 줌 중심점 조정
        Vector2 mousePositionOnMap = Input.mousePosition - mapDisplay.rectTransform.position;
        Vector2 newPosition = mapDisplay.rectTransform.anchoredPosition - (mousePositionOnMap * (currentZoom / previousZoom - 1));

        // 새로운 위치를 마스크 영역 내로 제한
        newPosition = ClampPositionToMask(newPosition);

        mapDisplay.rectTransform.anchoredPosition = newPosition;
    }

    private Vector2 ClampPositionToMask(Vector2 position)
    {
        if (maskRectTransform == null) return position;

        Rect maskRect = maskRectTransform.rect;
        Rect mapRect = mapDisplay.rectTransform.rect;

        float halfWidthDiff = (mapRect.width * currentZoom - maskRect.width) * 0.5f;
        float halfHeightDiff = (mapRect.height * currentZoom - maskRect.height) * 0.5f;

        position.x = Mathf.Clamp(position.x, -halfWidthDiff, halfWidthDiff);
        position.y = Mathf.Clamp(position.y, -halfHeightDiff, halfHeightDiff);

        return position;
    }

    private void SetupMaterial()
    {
        fogMaterial = new Material(Shader.Find("Custom/FogOfWar"));
        if (fogMaterial != null)
        {
            fogMaterial.SetTexture("_FogTex", fogTexture);

            // UI 마스킹을 위한 프로퍼티 설정
            fogMaterial.SetInt("_StencilComp", (int)UnityEngine.Rendering.CompareFunction.Always);
            fogMaterial.SetInt("_Stencil", 0);
            fogMaterial.SetInt("_StencilOp", (int)UnityEngine.Rendering.StencilOp.Keep);
            fogMaterial.SetInt("_StencilWriteMask", 255);
            fogMaterial.SetInt("_StencilReadMask", 255);
            fogMaterial.SetInt("_ColorMask", 15);

            mapDisplay.material = fogMaterial;
        }
        else
        {
            Logger.LogError("Failed to find or create Custom/FogOfWar shader.");
        }
    }

    private void InitializeFogTexture()
    {
        if (fogTexture == null)
        {
            fogTexture = new Texture2D(textureSize, textureSize);
            fogTexture.filterMode = FilterMode.Bilinear;
            Color[] colors = new Color[textureSize * textureSize];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            fogTexture.SetPixels(colors);
            fogTexture.Apply();
        }
    }

    public void UpdateMap()
    {
        //if (!gameObject.activeSelf || largeMapCamera == null) return;

        Transform playerTransform = Managers.Game._player._playerModel.transform;

        // 플레이어 월드 위치를 카메라의 뷰포트 좌표로 변환
        Vector3 viewportPoint = largeMapCamera.WorldToViewportPoint(playerTransform.position);

        // 뷰포트 좌표가 0~1 범위 내에 있는지 확인
        if (viewportPoint.z > 0f && viewportPoint.x >= 0f && viewportPoint.x <= 1f &&
            viewportPoint.y >= 0f && viewportPoint.y <= 1f)
        {
            // 뷰포트 좌표를 텍스처 좌표로 변환
            Vector2 texturePoint = new Vector2(
                viewportPoint.x * textureSize,
                viewportPoint.y * textureSize
            );

            RevealArea(texturePoint, exploredRadius);
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
                    int pixelX = Mathf.Clamp(Mathf.RoundToInt(texturePoint.x + x), 0, textureSize - 1);
                    int pixelY = Mathf.Clamp(Mathf.RoundToInt(texturePoint.y + y), 0, textureSize - 1);

                    float alpha = Mathf.Clamp01(1 - (distance / texRadius));
                    Color currentColor = fogTexture.GetPixel(pixelX, pixelY);
                    Color newColor = Color.Lerp(currentColor, Color.white, alpha);
                    fogTexture.SetPixel(pixelX, pixelY, newColor);
                }
            }
        }
        fogTexture.Apply();
    }

    private void CenterMapOnPlayer()
    {
        if (largeMapCamera == null || mapDisplay == null) return;

        Transform playerTransform = Managers.Game._player._playerModel.transform;
        Vector3 viewportPoint = largeMapCamera.WorldToViewportPoint(playerTransform.position);

        // 뷰포트 좌표를 UI 좌표로 변환
        Rect maskRect = maskRectTransform.rect;
        Vector2 targetPosition = new Vector2(
            (0.5f - viewportPoint.x) * maskRect.width,
            (0.5f - viewportPoint.y) * maskRect.height
        );

        // 줌 레벨을 고려하여 위치 조정
        targetPosition *= currentZoom;

        // 맵 위치를 제한된 범위 내로 조정
        targetPosition = ClampPositionToMask(targetPosition);

        // 맵 이동
        mapDisplay.rectTransform.anchoredPosition = targetPosition;
    }
}