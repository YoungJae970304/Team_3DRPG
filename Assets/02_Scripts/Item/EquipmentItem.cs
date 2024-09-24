using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentItem : Item
{
    public ItemData _itemData {  get; set; }

    public EquipmentItemData _equipmentItemData {  get; set; }

    public int _amount {  get; protected set; }
    //장비 아이템이 사용할 함수


    //장비 아이템은 1개가 최대치로 설정 해줄 함수
    public EquipmentItem(EquipmentItemData data, int amount = 1 ) : base(data)
    {
        _itemData = data;
        SetAmount(amount);
    }

    //개수 1개를 최대 개수로 장비에 지정
    public int SetAmount(int amount)
    {
        _amount = Mathf.Clamp(amount,0, _itemData.MaxAmount);

        return _amount;
    }
}
