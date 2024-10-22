using System.IO;
using System.Linq;
using UnityEngine;

public class Item
{
    public ItemData Data { get; private set; }

    public Item(ItemData data)
    {
        Data = data;
    }

    #region ID로
    public static Item ItemSpawn(int id,int amount =1)
    {
        //데이터테이블매니저 인스턴스
        DataTableManager dataTableManager = Managers.DataTable;

        ItemData itemData = null;
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var newItem in dataTableManager._AllItemData)
        {
            Logger.Log($"선택된아이템 아이디 {newItem.ID}");
            if (newItem.ID == id)
            {
                
                itemData = newItem;
                break;
            }
        }
        Logger.Log("EquipmentItemData" + (itemData is EquipmentItemData).ToString()) ;
        Logger.Log(itemData.GetType().ToString());
        if (itemData != null)
        {
                switch (itemData.Type)
                {
                    //장착 아이템
                    case ItemData.ItemType.Weapon:
                    case ItemData.ItemType.Armor:
                    case ItemData.ItemType.Accessories:
                        return new EquipmentItem(itemData);
                    //사용 가능 아이템
                    case ItemData.ItemType.Potion:
                        return new ConsumableItem(itemData, amount);
                    //수량만 있는 아이템
                    case ItemData.ItemType.Booty:
                        return new CountableItem(itemData, amount);
                default:
                        Logger.Log($"알 수 없는 아이템 타입 : {itemData.Type}");
                        return null;
                }
        }
        else
        {
            Logger.Log("해당 Id의 아이템을 찾을수가 없습니다 : " + id);
            return null;
        }
    }
    #endregion

    //스프라이트 이미지를 이름으로 저장하고 있기에 이미지 이름을 Resource.load로 경로에서 이미지아이콘 찾아오기
    public Sprite LoadIcon()
    {
        //플레이어 타입에따라 아이디는 같은데 로드 되는 이미지 다르게적용
        //MeleeIcon, MageIcon 폴더에서 로드
        //포션이랑 기타아이템은 EtcIcon폴더에서 로드
        string iconName = "Icon/" + Data.ID.ToString();
        Sprite icon = Resources.Load<Sprite>(iconName);

        if (icon == null) 
        {
            Logger.LogError("icon 로드 실패 ");
            return null;
        }

        return icon;
    }

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
