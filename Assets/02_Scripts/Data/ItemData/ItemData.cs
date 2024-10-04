using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class ItemDataListWrapper
{
    public List<ItemData> ItemDataList { get; set; } = new List<ItemData>();
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
    }

    public int ID { get { return _id; } set { _id = value; } }
    public string Name { get { return _name; } set { _name = value; } }
    public int Grade { get { return _grade; } set { _grade = value; } }
    public ItemType Type { get { return _itemType; }set { _itemType = value;} }
    public Sprite IconSprite { get { return _iconSprite; } set { _iconSprite = value; } }
    public int BuyingPrice { get { return _buyingPrice; } set { _buyingPrice = value; } }
    public int SellingPrice { get { return _sellingPrice; } set { _sellingPrice = value; } }
    public int MaxAmount { get { return _maxAmount; } set { _maxAmount = value; } }
    public int LimitLevel { get { return _limitLevel; } set { _limitLevel = value; } }


    //아이템 번호
    [SerializeField] int _id;
    //아이템 이름
    [SerializeField] string _name;
    //아이템 등급
    [SerializeField] int _grade;
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
    //착용 가능 레벨
    int _limitLevel;

    //아이템 데이터 초기화
    public void SetDefaultData()
    {
        ID = _id;
        Name = _name;
        Grade = _grade;
        Type = _itemType;
        IconSprite = _iconSprite;
        BuyingPrice = _buyingPrice;
        SellingPrice = _sellingPrice;
        MaxAmount = _maxAmount;
        LimitLevel = _limitLevel;
    }

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");
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
            Logger.Log($"Load failed (" + e.Message + ")");
        }
        return result;

    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::Save Data");

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
            Logger.Log("Save failed(" + e.Message + ")");
        }
        return result;
    }
}