using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Accessories_", menuName = "ItemData/Accessories", order = 3)]
public class AccessoriesItemData : EquipmentItemData
{
    public int HealthRegen => _healthRegen;
    public int Mana => _mana;
    public int ManaRegen => _manaRegen;

    //ü�� ����(�Ǽ�����)
    [SerializeField] int _healthRegen;
    //����(�Ǽ�����)
    [SerializeField] int _mana;
    //���� ����(�Ǽ�����)
    [SerializeField] int _manaRegen;
}
