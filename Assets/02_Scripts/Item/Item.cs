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


    public static Item ItemSpawn(int id)
    {
        //데이터테이블매니저 인스턴스
        DataTableManager _dataTableManager = new DataTableManager();
        //모든 아이템 데이터 로드
        _dataTableManager.LoadAllItemData();
        
        ItemData itemData = null;
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var equippedItem in _dataTableManager.ItemEquippedDataTable)
        {
            if(equippedItem.ID == id)
            {
                itemData = equippedItem;
                break;
            }
        }
        if (itemData == null)
        {
            foreach (var potionItem in _dataTableManager.ItemPotionDataTable)
            {
                if (potionItem.ID == id)
                {
                    itemData = potionItem;
                    break;
                }
            }
        }
        if(itemData == null)
        {
            foreach (var goodsItem in _dataTableManager.ItemGoodsDataTable)
            {
                if(goodsItem.ID == id)
                {
                    itemData = goodsItem;
                    break;
                }
            }
        }
        if (itemData != null)
        {
            return new Item(itemData);
        }
        else
        {
            Logger.Log("해당 Id의 아이템을 찾을수가 없슈 : " + id);
            return null;
        }
    }
}
