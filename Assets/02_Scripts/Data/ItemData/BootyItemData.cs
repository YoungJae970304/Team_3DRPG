using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Booty_", menuName = "ItemData/Booty", order = 5)]
public class BootyItemData : CountableItemData
{
    public string ToolTip => _toolTip;

    [Multiline]
    [SerializeField] string _toolTip;
}
