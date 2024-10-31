using System;
using System.Collections.Generic;
using UnityEngine;

public struct ItemTypeComparer : IEqualityComparer<ItemData.ItemType>
{
    public bool Equals(ItemData.ItemType a, ItemData.ItemType b) { return a == b; }
    public int GetHashCode(ItemData.ItemType a) { return (int)a; }
}
public class Inventory : MonoBehaviour//인벤토리
{   //아이템 그룹을 담을 딕셔너리 타입 하나당 인벤토리 탭 1개가 된다.
   Dictionary<ItemData.ItemType, ItemGroup> ItemDick = new Dictionary<ItemData.ItemType, ItemGroup>(new ItemTypeComparer());

    public Action GetItemAction;

    // Start is called before the first frame update
    private void Awake()
    {
        {
            //인벤토리 초기화
            AddGroup(15, 100, ItemData.ItemType.Weapon);
            AddGroup(ItemData.ItemType.Weapon, ItemData.ItemType.Armor);
            AddGroup(ItemData.ItemType.Weapon, ItemData.ItemType.Accessories);
            AddGroup(15, 100, ItemData.ItemType.Booty);
            AddGroup(15, 100, ItemData.ItemType.Potion);
        }
    }

    //새로운 아이템 그룹 추가
    //크기,최대로 커질수 있는 크기,아이템의 타입
    public void AddGroup(int maxSize, int LimitSize, ItemData.ItemType type)
    {
        ItemDick.Add(type, new ItemGroup(maxSize, LimitSize, type));

    }
    //여러가지의 타입이 한 그룹에 들어갈 수 있도록 추가하는 메서드
    public void AddGroup(ItemData.ItemType type, ItemData.ItemType type2)
    {
        ItemDick.Add(type2, ItemDick[type]);
        ItemDick[type].AddGroup(type2);

    }

    public int InsertItem(Item item)//아이템 삽입 빈칸이있으면 빈칸으로 중복이있으면 합쳐짐
    {
        Logger.Log(item.Data.Type.ToString());
        int result = ItemDick[item.Data.Type].Insert(item);
        GetItemAction?.Invoke();
        //아이템의 타입에 따라 타입에 맞는 그룹에 삽입한다
        return result;
    }

    public Item GetItem(int index, ItemData.ItemType type)//인덱스와 타입으로 아이템을 인벤토리에서 가져온다
    {
        return ItemDick[type].GetItem(index);
    }
    public Item Setitem(int index, Item item)//특정인덱스의 아이템에 특정아이템으로 변경한다
    {
        Item result = ItemDick[item.Data.Type].Setitem(index, item);
        GetItemAction?.Invoke();
        return result;
    }
    public Item Remove(int index, ItemData.ItemType type)//특정 타입의 인덱스 위치의 아이템을 제거
    {
        Item item = ItemDick[type].Remove(index);
        GetItemAction?.Invoke();
        return item;
    }
    public bool SwitchItem(int index1, int index2, ItemData.ItemType type)//두 인덱스간 아이템을 교환.
    {
        bool result = ItemDick[type].SwitchItem(index1, index2);
        GetItemAction?.Invoke();
        //아이템의 타입에 따라 타입에 맞는 그룹에 삽입한다
        return result;
    }
    //인벤토리에 특정id의 아이템이 몇개 있는지 반환
    public int GetItemAmount(int id) {
        ItemData.ItemType itemType =(ItemData.ItemType)(id / 10000);
        if (!Enum.IsDefined(typeof(ItemData.ItemType), itemType)) {
            return -1;
        }
        return ItemDick[itemType].GetItemAmount(id);
    }
    //아이디로 검색해서 가장 처음 값을 가져온다.
    public Item GetItemToId(int id) {
        ItemData.ItemType itemType = (ItemData.ItemType)(id / 10000);
        if (!Enum.IsDefined(typeof(ItemData.ItemType), itemType))
        {
            return null;
        }
        Item result = ItemDick[itemType].GetItemToId(id);
        GetItemAction?.Invoke();
        return result;
    }

