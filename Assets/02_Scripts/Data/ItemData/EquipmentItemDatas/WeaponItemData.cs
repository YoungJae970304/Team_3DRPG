using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemData : EquipmentItemData
{
    public int AttackPower { get { return _attackPower; } set { _attackPower = value; } }

    //공격력
     int _attackPower;
}
