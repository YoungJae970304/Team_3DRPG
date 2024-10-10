using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class ItemDataListWrapper
{
    public List<ItemData> ItemDataList = new List<ItemData>();
}

[Serializable]
public class ItemData : IData
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

    public int ID;
    public string Name; 
    public int Grade;
    public ItemType Type;
    public int BuyingPrice;
    public int SellingPrice;
    public int MaxAmount;
    public int LimitLevel;
    public string IconSprite = "Icon/TestIcon"; //아이템 이미지 스프라이트 아이콘 기본 경로


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

    //아이템 데이터 초기화
    public void SetDefaultData()
    {
        ID = _id;
        Name = _name;
        Grade = _grade;
        Type = _itemType;
        BuyingPrice = _buyingPrice;
        SellingPrice = _sellingPrice;
        MaxAmount = _maxAmount;
        LimitLevel = _limitLevel;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::세이브 데이터");
        bool result = false;
        try
        {
            string key = "ItemData_" + ID;
            string itemDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, itemDataJson);
            PlayerPrefs.Save();
            result = true;
           
        }catch(Exception e)
        {
            Logger.Log($"저장 실패(" + e.Message + ")");
        }
        return result;

    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::로드 데이터");

        bool result = false;
        try
        {
            string key = "ItemData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string itemDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(itemDataJson, this);
                result = true;
            }
            else
            {
                Logger.LogWarning("저장된 데이터가 없음");
            }
        }
        catch(Exception e)
        {
            Logger.Log("로드 실패(" + e.Message + ")");
        }
        return result;
    }
}