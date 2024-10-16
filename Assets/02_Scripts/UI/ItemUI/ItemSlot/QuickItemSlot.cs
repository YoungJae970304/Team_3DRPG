using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickItemSlot : ItemSlot
{
    int index = -1;
    public Inventory Inventory;
    
    public override void UpdateSlotInfo()
    {
        if (Item == null)
        {
            _Image.enabled = false;
            _text.text = "";
            return;
        }

        _Image.enabled = true;
        //_Image.sprite = Item.Data.IconSprite == null ? _Image.sprite : Item.Data.IconSprite;
        _Image.sprite = Item.LoadIcon();
        if (_text == null) { return; }
        if (Item is CountableItem)
        {
            _text.text = ((Item as CountableItem).GetCurrentAmount()+
                Inventory.GetItemAmount(Item.Data.ID)).ToString(); ;
        }
        else
        {
            _text.text = "";
        }
    }
    public override void ItemInsert(ItemSlot moveSlot)
    {
        if (!(moveSlot is InventorySlot))//인벤토리가 아니면 무시함
        { return; }
        Item item = moveSlot.Item;
        if (item ==null|| item.Data.Type != slotType) { return; }
        moveSlot.MoveItem(this);
        Item = item;
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;

        return true;
    }
}
