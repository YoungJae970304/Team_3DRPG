using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolTipUI :MonoBehaviour
{
    public TextMeshProUGUI _toolTiptext;
    public Image _icon;
    public RectTransform _rectTransform;


    public void Awake()
    {
       TryGetComponent<RectTransform>(out _rectTransform);
       _rectTransform.pivot = new Vector2(0f, 1f); // Left Top
    }
    private void Update()
    {
        _rectTransform.position = Input.mousePosition;
    }

    public void SetInfo(ItemSlot data) {
        _icon.sprite = data._Image.sprite;
        _toolTiptext.text = $"Name:{data.Item.Data.Name}";


    }


}
