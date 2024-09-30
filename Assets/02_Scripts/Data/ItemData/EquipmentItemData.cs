using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : ItemData
{
    public int LimitLevel { get { return _limitLevel; } set { _limitLevel = value; } }

    public int Grade { get { return _grade; } set { _grade = value; } }

    //착용 가능 레벨
     int _limitLevel;
    //등급
     int _grade;
}
