using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager
{
    //DataTable폴더 안에 있는 csv값을 스트링으로 가져오고 csv파일로 읽어올거임
    const string DATA_PATH = "DataTable";

    #region 아이템 데이터 로드
    //데이터 테이블 폴더 안에 있는 장비아이템 데이터 csv파일
    const string EQUIPMENT_ITEM_DATA_TABLE = "Equipment_Item_Data_Table";
    //데이터 테이블 폴더 안에 있는 포션아이템 데이터 csv파일
    const string POTION_ITEM_DATA_TABLE = "Potion_Data_Table";
    //데잍터 테이블 폴더 안에 있는 기타아이템 데이터 csv파일
    const string GOODS_ITEM_DATA_TABLE = "Goods_Data_Table";

    //실질적인 아이템 데이터의 리스트
    List<ItemData> ItemEquippedDataTable = new List<ItemData>();
    List<ItemData> ItemPotionDataTable = new List<ItemData>();
    List<ItemData> ItemGoodsDataTable = new List<ItemData>();

    void LoadItemDataTable()
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
                   ID = Convert.ToInt32(data["ID"]),
                   Name = data["Name"].ToString(),
                   LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                   Grade = Convert.ToInt32(data["Grade"]),
                   AttackPower = Convert.ToInt32(data["AttackPower"]),
                   BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                   SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                   MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
                ItemEquippedDataTable.Add(itemData);
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
                ItemEquippedDataTable.Add(itemData);
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
                ItemEquippedDataTable.Add(itemData);
            }
        }
        //불러와서 저장해준 장비cvs데이터를 리스트에 넣어서 저장해주기
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
                    ValType = PotionItemData.ValueType.Recovery,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
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
                    ValType = PotionItemData.ValueType.Atk,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
                    DurationTime = Convert.ToInt32(data["DurationTime"]),
                    Value = Convert.ToSingle(data["Value"]),//%값
                };
            }else if (itemType == "5" && valueType == "3")
            {
                itemData = new PotionItemData
                {
                    ID = Convert.ToInt32(data["ID"]),
                    Name = data["Name"].ToString(),
                    Grade = Convert.ToInt32(data["Grade"]),
                    BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                    SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                    ValType = PotionItemData.ValueType.Def,
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                    CoolTime = Convert.ToInt32(data["CoolTime"]),
                    DurationTime = Convert.ToInt32(data["DurationTime"]),
                    Value = Convert.ToSingle(data["Value"]),//%값
                };
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
                    FlavorText = data["FlavorText"].ToString(),
                    MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                };
            }
        }
        #endregion
    }
    #endregion
}
