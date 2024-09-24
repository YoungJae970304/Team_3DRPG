using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Armor_", menuName = "ItemData/Armor", order = 2)]
public class ArmorItemData : EquipmentItemData
{
    public int Defence => _defence;
    public int Health => _health;

    //����
    [SerializeField] int _defence;
    //ü��
    [SerializeField] int _health;
}