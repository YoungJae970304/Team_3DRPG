using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Armor_", menuName = "ItemData/Armor", order = 2)]
public class ArmorItemData : EquipmentItemData
{
    public int Defence => _defence;
    public int Health => _health;

    //방어력
    [SerializeField] int _defence;
    //체력
    [SerializeField] int _health;
}