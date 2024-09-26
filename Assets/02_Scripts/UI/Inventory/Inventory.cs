using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public ItemData.ItemType _currentType= ItemData.ItemType.Potion;

    public Transform _itemTrs;

    public ItemManager _itemManager;

    public ItemManager ItemManager { 
        get {
            UpdateSlot();
            return _itemManager; 
        } 
    }

    public List<InventorySlot> _inventorySlots;
    // Start is called before the first frame update
    void Start()
    {
        _itemManager.GetItemAction += UpdateSlot;
        SlotSetting(_currentType);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SlotSetting(ItemData.ItemType type) {
       int size= _itemManager.GetGroupSize(type);
        _inventorySlots = new List<InventorySlot>();
        for (int i = 0; i < size; i++) {
            InventorySlot slot= Managers.Resource.Instantiate("UI/Slot", _itemTrs).GetComponent<InventorySlot>();
            slot.Init(_itemManager,this);
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
}
