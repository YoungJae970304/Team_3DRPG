using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickItemSlot : ItemSlot
{
    int index = -1;
    public Inventory _inventory;
    
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
            _text.text = _inventory.GetItemAmount(Item.Data.ID).ToString(); ;
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
        Item = item;
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        Item = moveSlot.Item;

        return true;
    }
    
    public void Use()
    {
        (Item as IUsableItem).Use();
        if ((Item as CountableItem).GetCurrentAmount() == 0) {
            Item = _inventory.GetItemToId(Item.Data.ID);
        }
    }
    [ContextMenu("사용 테스트")]
    public void UseText()
    {
        (Item as CountableItem).SetAmount(1);
        Use();
    }

    public override void NullTarget()
    {
        Item = null;
    }
}
