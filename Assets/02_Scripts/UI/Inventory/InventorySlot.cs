using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item _item;
    public Item Item{get=>_item ;}
    public Image _Image;
    public int _index;
    Inventory _itemManager;
    InventoryUI _inventory;
    [SerializeField] Text _text;

    public void Init(Inventory itemManager, InventoryUI inventory) {
        _itemManager = itemManager;
        _inventory = inventory;
        _index = transform.GetSiblingIndex();
    }

    public void UpdateInfo()
    {
        
        _item = _itemManager.GetItem(_index, _inventory._currentType);
        if (_item == null)
        {
            _Image.enabled = false;
            _text.text = "";
            return;
        }
        
        _Image.enabled = true;
        _Image.sprite = _item.Data.IconSprite == null ? _Image.sprite : _item.Data.IconSprite;
        if (_item is CountableItem)
        {
            _text.text = (_item as CountableItem)._amount.ToString(); ;
        }
        else {
            _text.text = "";
        }
        
    }

    public void test()
    {
        if (_item == null) { return; }
        Logger.Log(_item.Data.Name);
    }
}
