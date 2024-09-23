using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Portion_", menuName = "ItemData/Portion", order = 3)]
public class PortionItemData : CountItemData
{
    //���� ������ �����͸� ����
    public float Value => _value;
    //ȸ����(ȿ�� - ������)
    [SerializeField] float _value;
}