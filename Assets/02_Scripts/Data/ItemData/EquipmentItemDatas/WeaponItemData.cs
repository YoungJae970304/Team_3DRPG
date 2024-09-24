using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Weapon_", menuName = "ItemData/Weapon", order = 1)]
public class WeaponItemData : EquipmentItemData
{
    public int Attack => _attack;
    //���ݷ�
    [SerializeField] int _attack;
}
