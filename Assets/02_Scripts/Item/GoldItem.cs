using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoldItem : GoldItemData
{
    public int _gold { get; protected set; }

    //���� ��带 �����ؼ� �Ѱ��� ����
    public void SetGold(int goldAmount)
    {
        _gold = goldAmount;
    }
}
