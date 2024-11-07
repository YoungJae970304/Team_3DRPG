using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class InventoryUI : ItemDragUI
{
    public ItemData.ItemType _currentType= ItemData.ItemType.Potion;

    public Inventory _inventory;
    
    public Inventory Inventory
    { 
        get {
            UpdateSlot();
            return _inventory; 
        } 
    }

    enum GameObjects {
        Inventory,
        Slots,
    }
    enum Texts
    {
        MoneyText,
    }
    public List<InventorySlot> _inventorySlots;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        _inventory = Managers.Game._player.gameObject.GetOrAddComponent<Inventory>();
        _inventory.GetItemAction += UpdateSlot;
        //ItemGrap.endGrapAction += UpdateSlot;
        GetGameObject((int)GameObjects.Inventory).GetOrAddComponent<ItemProxy>().SetProxy((moveSlot) => {
            if (!(moveSlot is ItemSlot)) { return; }
            if (moveSlot is InventorySlot|| moveSlot is QuickItemSlot) { return; }
            if (moveSlot is ShopItemSlot) { (moveSlot as ShopItemSlot).BuyConfirm(_inventorySlots[0]); return; }
            if (_inventory.InsertItem((moveSlot as ItemSlot).Item)==0)
            {
                (moveSlot as ItemSlot).RemoveItem();
            }
            else
            {
                (moveSlot as ItemSlot).UpdateSlotInfo();
            }
        }); ;
        SlotSetting(_currentType);
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        MoneyUpdate(Managers.Game._player._playerStatManager.Gold);
        PubAndSub.Subscrib<int>("Gold", MoneyUpdate);
        UpdateSlot();
    }
    void SlotSetting(ItemData.ItemType type) {
       int size= _inventory.GetGroupSize(type);
        _inventorySlots = new List<InventorySlot>();
        for (int i = 0; i < size; i++) {
            InventorySlot slot= Managers.Resource.Instantiate("UI/InventorySlot",
                GetGameObject((int)GameObjects.Slots).transform).GetComponent<InventorySlot>();
            slot.Init(_inventory,this);
            _inventorySlots.Add(slot);
        }
    }
    [ContextMenu("update")]
    public void UpdateSlot()
    {
        if (!gameObject.activeSelf) { return; }
        for (int i = 0; i < _inventorySlots.Count; i++)
        {
            _inventorySlots[i].UpdateInfo();
        }
    }

    public void ChageGroup(int type) {
        _currentType = (ItemData.ItemType)type;
        if (Enum.IsDefined(typeof(ItemData.ItemType), _currentType)) {
            UpdateSlot();
        }
    }

    public void MoneyUpdate(int amount) {
        GetText((int)Texts.MoneyText).text = amount.ToString();;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        IItemDragAndDropAble slot= (Managers.UI.GetActiveUI<DragAndDrop>() as DragAndDrop).CurrnetSlot;
        if (slot != null&& slot is ItemSlot) {
            ItemSlot itemSlot = slot as ItemSlot;
            if (itemSlot.Item != null) {
                _currentType = itemSlot.Item.Data.Type;
                UpdateSlot();

            }
        
        }
    }
}
