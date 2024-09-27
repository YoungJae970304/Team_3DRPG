using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemData : EquipmentItemData
{
    public int Attack => _attack;
    //공격력
    [SerializeField] int _attack;
}
