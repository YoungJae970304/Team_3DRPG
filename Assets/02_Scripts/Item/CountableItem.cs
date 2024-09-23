using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountableItem : Item
{
    //������ �ִ� �����ۿ� ����
    //���� �ִ� ������ ������ ������Ƽ
    public CountItemData _countItemData { get; private set; }

    //���� ������ ����
    public int _amount { get; protected set; }
    //�ִ�� ������ �� �ִ� ����
    public int _maxAmount => _countItemData.MaxAmount;

    //������ ���� á���� ����
    public bool _isMax => _amount >= _countItemData.MaxAmount;
    //������ ������ ����
    public bool _isEmpty => _amount <= 0;

    public CountableItem(CountItemData data, int amount = 1) : base(data)
    {
        _countItemData = data;
        SetAmount(amount);
    }

    //�ִ� ���� ����
    public void SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);
    }
}
