using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItemData : ItemData
{
    //������ �����ϴ� ������ ������
    //�⺻���� ItemData ���
    public int MaxAmount => _maxAmount;
    
    //�ִ� ���� ����
    [SerializeField] int _maxAmount = 99;
    
}
