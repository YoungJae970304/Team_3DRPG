using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopUIData : BaseUIData {
    public List<(int,int)> _itemCode;
}

public class ShopUI : ItemUI
{
    public Transform _itemTrs;
    [SerializeField] int _size;
    public List<ShopItemSlot> _shopItemSlot;

    

    enum GameObjects
    {
        Slots,
        Window
    }

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<GameObject>(typeof(GameObjects));
        GetGameObject((int)GameObjects.Window).GetOrAddComponent<ItemProxy>().SetProxy((moveSlot) => {
            _shopItemSlot[0].ItemInsert(moveSlot);
        }); ;
    }
    //아이템 판매
    public void SellItem(Item item) { 
        
    
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
        ShopUIData shopData = uiData as ShopUIData;
        SlotSetting(shopData._itemCode.Count);
        for (int i = 0; i < shopData._itemCode.Count; i++) {
            _shopItemSlot[i].Setitem(Item.ItemSpawn(shopData._itemCode[i].Item1, shopData._itemCode[i].Item2)); 
        }
    }

    void SlotSetting(int size)
    {
        _shopItemSlot = new List<ShopItemSlot>();
        for (int i = 0; i < size; i++)
        {
            ShopItemSlot slot = Managers.Resource.Instantiate("UI/ShopItemSlot",
                GetGameObject((int)GameObjects.Slots).transform).GetComponent<ShopItemSlot>();
            slot.Init();
            _shopItemSlot.Add(slot);
        }
    }
}
/*
class savedata {
    List<InventoryData> inventoryDatas;
    List<SkillSaveData> SkillSaveDatas;
    List<equipmentsaveData> equipmentsaveDatas;
    PLayerPosition pLayerPosition;
    //퀘스트 클리어 정보
    List<QuestData> questList;
    int level;
    int exp;
    int sp;
}

class PLayerPosition {
    int x;
    int y;
    int z;
}
class QuestData {
    string name;
    int amount1;
    int amount2;
}

class equipmentsaveData { 


}
class InventoryData {
    int id;
    int index;
    int amount;
}
class SkillSaveData {
    string name;
    int level;
}
*/