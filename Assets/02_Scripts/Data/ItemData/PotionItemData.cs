using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion_", menuName = "ItemData/Potion", order = 2)]
public class PotionItemData : CountItemData
{
    //포션 아이템 데이터를 생성
    public float Value => _value;
    //회복량(효과 - 버프등)
    [SerializeField] float _value;
}