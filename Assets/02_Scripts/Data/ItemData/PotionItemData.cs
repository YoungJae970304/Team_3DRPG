using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Potion_", menuName = "ItemData/Potion", order = 2)]
public class PotionItemData : CountItemData
{
    //���� ������ �����͸� ����
    public float Value => _value;
    //ȸ����(ȿ�� - ������)
    [SerializeField] float _value;
}