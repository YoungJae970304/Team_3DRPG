using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : ItemData
{
    public int LimitLevel { get { return _limitLevel; } set { _limitLevel = value; } }

    //착용 가능 레벨
    int _limitLevel;
}
