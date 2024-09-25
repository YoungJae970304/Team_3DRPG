using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Accessories_", menuName = "ItemData/Accessories", order = 3)]
public class AccessoriesItemData : EquipmentItemData
{
    public int HealthRegen => _healthRegen;
    public int Mana => _mana;
    public int ManaRegen => _manaRegen;

    //Ã¼·Â ¸®Á¨(¾Ç¼¼¼­¸®)
    [SerializeField] int _healthRegen;
    //¸¶³ª(¾Ç¼¼¼­¸®)
    [SerializeField] int _mana;
    //¸¶³ª ¸®Á¨(¾Ç¼¼¼­¸®)
    [SerializeField] int _manaRegen;
}
