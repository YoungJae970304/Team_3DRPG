using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTableManager
{
    //CSVData폴더 안에 있는 csv값을 스트링으로 가져오고 csv파일로 읽어올거임
    const string DATA_PATH = "CSVData";
    //저장할 때 사용할 키
    const string _PLAYER_PREFS_KEY = "ItemDataList";
    public void Init()
    {
        LoadItemDataTable();
    }

    #region 아이템 데이터 로드
    //폴더 안에 있는 장비아이템 데이터 csv파일
    const string EQUIPMENT_ITEM_DATA_TABLE = "Equipment_Data_Table";
    //데이터 테이블 폴더 안에 있는 포션아이템 데이터 csv파일
    const string POTION_ITEM_DATA_TABLE = "Potion_Data_Table";
    //데잍터 테이블 폴더 안에 있는 기타아이템 데이터 csv파일
    const string GOODS_ITEM_DATA_TABLE = "Goods_Data_Table";

    //실질적인 아이템 데이터의 리스트
    public List<ItemData> ItemEquippedDataTable = new List<ItemData>();
    public List<ItemData> ItemPotionDataTable = new List<ItemData>();
    public List<ItemData> ItemGoodsDataTable = new List<ItemData>();
    public List<ItemData> _AllItemData = new List<ItemData>(); 
    public void LoadItemDataTable()
    {
        #region 장비 데이터
        //장비 csv 파일 읽어오기
        var parsedEquippedDataTable = CSVReader.Read($"{DATA_PATH}/{EQUIPMENT_ITEM_DATA_TABLE}");

        foreach (var data in parsedEquippedDataTable)
        {
            ItemData itemData = null;
            //아이템 데이터 안에있는 적잘한 서브클래스인스턴스를 생성
            //장비 데이터cvs파일을 불러와서 저장해주기
            string equippedType = data["EquippedType"].ToString();
            //무기
            if(equippedType == "1")
            {
                itemData = new WeaponItemData()
                {
                    //id값
                   ID = Convert.ToInt32(data["ID"]),
                   //이름
                   Name = data["Name"].ToString(),
                   //착용 레벨
                   LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                   //등급
                   Grade = Convert.ToInt32(data["Grade"]),
                   //공격력
                   AttackPower = Convert.ToInt32(data["AttackPower"]),
                   //구매 가격
                   BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                   //판매 가격
                   SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                   //소지 개수 : 1
                   MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
            }
            //방어구
            else if (equippedType == "2")
            {
                itemData = new ArmorItemData()
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                    Grade = Convert.ToInt32(data["Grade"]),
                    Health = Convert.ToInt32(data["Health"]),
                    Defense = Convert.ToInt32(data["Defense"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
            }
            //악세
            else if (equippedType == "3")
            {
                itemData = new AccessoriesItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                    Grade = Convert.ToInt32(data["Grade"]),
                    HealthRegen = Convert.ToInt32(data["HealthRegen"]),
                    ManaRegen = Convert.ToInt32(data["ManaRegen"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
            }
            if(itemData != null)
            {
                ItemEquippedDataTable.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
        #endregion

        #region 포션 데이터
        //포션 데이터 터이블 가져오기
        var parsedPotionDataTable = CSVReader.Read($"{DATA_PATH}/{POTION_ITEM_DATA_TABLE}");
        //순회 하며 포션데이터를 현재 포션 아이템 데이터리스트에 넣어주기
        foreach (var data in parsedPotionDataTable)
        {
            ItemData itemData = null;
            string itemType = data["ItemType"].ToString();
            string valueType = data["ValueType"].ToString();

            if(itemType == "4" && valueType == "1")
            {
                itemData = new PotionItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    Grade = Convert.ToInt32(data["Grade"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    //적용 종류 : 회복
                    ValType = PotionItemData.ValueType.Recovery,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    //쿨타임
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
                    //회복 및 버프 효과
                    Value = Convert.ToSingle(data["Value"]),//%값
                };
            }else if(itemType == "5" && valueType == "2")
            {
                itemData = new PotionItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    Grade = Convert.ToInt32(data["Grade"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    //적용 종류 : 공격력 증가
                    ValType = PotionItemData.ValueType.Atk,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
                    DurationTime = Convert.ToInt32(data["DurationTime"]),
                    Value = Convert.ToSingle(data["Value"]),//%값
                };
            }
            else if (itemType == "5" && valueType == "3")
            {
                itemData = new PotionItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    Grade = Convert.ToInt32(data["Grade"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    //적용 종류 : 방어력 증가
                    ValType = PotionItemData.ValueType.Def,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
                    DurationTime = Convert.ToInt32(data["DurationTime"]),
                    Value = Convert.ToSingle(data["Value"]),//%값
                };
            }
            if (itemData != null)
            {
                ItemPotionDataTable.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
        #endregion

        #region 기타 데이터
        //기타아이템 데이터 테이블 가져오기
        var parsedGoodsDatTable = CSVReader.Read($"{DATA_PATH}/{GOODS_ITEM_DATA_TABLE}");
        
        foreach ( var data in parsedGoodsDatTable )
        {
            ItemData itemData = null;
            string itemType = data["ItemType"].ToString();
            if(itemType == "1")
            {
                itemData = new GoodsItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    Grade = Convert.ToInt32(data["Grade"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    //설명 텍스트
                    FlavorText = data["FlavorText"].ToString(),
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
            }
            if (itemData != null)
            {
                ItemGoodsDataTable.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
        #endregion
    }
    #endregion

    #region 모든 데이터 저장및 로드
    //모든 데이터 플레이어프랩스로 제이슨저장
    public void SaveAllItemData()
    {
        ItemDataListWrapper allItemData = new ItemDataListWrapper()
        {
            ItemDataList = new List<ItemData>()
        };
        //모든 아이템 데이터를 하나의 리스트로 합침
        allItemData.ItemDataList.AddRange(ItemEquippedDataTable);
        allItemData.ItemDataList.AddRange(ItemPotionDataTable);
        allItemData.ItemDataList.AddRange(ItemGoodsDataTable);

        //합친 데이터를 Json으로 변환
        string itemJson = JsonUtility.ToJson(allItemData);
        //Json데이터를 플레이어프랩스에 저장
        PlayerPrefs.SetString(_PLAYER_PREFS_KEY, itemJson);
        PlayerPrefs.Save();
        Logger.Log("저장 완료 : " + itemJson);
    }
    //모든 데이터를 플레이어프랩스로 제이슨 로드
    public void LoadAllItemData()
    {
        //저장된 제이슨 문자열 가져오기
        string itemDataJson = PlayerPrefs.GetString(_PLAYER_PREFS_KEY);
        if (!string.IsNullOrEmpty(itemDataJson))
        {
            //Json을 다시 객체로 변환시킴
            ItemDataListWrapper loadedData = JsonUtility.FromJson<ItemDataListWrapper>(itemDataJson);
            //기존 데이터 비우기
            ItemEquippedDataTable.Clear();
            ItemPotionDataTable.Clear();
            ItemGoodsDataTable.Clear();
            //타입에 맞춰 데이터를 다시 리스트에 추가
            foreach (var item in loadedData.ItemDataList)
            {
                if (item.Type == ItemData.ItemType.Weapon || item.Type == ItemData.ItemType.Armor || item.Type == ItemData.ItemType.Accessories)
                {
                    ItemEquippedDataTable.Add(item);
                }
                else if (item.Type == ItemData.ItemType.Potion)
                {
                    ItemPotionDataTable.Add(item);
                }
                else if (item.Type == ItemData.ItemType.Booty)
                {
                    ItemGoodsDataTable.Add(item);
                }
                _AllItemData.Add(item);
            }
            Logger.Log("데이터 로드 완료" + itemDataJson);
        }
        else
        {
            Logger.LogError("저장된 데이터가 없음");
        }
    }
    #endregion
}
