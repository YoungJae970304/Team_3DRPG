using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : GoldItemData
{
    public int _gold { get; protected set; }

    //현재 골드를 지정해서 넘겨줌 골드는
    public void SetGold(int goldReward)
    {
        _gold = goldReward;
    }
}
