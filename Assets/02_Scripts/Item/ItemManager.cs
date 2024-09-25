using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour//�κ��丮
{   //������ �׷��� ���� ��ųʸ� Ÿ�� �ϳ��� �κ��丮 �� 1���� �ȴ�.
    Dictionary<ItemData.ItemType, ItemGroup> ItemDick = new Dictionary<ItemData.ItemType, ItemGroup>();
    // Start is called before the first frame update
    void Start()
    {
        //�κ��丮 �ʱ�ȭ
        ItemDick.Add(ItemData.ItemType.Booty, new ItemGroup(15, 100, ItemData.ItemType.Booty));
        ItemDick.Add(ItemData.ItemType.Equipment, new ItemGroup(15, 100, ItemData.ItemType.Equipment));
        ItemDick.Add(ItemData.ItemType.Potion, new ItemGroup(15, 100, ItemData.ItemType.Potion));
    }

    public bool InsertItem(Item item)//������ ���� ��ĭ�������� ��ĭ���� �ߺ��������� ������
    {
        //�������� Ÿ�Կ� ���� Ÿ�Կ� �´� �׷쿡 �����Ѵ�
        return ItemDick[item.Data.Type].Insert(item);
    }

    public Item GetItem(int index, ItemData.ItemType type)//�ε����� Ÿ������ �������� �κ��丮���� �����´�
    {
        return ItemDick[type].GetItem(index);
    }
    public Item Setitem(int index, Item item)//Ư���ε����� �����ۿ� Ư������������ �����Ѵ�
    {
        return ItemDick[item.Data.Type].Setitem(index, item);
    }

    public bool SwitchItem(int index1, int index2, ItemData.ItemType type) { 
        return ItemDick[type].SwitchItem(index1,index2);
    }

    public class ItemGroup
    {//Ư�� Ÿ���� �������� ��� �����ϴ� ������ �׷�
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
        {//�ߺ��� ������ �������� ������ �ø��� �ִ�ġ�� �Ѱ�ٸ� �������� �Ѿ��
        //�ߺ��� ������ ��ĭ�� �������� �Ҵ��Ѵ�.
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
                    //������ ���� �پ��� ����� �������� ��������
                    //���� ���� ������ ������ ���Ҵٸ� continue
                    //�ƴϸ� return;
                }
            }
            if (nullIndex > -1)
            {
                _items[nullIndex] = item;
            }
            return false;
        }

        public Item GetItem(int index)
        {//�ε����� �������� ����
            if (index <= _maxSize)
            {
                return _items[index];
            }
            return null;
        }

        public Item Setitem(int index, Item item)
        {//Ư�� �ε����� ���ϴ� �������� �Ҵ��ϰ� ������ �ִ� �������� ����
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
        {//�� �ε������� �������� ��ġ�� �����Ѵ�.
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