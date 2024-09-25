using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : Item
{
    public ItemData _itemData {  get; set; }

    public EquipmentItemData _equipmentItemData {  get; set; }

    public int _amount {  get; protected set; }
    //��� �������� ����� �Լ�


    //��� �������� 1���� �ִ�ġ�� ���� ���� �Լ�
    public EquipmentItem(EquipmentItemData data, int amount = 1 ) : base(data)
    {
        _itemData = data;
        SetAmount(amount);
    }

    //���� 1���� �ִ� ������ ��� ����
    public int SetAmount(int amount)
    {
        _amount = _itemData.MaxAmount;

        amount = Mathf.Clamp(amount,0, _amount);

        return amount;
    }
}
