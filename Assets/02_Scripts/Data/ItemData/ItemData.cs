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
        BuffPotion,
        Booty,//5
        DropData,
    }

    //아이템 번호
    public int ID;
    //아이템 이름
    public string Name;
    //아이템 등급
    public int Grade;
    //아이템 타입
    public ItemType Type;
    //구매 가격
    public int BuyingPrice;
    //판매 가격
    public int SellingPrice;
    //최대 소지 갯수
    public int MaxAmount;
    //착용 가능 레벨
    public int LimitLevel;
    //이미지 로드용 변수
    public string IconSprite;
}