using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equipment_", menuName = "ItemData/Equipment")]
public class EquipmentItemData : ItemData
{
    public int Attack => _attack;
    public int Defence => _defence;
    public int Health => _health;
    public int HealthRegen => _healthRegen;
    public int Mana => _mana;
    public int ManaRegen => _manaRegen;
    public int Level => _level;
    public int Grade => _grade;
    //공격력
    [SerializeField] int _attack;
    //방어력
    [SerializeField] int _defence;
    //체력
    [SerializeField] int _health;
    //체력 리젠
    [SerializeField] int _healthRegen;
    //마나
    [SerializeField] int _mana;
    //마나 리젠
    [SerializeField] int _manaRegen;
    //착용 가능 레벨
    [SerializeField] int _level;
    //등급
    [SerializeField] int _grade;
}
