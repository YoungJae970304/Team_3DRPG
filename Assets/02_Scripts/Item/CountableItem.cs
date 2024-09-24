using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    //������ �ִ� �����ۿ� ����
    //���� �ִ� ������ ������ ������Ƽ
    public CountableItemData _countItemData { get; private set; }

    //���� ������ ����
    public int _amount { get; protected set; }
    //�ִ�� ������ �� �ִ� ����
    public int _maxAmount => _countItemData.MaxAmount;

    //������ ���� á���� ����
    public bool _isMax => _amount >= _countItemData.MaxAmount;
    //������ ������ ����
    public bool _isEmpty => _amount <= 0;

    public CountableItem(CountableItemData data, int amount = 1) : base(data)
    {
        _countItemData = data;
        SetAmount(amount);
    }

    //�ִ� ���� ����
    public void SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);
    }

    //99���� �Ѿ�� �� (�Լ� ȣ�� �ϰ�)
    //�κ��丮 �ȿ� �ִ� ���� ������ ����(idx?) ���� ������ ����(idx?)���� ���� �ٽ� CountableItem�Լ� ȣ��

    //������ 0 �� �� (�Լ� ȣ��) �κ��丮���� ���ְ� �κ��丮 ����

}
