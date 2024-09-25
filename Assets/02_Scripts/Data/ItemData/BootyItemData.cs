using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Booty_", menuName = "ItemData/Booty", order = 5)]
public class BootyItemData : ItemData
{
    public string ToolTip => _toolTip;
    //기타 아이템 설명
    [Multiline]
    [SerializeField] string _toolTip;
}
