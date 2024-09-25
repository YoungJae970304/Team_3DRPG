using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : ItemData
{
    public int Level => _level;
    public int Grade => _grade;
    
    //착용 가능 레벨
    [SerializeField] int _level;
    //등급
    [SerializeField] int _grade;
}
