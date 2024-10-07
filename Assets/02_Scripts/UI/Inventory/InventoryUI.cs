using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class InventoryUI : ItemUI
{
    public ItemData.ItemType _currentType= ItemData.ItemType.Potion;

    public Transform _itemTrs;

    public Inventory _inventory;
    
    public Inventory Inventory
    { 
        get {
            UpdateSlot();
            return _inventory; 
        } 
    }

    enum GameObjects {
        Slots,

    }

    public List<ItemSlot> _inventorySlots;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        Bind<GameObject>(typeof(GameObjects));
        _inventory = Managers.Game._player.GetOrAddComponent<Inventory>();
        _inventory.GetItemAction += UpdateSlot;
        //ItemGrap.endGrapAction += UpdateSlot;
        SlotSetting(_currentType);
    }
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        UpdateSlot();
    }
    void SlotSetting(ItemData.ItemType type) {
       int size= _inventory.GetGroupSize(type);
        _inventorySlots = new List<ItemSlot>();
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
}
