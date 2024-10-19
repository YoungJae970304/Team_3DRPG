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
            float scrollDelta = Input.mouseScrollDelta.y;
            if (scrollDelta != 0)
            {
                ZoomMap(scrollDelta);
            }
        }
    }

    private void ZoomMap(float zoomDelta)
    {
        currentZoom += zoomDelta * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        Vector3 newScale = Vector3.one * currentZoom;
        mapDisplay.rectTransform.localScale = newScale;

        // 맵 중앙 유지
        mapDisplay.rectTransform.anchoredPosition = Vector2.zero;
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
        fogMaterial.SetTexture("_FogTex", fogTexture);
        mapDisplay.material = fogMaterial;
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