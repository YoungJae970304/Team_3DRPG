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
        Logger.Log($"{_amount} ����üũ");
    }

    //�ִ� ���� ����
    public int SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount, 0, _maxAmount);

        return _amount;
    }

    //���� ���� ����� �˷��� �Լ�
    public int GetCurrentAmount()
    {
        return _amount;
    }

    //���� ����� Ȯ�� ���ִ� �Լ�
    public int AddAmount(int amount)
    {
        int nextAmount = _amount + amount;
        //���� ������ �߰��� ������ _maxAmount�� �ʰ� �ߴ��� Ȯ�� �� ����
        int over = 0;
        //�߰��� ������ �ִ밳��(99) ��Ÿ Ŀ����
        if(nextAmount > _maxAmount)
        {
            over = nextAmount - _maxAmount;
            _amount = _maxAmount;
        }else
        {
            _amount = nextAmount;
        }
        return over;
    }

    //���ο� ������ ����
    protected abstract CountableItem Clone(int amount);
}
