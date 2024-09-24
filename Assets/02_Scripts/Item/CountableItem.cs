using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableItem : Item
{
    //������ �ִ� �����ۿ� ����
    //���� �ִ� ������ ������ ������Ƽ
    public ItemData _itemData { get; private set; }

    //���� ������ ����
    public int _amount { get; protected set; }
    //�ִ�� ������ �� �ִ� ����
    public int _maxAmount => _itemData.MaxAmount;

    //������ ���� á���� ����
    public bool _isMax => _amount >= _itemData.MaxAmount;
    //������ ������ ����
    public bool _isEmpty => _amount <= 0;

    //������ �ִ� �������� 1������ �����ϴ� �Լ�
    public CountableItem(ItemData data, int amount = 1) : base(data)
    {
        _itemData = data;
        SetAmount(amount);
    }
  
    //�ִ� ���� ����
    public int SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);

        return _amount;
    }

    //99���� �Ѿ�� �� (�Լ� ȣ�� �ϰ�)
    //�κ��丮 �ȿ� �ִ� ���� ������ ����(idx?) ���� ������ ����(idx?)���� ���� �ٽ� CountableItem�Լ� ȣ��

    //������ 0 �� �� (�Լ� ȣ��) �κ��丮���� ���ְ� �κ��丮 ����

}
