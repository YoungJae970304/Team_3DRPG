using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountItemData : ItemData
{
    //������ �����ϴ� ������ ������
    //�⺻���� ItemData ���
    public int MaxAmount => _maxAmount;
    public float CoolTime => _coolTime;
    //�ִ� ���� ����
    [SerializeField] int _maxAmount = 99;
    //������ ��Ÿ��
    [SerializeField] float _coolTime;
}
