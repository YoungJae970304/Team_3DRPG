using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProxy : MonoBehaviour,IItemDropAble
{
    Inventory _inventory;
    private void Awake()
    {
        _inventory = Managers.Game._player.GetComponent<Inventory>();
    }
    public void ItemInsert(ItemSlot moveSlot)
    {
        if (moveSlot is InventorySlot) { return; }
        if (_inventory.InsertItem(moveSlot.Item))
        {
            moveSlot.RemoveItem();
        }
        else
        {
            moveSlot.UpdateSlotInfo();

        }
    }
}
