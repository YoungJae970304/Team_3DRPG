using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour//인벤토리
{   //아이템 그룹을 담을 딕셔너리 타입 하나당 인벤토리 탭 1개가 된다.
    Dictionary<ItemData.ItemType, ItemGroup> ItemDick = new Dictionary<ItemData.ItemType, ItemGroup>();

    public Action GetItemAction;

    // Start is called before the first frame update
    private void Awake()
    {
        {
            //인벤토리 초기화
            AddGroup(15, 100, ItemData.ItemType.Weapon);
            AddGroup(15, 100, ItemData.ItemType.Armor);
            AddGroup(15, 100, ItemData.ItemType.Accessories);
            AddGroup(15, 100, ItemData.ItemType.Booty);
            AddGroup(15, 100, ItemData.ItemType.Potion);
        }
    }
    

    public void AddGroup(int maxSize, int LimitSize, ItemData.ItemType type) {
        ItemDick.Add(type, new ItemGroup(maxSize, LimitSize, type));

    }

    public bool InsertItem(Item item)//아이템 삽입 빈칸이있으면 빈칸으로 중복이있으면 합쳐짐
    {
        GetItemAction?.Invoke();
        //아이템의 타입에 따라 타입에 맞는 그룹에 삽입한다
        return ItemDick[item.Data.Type].Insert(item);
    }

    public Item GetItem(int index, ItemData.ItemType type)//인덱스와 타입으로 아이템을 인벤토리에서 가져온다
    {
        return ItemDick[type].GetItem(index);
    }
    public Item Setitem(int index, Item item)//특정인덱스의 아이템에 특정아이템으로 변경한다
    {
        return ItemDick[item.Data.Type].Setitem(index, item);
    }
    public Item Remove(int index, ItemData.ItemType type)
    {
        GetItemAction?.Invoke();
        return ItemDick[type].Remove(index);
    }
    public bool SwitchItem(int index1, int index2, ItemData.ItemType type) { 
        return ItemDick[type].SwitchItem(index1,index2);
    }


    public int GetGroupSize(ItemData.ItemType type) {
        return ItemDick[type]._maxSize;
    }

    public class ItemGroup
    {//특정 타입의 아이템을 모아 관리하는 아이템 그룹
        Item[] _items;
        public int _maxSize;
        ItemData.ItemType _type;


        public ItemGroup(int maxSize, int LimitSize, ItemData.ItemType type)
        {
            _items = new Item[LimitSize];
            _maxSize = maxSize;
            _type = type;
        }


        public bool Insert(Item item)
        {//중복이 있으면 아이템의 개수를 늘리고 최대치를 넘겼다면 다음으로 넘어가며
         //중복이 없으면 빈칸에 아이템을 할당한다.
            
            if (item is CountableItem)
            {
                CountableItem countableItem = item as CountableItem;
                for (int i = 0; i < _maxSize; i++)
                {

                    if (_items[i] == null || !(_items[i] is CountableItem)) { continue; }//빈칸이거나 최대치인 아이템은 무시
                    if (_items[i].Data.ID == item.Data.ID)
                    {
                        int overamount = ((CountableItem)_items[i]).AddAmount(countableItem._amount);
                        if (overamount == 0) { return true; }
                        else
                        {
                            countableItem.SetAmount(overamount);
                        }
                        //아이템 갯수 줄어들고 저장된 아이템의 개수증가
                        //만약 받은 아이템 개수가 남았다면 continue
                        //아니면 return;
                    }
                }
                item = countableItem;
            }
                for (int i = 0; i < _maxSize; i++) {
                
                    if (_items[i] == null) { _items[i] = item; return true; }
                }
            
            
            return false;
        }

        public Item GetItem(int index)
        {//인덱스로 아이템을 리턴
            if (index <= _maxSize)
            {
                return _items[index];
            }
            return null;
        }

        public Item Setitem(int index, Item item)
        {//특정 인덱스에 원하는 아이템을 할당하고 이전에 있던 아이템을 리턴
            Item lastItme;
            if (index <= _maxSize)
            {
                lastItme = _items[index];
                _items[index] = item;
                return lastItme;
            }

            return null;
        }
        public Item Remove(int index) {
            if (index <= _maxSize)
            {
                Item lastItme;
                lastItme = _items[index];
                _items[index] = null;
                return lastItme;
            }
            return null;
        }


        public bool SwitchItem(int index1, int index2)
        {//두 인덱스간의 아이템의 위치를 변경한다.
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