    //슬롯 사이즈 반환
    public int GetGroupSize(ItemData.ItemType type)
    {
        return ItemDick[type]._maxSize;
    }
    //내부에 아이템이 있는지 확인
    public bool IsContain(Item item, ItemData.ItemType type)
    {
        return ItemDick[type].IsContain(item);
    }
    //타입끼리 연결되어있는지 확인
    public bool Containtype(ItemData.ItemType type, ItemData.ItemType type2)
    {
        return ItemDick[type].Containtype(type2);
    }
    public class ItemGroup
    {//특정 타입의 아이템을 모아 관리하는 아이템 그룹
        Item[] _items;              //아이템 배열
        public int _maxSize;        //그룹의 크기
        List<ItemData.ItemType> _type = new List<ItemData.ItemType>();//그룹이 담고있는 아이템들의 타입들
        bool isFull = false;        //슬롯이 꽉찼는지 확인
        public ItemGroup(int maxSize, int LimitSize, ItemData.ItemType type)//크기,한계크기,타입
        {
            _items = new Item[LimitSize];
            _maxSize = maxSize;
            _type.Add(type);
        }
        public void AddGroup(ItemData.ItemType type)//타입추가
        {
            _type.Add(type);
        }
        public int Insert(Item item)
        {//중복이 있으면 아이템의 개수를 늘리고 최대치를 넘겼다면 다음으로 넘어가며
         //중복이 없으면 빈칸에 아이템을 할당한다.
            
            int overAmount = 1; ;
            if (item is CountableItem)//쌓이는 아이템일때
            {
                CountableItem countableItem = item as CountableItem;
                //슬롯이 가득차면 수량 전부 리턴
                if (isFull) { return countableItem.GetCurrentAmount(); }
                for (int i = 0; i < _maxSize; i++)
                {

                    if (_items[i] == null || !(_items[i] is CountableItem)) { continue; }//빈칸이거나 쌓이지않는 아이템은 무시
                    if (_items[i].Data.ID == item.Data.ID)
                    {
                        //아이템 갯수 줄어들고 저장된 아이템의 개수증가
                        //만약 받은 아이템 개수가 남았다면 continue
                        //아니면 return;
                        overAmount = ((CountableItem)_items[i]).AddAmount(countableItem._amount);
                        if (overAmount == 0) { return 0; }
                        else
                        {
                            countableItem.SetAmount(overAmount);
                        }
                        
                    }
                }
                item = countableItem;
            }
            //꽉찼는데 CountableItem이 아니면 1리턴
            if (isFull) { return 1; }
            for (int i = 0; i < _maxSize; i++)//빈칸을 찾아 아이템 설정
            {

                if (_items[i] == null)
                {
                    _items[i] = item; return 0;
                }
            }
            //아이템을 넣지 못했다면 꽉찼다는 표시 후 남은 량만큼 리턴
            isFull = true;
            return overAmount;
        }
        public Item GetItem(int index)
        {//인덱스로 아이템을 리턴
            if (index <= _maxSize)
            {
                return _items[index];
            }
            return null;
        }
        public Item GetItemToId(int id)
        {//ID로 아이템을 리턴
            for (int i = 0; i < _maxSize; i++)
            {
                if (_items[i] == null) { continue; }//빈칸이면 무시
                if (_items[i].Data.ID == id)
                {//개수가 0인아이템이 남아있다면삭제
                    if (_items[i] is CountableItem&& (_items[i] as CountableItem).GetCurrentAmount() == 0) {
                        Remove(i);
                        continue;
                    }
                    return _items[i];
                }
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
        public Item Remove(int index)
        {
            if (index <= _maxSize)
            {
                Item lastItme;
                lastItme = _items[index];
                _items[index] = null;
                isFull = false;
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
        public bool IsContain(Item item)
        {
            return Array.Exists(_items, x => x == item);
        }
        public bool Containtype(ItemData.ItemType type)
        {
            return _type.Contains(type);
        }

        public int GetItemAmount(int id)
        {
            int amount = 0;
            for (int i = 0; i < _maxSize; i++)
            {
                if (_items[i] == null) { continue; }//빈칸이면 무시
                if (_items[i].Data.ID == id)
                {
                    if (_items[i] is CountableItem)
                    {
                        amount += (_items[i] as CountableItem).GetCurrentAmount();
                    }
                    else
                    {
                        amount += 1;
                    }
                }
            }
            return amount;
        }
    }
}