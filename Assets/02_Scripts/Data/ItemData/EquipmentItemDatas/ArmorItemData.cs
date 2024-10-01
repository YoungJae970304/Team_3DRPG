using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItemData : EquipmentItemData
{
    public int Defense { get { return _defense; } set { _defense = value; } }
    public int Health { get { return _health; } set { _health = value; } }

    //방어력
     int _defense;
    //체력
     int _health;
}