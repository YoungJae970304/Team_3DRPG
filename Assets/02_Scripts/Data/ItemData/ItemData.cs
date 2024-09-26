using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    //아이템이 공용으로 사용할 정보들
    public enum ItemType
    {
        Equipment,//0, 1, 2
        Potion,//3
        Booty,//4
    }

    public ItemType Type => _itemType;
    public int ID => _id;
    public string Name => _name;
    public Sprite IconSprite => _iconSprite;
    public int BuyingPrice => _buyingPrice;
    public int SellingPrice => _sellingPrice;
    public int MaxAmount => _maxAmount;

    //아이템 타입
    [SerializeField] ItemType _itemType;
    //최대 소지 갯수
    [SerializeField] int _maxAmount = 99;
    //아이템 번호
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    //아이템 아이콘
    [SerializeField] Sprite _iconSprite;
    //구매 가격
    [SerializeField] int _buyingPrice;
    //판매 가격
    [SerializeField] int _sellingPrice;
}