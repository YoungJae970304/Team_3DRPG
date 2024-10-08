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
        SaveAllItemData();
        LoadAllItemData();
    }

    //장비아이템 데이터 CSV파일
    const string EQUIPMENT_ITEM_DATA_TABLE = "Equipment_Data_Table";
    //포션아이템 데이터 CSV파일
    const string POTION_ITEM_DATA_TABLE = "Potion_Data_Table";
    //기타아이템 데이터 CSV파일
    const string GOODS_ITEM_DATA_TABLE = "Goods_Data_Table";
    //드랍 데이터 테이블 CSV파일
    const string MONSTER_DROP_DATA_TABLE = "Monster_Drop_Data_Table";
    //퀘스트 데이터 테이블 CSV 파일
    const string QUEST_DATA_TABLE = "";
    //각각의 아이템 데이터 리스트-드랍할때 알맞게 사용-
    public List<ItemData> _EquipeedItemData = new List<ItemData>();
    public List<ItemData> _PotionItemData = new List<ItemData>();
    public List<ItemData> _GoodsItemData = new List<ItemData>();
    public List<DropData> _MonsterDropData = new List<DropData>();
    //실질적인 아이템 데이터 리스트의 전체 리스트
    public List<ItemData> _AllItemData = new List<ItemData>();
    //csv파일에서 퀘스트 데이터를 저장 하였으니 그 리스트 를 매니저에서 불러와 줌
    public List<QuestData> QuestDataList = new List<QuestData>();

    #region 장비데이터테이블 함수
    void EquipmentDataTable(string dataPath, string equipmentDataTable)
    {
        //데이터테이블에서 불러와
        var parsedEquippedDataTable = CSVReader.Read($"{dataPath}/{equipmentDataTable}");
        foreach (var data in parsedEquippedDataTable)
        {
            ItemData itemData = null;
            //아이템 데이터 안에있는 적잘한 서브클래스인스턴스를 생성
            //장비 데이터csv파일을 불러와서 저장해주기
            itemData = new EquipmentItemData
            {
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //이름
                Name = data["Name"].ToString(),
                //등급
                Grade = Convert.ToInt32(data["Grade"]),
                //아이템 타입
                Type = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), data["EquippedType"].ToString()),
                //착용 레벨
                LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                //판매 가격
                BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                //구매 가격
                SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                //공격력
                AttackPower = Convert.ToInt32(data["AttackPower"]),
                //방어력
                Defense = Convert.ToInt32(data["Defense"]),
                //체력
                Health = Convert.ToInt32(data["Health"]),
                //체력 리젠
                HealthRegen = Convert.ToInt32(data["HealthRegen"]),
                //마나
                Mana = Convert.ToInt32(data["Mana"]),
                //마나 리젠
                ManaRegen = Convert.ToInt32(data["ManaRegen"]),
                //소지 수량
                MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                //csv파일에 없어서 일단 임시로 로드만 해줌
                IconSprite = Resources.Load<Sprite>("Icon/TestIcon"),
                //아이콘
                //IconSprite = Resources.Load<Sprite>(data["Icon/TestIcon"].ToString()),
            };
            if (itemData != null)
            {
                Logger.Log($"{itemData} 저장됨");
                _EquipeedItemData.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
    }
    #endregion

    #region 소비데이터테이블 함수
    void PotionDataTable(string dataPath, string potionDataTable)
    {
        var parsedPotionData = CSVReader.Read($"{dataPath}/{potionDataTable}");
        foreach (var data in parsedPotionData)
        {
            ItemData itemData = null;
            itemData = new PotionItemData
            {
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //이름
                Name = data["Name"].ToString(),
                //등급
                Grade = Convert.ToInt32(data["Grade"]),
                Type = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), data["ItemType"].ToString()),
                LimitLevel = Convert.ToInt32(data["LimitLevel"]),
                //구매 가격
                BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                //판매 가격
                SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                //회복 타입
                ValType = (PotionItemData.ValueType)Enum.Parse(typeof(PotionItemData.ValueType), data["ValueType"].ToString()),
                //실제 회복 밸류 %(버프는 0)
                Value = Convert.ToSingle(data["Value"]),
                //쿨타임
                CoolTime = Convert.ToInt32(data["CoolTime"]),
                //지속 시간(회복은 0)
                DurationTime = Convert.ToInt32(data["DurationTime"]),
                //소지 개수
                MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                //csv파일에 없어서 일단 임시로 로드만 해줌
                IconSprite = Resources.Load<Sprite>("Icon/TestIcon"),
                //아이콘
                //IconSprite = Resources.Load<Sprite>(data["Icon/TestIcon"].ToString()),
            };
            if (itemData != null)
            {
                Logger.Log($"{itemData} 저장됨");
                _PotionItemData.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
    }
    #endregion

    #region 기타 데이터테이블 함수
    void GoodsDataTable(string dataPath, string goodsDataTable)
    {
        //기타아이템 데이터 테이블 가져오기
        var parsedGoodsDatTable = CSVReader.Read($"{dataPath}/{goodsDataTable}");

        foreach (var data in parsedGoodsDatTable)
        {
            ItemData itemData = null;

            itemData = new GoodsItemData
            {
                ID = Convert.ToInt32(data["ID"]),
                Name = data["Name"].ToString(),
                Grade = Convert.ToInt32(data["Grade"]),
                Type = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), data["ItemType"].ToString()),
                LimitLevel = Convert.ToInt32(data["LimitLv"]),
                BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                //설명 텍스트
                FlavorText = data["FlavorText"].ToString(),
                MaxAmount = Convert.ToInt32(data["MaxAmount"]),
                //csv파일에 없어서 일단 임시로 로드만 해줌
                IconSprite = Resources.Load<Sprite>("Icon/TestIcon"),
                //아이콘
                //IconSprite = Resources.Load<Sprite>(data["Icon/TestIcon"].ToString()),
            };
            if (itemData != null)
            {
                Logger.Log($"{itemData} 저장됨");
                _GoodsItemData.Add(itemData);
                _AllItemData.Add(itemData);
            }
        }
    }
    #endregion

    #region 드랍 데이터테이블 함수
    void DropDataTable(string dataPath, string monsterDropTable)
    {
        var parsedDropdataTable = CSVReader.Read($"{dataPath}/{monsterDropTable}");
        foreach (var data in parsedDropdataTable)
        {
            DropData itemData = null;
            itemData = new DropData
            {
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //이름
                Name = data["Name"].ToString(),
                //아이템 타입1 - 이지
                DropType1 = Convert.ToInt32(data["DropType1"]),
                //시작 값1 - 이지
                StartValue1 = Convert.ToInt32(data["StartVaule1"]),
                //종료 값1 - 이지
                EndValue1 = Convert.ToInt32(data["EndVaule1"]),
                //아이템 타입2 - 노말
                DropType2 = Convert.ToInt32(data["DropType2"]),
                //시작 값2 - 노말
                StartValue2 = Convert.ToInt32(data["StartVaule2"]),
                //종료 값2 - 노말
                EndValue2 = Convert.ToInt32(data["EndVaule2"]),
                //아이템 타입3 - 하드
                DropType3 = Convert.ToInt32(data["DropType3"]),
                //시작 값3 - 하드
                StartValue3 = Convert.ToInt32(data["StartVaule3"]),
                //종료 값3 - 하드
                EndValue3 = Convert.ToInt32(data["EndVaule3"]),
                //아이템 타입4 - 골드
                DropType4 = Convert.ToInt32(data["DropType4"]),
                //시작 값4 - 골드
                StartValue4 = Convert.ToInt32(data["StartVaule4"]),
                //종료 값4 - 골드
                EndValue4 = Convert.ToInt32(data["EndVaule4"]),
                //경험치
                ItemValue5 = Convert.ToInt32(data["ItemType5"]),
                //경험치 값
                Value5 = Convert.ToInt32(data["Vaule5"]),
                //기타템
                ItemValue6 = Convert.ToInt32(data["ItemType6"]),
                //기타템 종류
                Value6 = Convert.ToInt32(data["Vaule6"]),

            };
            if (itemData != null)
            {
                Logger.Log($"{itemData} 저장됨");
                _MonsterDropData.Add(itemData);

            }
        }
    }
    #endregion

    #region 모든 데이터 저장및 로드
    public void LoadItemDataTable()
    {
        EquipmentDataTable(DATA_PATH, EQUIPMENT_ITEM_DATA_TABLE);
        PotionDataTable(DATA_PATH, POTION_ITEM_DATA_TABLE);
        GoodsDataTable(DATA_PATH, GOODS_ITEM_DATA_TABLE);
        DropDataTable(DATA_PATH, MONSTER_DROP_DATA_TABLE);
    }

    //모든 데이터 플레이어프랩스로 제이슨저장
    public void SaveAllItemData()
    {
        ItemDataListWrapper savedData = new ItemDataListWrapper { ItemDataList = _AllItemData };
        DropDataListWrapper savedDropData = new DropDataListWrapper { DropDataList = _MonsterDropData };
        //합친 데이터를 Json으로 변환
        string itemJson = JsonUtility.ToJson(savedData);
        string dropJson = JsonUtility.ToJson(savedDropData);
        //Json데이터를 플레이어프랩스에 저장
        PlayerPrefs.SetString(_PLAYER_PREFS_KEY, itemJson);
        PlayerPrefs.SetString(_PLAYER_PREFS_KEY, dropJson);
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
            DropDataListWrapper loadedDropData = JsonUtility.FromJson<DropDataListWrapper>(itemDataJson);
            //기존 데이터 비우기
            _EquipeedItemData.Clear();
            _PotionItemData.Clear();
            _GoodsItemData.Clear();
            _AllItemData.Clear();
            //타입에 맞춰 데이터를 다시 리스트에 추가
            foreach (var item in loadedData.ItemDataList)
            {
                switch (item.Type)
                {
                    case ItemData.ItemType.Potion:
                        _PotionItemData.Add(item);
                        break;
                    case ItemData.ItemType.Booty:
                        _GoodsItemData.Add(item);
                        break;
                    case ItemData.ItemType.Weapon:
                    case ItemData.ItemType.Armor:
                    case ItemData.ItemType.Accessories:
                        _EquipeedItemData.Add(item);
                        break;
                    default:
                        Logger.LogWarning($"{item.Type}은 알 수 없는 타입입니다.");
                        break;
                }
            }
        }
        else
        {
            Logger.LogError("저장된 데이터가 없음");
        }
    }
    #endregion
}
