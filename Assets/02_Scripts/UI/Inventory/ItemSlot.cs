using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class ItemSlot : MonoBehaviour
{
    protected Item _item;
    protected Action itemChangedAction;
    public Item Item{get=>_item ; protected set {
            
            itemChangedAction?.Invoke();
            _item = value;
            UpdateSlotInfo();
        } }
    public Image _Image;
    [SerializeField] protected Text _text;
    public ItemData.ItemType slotType = ItemData.ItemType.Weapon;


    public void UpdateSlotInfo()
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
        if (Item is CountableItem)
        {
            _text.text = (Item as CountableItem)._amount.ToString(); ;
        }
        else {
            _text.text = "";
        }
    }

    public abstract void ItemInsert(ItemSlot moveSlot);
    public abstract bool MoveItem(ItemSlot moveSlot);
}
