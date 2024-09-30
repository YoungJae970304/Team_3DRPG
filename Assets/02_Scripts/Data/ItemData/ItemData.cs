using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ItemData
{
    //아이템이 공용으로 사용할 정보들
    [Serializable]
    public enum ItemType
    {
        Weapon = 1, //1
        Armor,//2
        Accessories,//3
        Potion,//4
        Booty,//5
    }

    public int ID { get { return _id; } set { _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Rarity { get { return _rarity; } set { _rarity = value; } }
    public ItemType Type { get { return _itemType; }set { _itemType = value;} }
    public Sprite IconSprite { get { return _iconSprite; } set { _iconSprite = value; } }
    public int BuyingPrice { get { return _buyingPrice; } set { _buyingPrice = value; } }
    public int SellingPrice { get { return _sellingPrice; } set { _sellingPrice = value; } }
    public int MaxAmount { get { return _maxAmount; } set { _maxAmount = value; } }


    //아이템 번호
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    //아이템 등급
    [SerializeField] int _rarity;
    //아이템 타입
    [SerializeField] ItemType _itemType;
    //아이템 아이콘
    [SerializeField] Sprite _iconSprite;
    //구매 가격
    [SerializeField] int _buyingPrice;
    //판매 가격
    [SerializeField] int _sellingPrice;
    //최대 소지 갯수
    [SerializeField] int _maxAmount = 99;
}