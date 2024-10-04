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
        foreach (var newItem in _dataTableManager._AllItemData)
        {
            if (newItem.ID == id)
            {
                itemData = newItem;
                break;
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

    #region type에따른 id로 생성 시키기
    //public static Item ItemSpawn(ItemData.ItemType itemType, int id)
    //{
    //    DataTableManager _dataTableManager = new DataTableManager();
    //    _dataTableManager.LoadAllItemData();
    //    List<ItemData> itemDataList = null;

    //    switch (itemType)
    //    {
    //        case ItemData.ItemType.Weapon:
    //        case ItemData.ItemType.Armor:
    //        case ItemData.ItemType.Accessories:
    //            itemDataList = _dataTableManager._EquipeedItemData;
    //            break;
    //        case ItemData.ItemType.Potion:
    //            itemDataList = _dataTableManager._PotionItemData;
    //            break;
    //        case ItemData.ItemType.Booty:
    //            itemDataList = _dataTableManager._GoodsItemData;
    //            break;
    //        default:
    //            Logger.Log("타입 없음 : " + itemType);
    //            break;
    //    }
    //    //아이템 데이터가 저장된 아이템 데이터 리스트에서순회하며 id를 찾고 새로운 아이템을 반환
    //    foreach (ItemData itemData in itemDataList)
    //    {
    //        if (itemData.ID == id)
    //        {
    //            return new Item(itemData);
    //        }
    //    }
    //    Logger.Log($"해당{id}와 일치하는게 없음");
    //    return null;
    //}
    #endregion
}
