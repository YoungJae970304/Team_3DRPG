using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemData Data { get; private set; }


    public Item(ItemData data)
    {
        Data = data;
    }

    /*
    protected static Item ItemSpawn(int id)
    {

    }
    */

    //여기서 해줄수 있을 기능은?
    //1. 새로운 아이템 생성
    //2. 이 아이템이 획득한 아이템이냐 장착한 아이템이냐
    //3. 장착한 아이템이 어떤 아이템 타입이고 그 타입의 id는 무엇이냐
    // Item.ItemSpawn(id);

    //protected static Item ItemSpawn(int id)
    //{
    //    //
    //}

    //protected abstract Item Clone(int amount);
}
