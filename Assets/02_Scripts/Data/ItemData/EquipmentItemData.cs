using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Equipment_", menuName = "ItemData/Equipment", order = 1)]
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
    //���ݷ�(����)
    [SerializeField] int _attack;
    //����(��)
    [SerializeField] int _defence;
    //ü��(��)
    [SerializeField] int _health;
    //ü�� ����(�Ǽ�����)
    [SerializeField] int _healthRegen;
    //����(�Ǽ�����)
    [SerializeField] int _mana;
    //���� ����(�Ǽ�����)
    [SerializeField] int _manaRegen;
    //���� ���� ����(����)
    [SerializeField] int _level;
    //���(����)
    [SerializeField] int _grade;
}
