using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion_", menuName = "ItemData/Potion", order = 4)]
public class PotionItemData : ItemData
{
    //���� ������ �����͸� ����
    public float Value => _value;
    public float CoolTime => _coolTime;
    //ȸ����(ȿ�� - ������)
    [SerializeField] float _value;
    //������ ��Ÿ��
    [SerializeField] float _coolTime;
}