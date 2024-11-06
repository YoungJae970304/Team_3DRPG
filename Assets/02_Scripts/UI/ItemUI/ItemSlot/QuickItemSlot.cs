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
    public override void ItemInsert(IItemDragAndDropAble moveSlot)
    {
        if (!(moveSlot is InventorySlot))//인벤토리가 아니면 무시함
        { return; }
        Item item = (moveSlot as InventorySlot).Item;
        if (item ==null|| item.Data.Type != _slotType) { return; }
        Setitem(item);
    }
    public override bool MoveItem(ItemSlot moveSlot)
    {
        if (moveSlot.GetType() == GetType()) {
            Item = moveSlot.Item;
            if (Item is ConsumableItem)
            {
                _isUsealbe = true;
            }
            return true;
        }
        return false;
    }

    public override void Use()
    {
        base.Use();
        (Item as IUsableItem).Use(_inventory.GetComponent<Player>());
        if (Item is CountableItem)
        {
            _text.text = _inventory.GetItemAmount(Item.Data.ID).ToString(); ;
        }
        if ((Item as CountableItem).GetCurrentAmount() == 0)
        {
            Item = _inventory.GetItemToId(Item.Data.ID);
        }
    }

    public override void Setitem(Item item)
    {
        Item = item;
        if (Item is ConsumableItem)
        {
            _isUsealbe = true;
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
