using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemDataListWrapper
{
    public List<ItemData> ItemDataList = new List<ItemData>();
}

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
        DropData,
    }

    public string paths;

    public int ID;
    public string Name; 
    public int Grade;
    public ItemType Type;
    public int BuyingPrice;
    public int SellingPrice;
    public int MaxAmount;
    public int LimitLevel;
    public string IconSprite;

    //아이템 번호
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    //아이템 등급
    [SerializeField] int _grade;
    //아이템 타입
    [SerializeField] ItemType _itemType;
    //구매 가격
    [SerializeField] int _buyingPrice;
    //판매 가격
    [SerializeField] int _sellingPrice;
    //최대 소지 갯수
    [SerializeField] int _maxAmount = 99;
    //착용 가능 레벨
    [SerializeField] int _limitLevel;
}