using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItemData : ItemData
{
    public int Level => _level;
    public int Grade => _grade;
    
    //���� ���� ����
    [SerializeField] int _level;
    //���
    [SerializeField] int _grade;
}
