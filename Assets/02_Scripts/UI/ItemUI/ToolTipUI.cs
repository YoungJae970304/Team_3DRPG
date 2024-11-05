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
        string text;
        text = $"Name:{data.Item.Data.Name}\n";
        switch (data.Item.Data.Type) {//아이템 타입에 따라 정보 다르게 표시
            case ItemData.ItemType.Weapon:
            case ItemData.ItemType.Armor:
            case ItemData.ItemType.Accessories://장비 아이템의 경우 존재하는 스탯은 출력하고 0이면 무시
                EquipmentItemData equipmentData = data.Item.Data as EquipmentItemData;
                text += equipmentData.Grade != 0 ? $"등급:{equipmentData.Grade}성\n" : "";
                text += equipmentData.LimitLevel != 0 ? $"레벨 제한:{equipmentData.LimitLevel}\n" : "-";
                text += equipmentData.AttackPower!=0 ? $"Atk:{equipmentData.AttackPower}\n": "";
                text += equipmentData.Health != 0 ? $"Health:{equipmentData.Health}\n" : "";
                text += equipmentData.Mana != 0 ? $"Mana:{equipmentData.Mana}\n" : "";
                text += equipmentData.ManaRegen != 0 ? $"MPRegen:{equipmentData.ManaRegen}\n" : "";
                text += equipmentData.Defense != 0 ? $"Defense:{equipmentData.Defense}\n" : "";
                text += equipmentData.HealthRegen != 0 ? $"HPRegen:{equipmentData.HealthRegen}\n" : "";
                break;
            case ItemData.ItemType.Potion://소모품은 종류에 따라 다른 설명을 표시
                PotionItemData potionData = data.Item.Data as PotionItemData;
                switch (potionData.ValType) {
                    case PotionItemData.ValueType.Recovery:
                        text += $"회복량:{potionData.Value}%\n";
                        break;
                    case PotionItemData.ValueType.Atk:
                        text += $"공격력 증가:{potionData.Value}%\n";
                        text += $"지속시간:{potionData.DurationTime}%\n";
                        break;
                    case PotionItemData.ValueType.Def:
                        text += $"방어력 증가:{potionData.Value}%\n";
                        text += $"지속시간:{potionData.DurationTime}%\n";
                        break;
                }
                break;
            case ItemData.ItemType.Booty://기타 아이템은 플레이버 텍스트를 출력한다.
                GoodsItemData goodsData = data.Item.Data as GoodsItemData;
                text += $"{goodsData.FlavorText}%\n";
                break;
        
        
        }

        _toolTiptext.text = text;
    }


}
