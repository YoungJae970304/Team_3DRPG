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

    private RawImage mapDisplay;
    private Material fogMaterial;
    private static Texture2D fogTexture;
    public int textureSize = 512;
    public float worldSize = 100;
    public float exploredRadius = 5f;

    // 지도 줌아웃을 위한 변수
    public float minZoom = 1f;
    public float maxZoom = 100f;
    public float zoomSpeed = 0.5f;
    private float currentZoom = 1f;
    [SerializeField] private RectTransform maskRectTransform; // Inspector에서 할당해주세요

    // 지도 이동을 위한 변수
    private bool isDragging = false;
    private Vector2 lastMousePosition;

    private void OnEnable()
    {
        Managers.Game._cantInputKey = true;
    }

    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;
    }

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        Bind<RawImage>(typeof(RawImages));

        mapDisplay = GetRawImage((int)RawImages.MapDisplay); //Get<RawImage>((int)RawImages.MapDisplay);

        // 초기 줌 설정
        currentZoom = 7.5f;

        InitializeFogTexture();
        SetupMaterial();

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

    private void InitializeFogTexture()
    {
        if (fogTexture == null)
        {
            fogTexture = new Texture2D(textureSize, textureSize);
            Color[] colors = new Color[textureSize * textureSize];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.black;
            }
            fogTexture.SetPixels(colors);
            fogTexture.Apply();
        }
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
            Debug.Log("Fog of War shader applied successfully with UI masking properties.");
        }
        else
        {
            Debug.LogError("Failed to find or create Custom/FogOfWar shader.");
        }
    }

    public void RevealArea(Vector2 worldPosition, float radius)
    {
        Vector2 texPosition = WorldToTexturePosition(worldPosition);
        int texRadius = Mathf.RoundToInt(radius * textureSize / worldSize);

        for (int y = -texRadius; y <= texRadius; y++)
        {
            for (int x = -texRadius; x <= texRadius; x++)
            {
                float distance = Mathf.Sqrt(x * x + y * y);
                if (distance <= texRadius)
                {
                    int pixelX = Mathf.RoundToInt(texPosition.x + x);
                    int pixelY = Mathf.RoundToInt(texPosition.y + y);
                    if (pixelX >= 0 && pixelX < textureSize && pixelY >= 0 && pixelY < textureSize)
                    {
                        float alpha = Mathf.Clamp01(1 - (distance / texRadius));
                        Color currentColor = fogTexture.GetPixel(pixelX, pixelY);
                        Color newColor = Color.Lerp(currentColor, Color.white, alpha);
                        fogTexture.SetPixel(pixelX, pixelY, newColor);
                    }
                }
            }
        }
        fogTexture.Apply();
    }

    private Vector2 WorldToTexturePosition(Vector2 worldPos)
    {
        return new Vector2(
            (worldPos.x + worldSize / 2) / worldSize * textureSize,
            (worldPos.y + worldSize / 2) / worldSize * textureSize
        );
    }

    public void UpdateMap()
    {
        Vector3 playerPos = Managers.Game._player._playerModel.transform.position;
        RevealArea(new Vector2(playerPos.x, playerPos.z), exploredRadius);
    }
}