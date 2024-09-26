using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item _Item;
    Image _Image;
    int _index;
    ItemManager _itemManager;
    Inventory _inventory;
    [SerializeField] Text _text;
    private void Awake()
    {
        _Image = GetComponent<Image>();

    }
    private void Start()
    {
        _index = transform.GetSiblingIndex();
        
    }

    public void OnEnable()
    {
        //UpdateInfo();
    }

    public void Init(ItemManager itemManager, Inventory inventory) {
        _itemManager = itemManager;
        _inventory = inventory;
    }

    public void UpdateInfo()
    {
        _Item = _itemManager.GetItem(_index, _inventory._currentType);
        if (_Item == null)
        {
            _Image.sprite = null;
            _text.text = "";
            return;
        }
            _Image.sprite = _Item.Data.IconSprite == null ? _Image.sprite : _Item.Data.IconSprite;
        if (_Item is CountableItem)
        {
            _text.text = (_Item as CountableItem)._amount.ToString(); ;
        }
        else {
            _text.text = "";
        }
        
    }

    public void test()
    {
        if (_Item == null) { return; }
        Logger.Log(_Item.Data.Name);
    }
}
