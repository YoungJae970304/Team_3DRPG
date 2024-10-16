using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShopUIData : BaseUIData {
    List<int> _itemCode;
}

public class ShopUI : ItemUI
{
    public Transform _itemTrs;
    [SerializeField] int _size;
    public List<ShopItemSlot> _inventorySlots;
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
            
        }); ;
        SlotSetting();
    }
    //아이템 판매
    public void SellItem(Item item) { 
        
    
    }
    public override void SetInfo(BaseUIData uiData)
    {
        base.SetInfo(uiData);
    }

    void SlotSetting()
    {
        _inventorySlots = new List<ShopItemSlot>();
        for (int i = 0; i < _size; i++)
        {
            ShopItemSlot slot = Managers.Resource.Instantiate("UI/ShopItemSlot",
                GetGameObject((int)GameObjects.Slots).transform).GetComponent<ShopItemSlot>();
            slot.Init();
            _inventorySlots.Add(slot);
        }
    }
}
