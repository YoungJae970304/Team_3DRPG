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


    //protected static Item ItemSpawn(int id)
    //{

    //}
    

    //여기서 해줄수 있을 기능은?
    //1. 새로운 아이템 생성
    //어떤 걸로든 아이템을 생성해주고 return 해줄 수 있는지
    // Item.ItemSpawn(id);

    //protected static Item ItemSpawn(int id)
    //{
    //    //
    //}
}
