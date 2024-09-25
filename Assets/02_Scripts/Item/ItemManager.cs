using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    Dictionary<ItemData.ItemType, ItemGroup> ItemDick = new Dictionary<ItemData.ItemType, ItemGroup>();
    // Start is called before the first frame update
    void Start()
    {
        ItemDick.Add(ItemData.ItemType.Booty, new ItemGroup(15, 100, ItemData.ItemType.Booty));
        ItemDick.Add(ItemData.ItemType.Equipment, new ItemGroup(15, 100, ItemData.ItemType.Equipment));
        ItemDick.Add(ItemData.ItemType.Potion, new ItemGroup(15, 100, ItemData.ItemType.Potion));
    }

    public bool InsertItem(Item item)
    {
        return ItemDick[item.Data.Type].Insert(item);
    }

    public Item GetItem(int index, ItemData.ItemType type)
    {
        return ItemDick[type].GetItem(index);
    }
    public Item Setitem(int index, Item item)
    {
        return ItemDick[item.Data.Type].Setitem(index, item);
    }

    public class ItemGroup
    {//특정 타입의 아이템을 모아 관리하는 아이템 그룹
        Item[] _items;
        int _maxSize;
        ItemData.ItemType _type;


        public ItemGroup(int maxSize, int LimitSize, ItemData.ItemType type)
        {
            _items = new Item[LimitSize];
            _maxSize = maxSize;
            _type = type;
        }


        public bool Insert(Item item)
        {
            if (item.Data.Type != _type) { return false; }
            int nullIndex = -1;
            for (int i = 0; i < _maxSize; i++)
            {
                if (_items[i] == null && nullIndex == -1)
                {
                    nullIndex = i;
                }
                if (_items[i] == null || _items[i].Data.MaxAmount == 99) { continue; }
                if (_items[i].Data.ID == item.Data.ID && _items[i].Data.MaxAmount == 99)
                {
                    //아이템 갯수 줄어들고 저장된 아이템의 개수증가
                    //만약 받은 아이템 개수가 남았다면 continue
                    //아니면 return;
                }
            }
            if (nullIndex > -1)
            {
                _items[nullIndex] = item;
            }
            return false;
        }

        public Item GetItem(int index)
        {
            if (index <= _maxSize)
            {
                return _items[index];
            }
            return null;
        }

        public Item Setitem(int index, Item item)
        {
            if (item.Data.Type != _type) { return null; }
            Item lastItme;
            if (index <= _maxSize)
            {
                lastItme = _items[index];
                _items[index] = item;
                return lastItme;
            }

            return null;
        }

        public bool SwitchItem(int index1, int index2)
        {
            if (index1 <= _maxSize && index2 <= _maxSize)
            {
                Item temp = _items[index1];
                _items[index1] = _items[index2];
                _items[index2] = temp;
                return true;
            }
            return false;
        }

    }
}