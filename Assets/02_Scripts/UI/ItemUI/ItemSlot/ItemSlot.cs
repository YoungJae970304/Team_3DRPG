using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public abstract class ItemSlot : MonoBehaviour, IItemDropAble
{
    protected Item _item;
    public Action itemChangedAction;
    public Item Item{get=>_item ; protected set {
            _item = value; 
            itemChangedAction?.Invoke();
            UpdateSlotInfo();
        } }
    public Image _Image;
    [SerializeField] protected TextMeshProUGUI _text;
    public ItemData.ItemType slotType = ItemData.ItemType.Weapon;


    public virtual void UpdateSlotInfo()
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
            _text.text = (Item as CountableItem)._amount.ToString(); ;
        }
        else {
            _text.text = "";
        }
    }

    public virtual void ItemInsert(ItemSlot moveSlot) {
        if(Item != null && Item.Data.ID == moveSlot.Item.Data.ID)
            {
            if (Item is CountableItem)
            {
                int overAmount = ((CountableItem)Item).AddAmount(((CountableItem)moveSlot.Item)._amount);
                ((CountableItem)moveSlot.Item).SetAmount(overAmount);
                return;
            }
        }

    }
    public abstract bool MoveItem(ItemSlot moveSlot);

    public void RemoveItem() {
        Item = null;
    }
}
