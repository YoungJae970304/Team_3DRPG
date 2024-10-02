using System.Collections.Generic;

public class Item
{
    public ItemData Data { get; private set; }

    public Item(ItemData data)
    {
        Data = data;
    }

    #region ID로
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
            if (equippedItem.ID == id)
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
        if (itemData == null)
        {
            foreach (var goodsItem in _dataTableManager.ItemGoodsDataTable)
            {
                if (goodsItem.ID == id)
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
    #endregion

    #region type으로?
    //public static ItemSpawn(ItemData.ItemType itemType)
    //{
    //    DataTableManager _dataTableManager = new DataTableManager();
    //    _dataTableManager.LoadAllItemData();
    //    List<ItemData> itemDataList = null;

    //    switch (itemType)
    //    {
    //        case ItemData.ItemType.Weapon:
    //            itemDataList = _dataTableManager.ItemEquippedDataTable;
    //            break;
    //        case ItemData.ItemType.Armor:
    //            itemDataList = _dataTableManager.ItemEquippedDataTable;
    //            break;
    //        case ItemData.ItemType.Accessories:
    //            itemDataList = _dataTableManager.ItemEquippedDataTable;
    //            break;
    //        case ItemData.ItemType.Potion:
    //            itemDataList = _dataTableManager.ItemPotionDataTable;
    //            break;
    //        case ItemData.ItemType.Booty:
    //            itemDataList = _dataTableManager.ItemGoodsDataTable;
    //            break;
    //        default:
    //            Logger.Log("타입 없음 : " + itemType);
    //            return null;
    //    }
    //    if(itemDataList.Count > 0)
    //    {
    //        ItemData itemData = itemDataList[0];
    //        return new Item(itemData);
    //    }
    //    else
    //    {
    //        Logger.Log("해당타입 없음 : " + itemType);
    //        return null;
    //    }
    //}
    #endregion
}
