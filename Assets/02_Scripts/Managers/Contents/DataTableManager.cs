using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

public class DataTableManager
{
    //CSVData폴더 안에 있는 csv값을 스트링으로 가져오고 csv파일로 읽어올거임
    public const string _DATA_PATH = "CSVData";

    public void Init()
    {
        LoadItemDataTable();
    }

    //장비아이템 데이터 CSV파일
    const string _EQUIPMENT_ITEM_DATA_TABLE = "Equipment_Data_Table";
    //포션아이템 데이터 CSV파일
    const string _POTION_ITEM_DATA_TABLE = "Potion_Data_Table";
    //기타아이템 데이터 CSV파일
    const string _GOODS_ITEM_DATA_TABLE = "Goods_Data_Table";
    //드랍 데이터 테이블 CSV파일
    const string _MONSTER_DROP_DATA_TABLE = "Monster_Drop_Data_Table";
    //던전 데이터 테이블 CSV파일
    const string _DUNGEON_DATA_TABLE = "DungeonDataTable";
    //몬스터 데이터 테이블 CSV파일
    const string _MONSTER_DATA_TABLE = "Monster_Manager_Table";
    //퀘스트 데이터 테이블 CSV 파일
    const string _QUEST_DATA_TABLE = "Quest_Data_Table";
    // 플레이어 데이터 테이블 CSV 파일
    public string _PLAYER_LEVEL_DATA_TABLE = "Player_Level_Data_Table";
    // 아이템 조합 데이터 테이블 CSV 파일
    const string _SYNTHESIS_DATA_TABLE = "Fusion_List_Data_Table";
    // 스킬 데이터 테이블 CSV 파일
    const string _SKILL_DATA_TABLE = "Skill_Data_Table";
    // 몬스터 스텟 데이터 테이블 CSV 파일
    const string _MONSTERSTAT_DATA_TABLE = "Monster_Stat_Data_Table";//추가 전
    //상점 항목 데이터 테이블 CSV 파일
    public string _SHOP_DATA_TABLE = "Shop_ItemList_Data_Table";

    //각각의 아이템 데이터 리스트-드랍할때 알맞게 사용-
    public List<ItemData> _EquipeedItemData = new List<ItemData>();
    public List<ItemData> _PotionItemData = new List<ItemData>();
    public List<ItemData> _GoodsItemData = new List<ItemData>();
    public List<DropData> _MonsterDropData = new List<DropData>();
    public List<DungeonData> _DungeonData = new List<DungeonData>();
    public List<MonsterData> _MonsterData = new List<MonsterData>();
    public List<QuestData> _QuestData = new List<QuestData>();
    public List<PlayerLevelData> _PlayerLevelData = new List<PlayerLevelData>();
    public List<FusionData> _FusionData = new List<FusionData>();
    public List<SkillData> _SkillData = new List<SkillData>();
    public List<MonsterStatData> _MonsterStatData = new List<MonsterStatData>();
    public List<ShopUIData> _ShopUIData = new List<ShopUIData>();

    //실질적인 아이템만의 데이터 리스트의 전체 리스트
    public List<ItemData> _AllItemData = new List<ItemData>();

    #region 모든 데이터 저장및 로드
    public void LoadItemDataTable()
    {
        EquipmentDataTable(_DATA_PATH, _EQUIPMENT_ITEM_DATA_TABLE);
        PotionDataTable(_DATA_PATH, _POTION_ITEM_DATA_TABLE);
        GoodsDataTable(_DATA_PATH, _GOODS_ITEM_DATA_TABLE);
        DropDataTable(_DATA_PATH, _MONSTER_DROP_DATA_TABLE);
        QuestDataTable(_DATA_PATH, _QUEST_DATA_TABLE);
        DungeonDataTable(_DATA_PATH, _DUNGEON_DATA_TABLE);
        PlayerLevelDataTable(_DATA_PATH, _PLAYER_LEVEL_DATA_TABLE);
        MonsterDataTable(_DATA_PATH, _MONSTER_DATA_TABLE);
        FusionDataTable(_DATA_PATH, _SYNTHESIS_DATA_TABLE);
        SkillDataTable(_DATA_PATH, _SKILL_DATA_TABLE);
        MonsterStatDataTable(_DATA_PATH, _MONSTERSTAT_DATA_TABLE);
        ShopDataTable(_DATA_PATH, _SHOP_DATA_TABLE);
    }
    #endregion

    #region 플레이어 레벨 데이터테이블
    public void PlayerLevelDataTable(string dataPath, string playerLevelDataTable)
    {
        _PlayerLevelData.Clear();

        var parsedPlayerLevelDataTable = CSVReader.Read($"{dataPath}/{playerLevelDataTable}");

        PlayerLevelData levelData = null;

        foreach (var data in parsedPlayerLevelDataTable)
        {
            levelData = new PlayerLevelData
            {
                Level = Convert.ToInt32(data["Level"]),
                MaxEXP = Convert.ToInt32(data["MaxEXP"]),
                SpAddAmount = Convert.ToInt32(data["SpAddAmount"])
            };
            if (levelData != null)
            {
                _PlayerLevelData.Add(levelData);
            }
        }
    }

    public PlayerLevelData GetPlayerLevelData(int level)
    {
        return _PlayerLevelData.FirstOrDefault(data => data.Level == level);
    }
    #endregion

    #region 스킬 데이터테이블
    public void SkillDataTable(string dataPath, string skillDataTable)
    {
        var parsedSkillDataTable = CSVReader.Read($"{dataPath}/{skillDataTable}");
        foreach (var data in parsedSkillDataTable)
        {
            SkillData skillData = null;
            skillData = new SkillData
            {
                ID = Convert.ToInt32(data["ID"]),
                SkillName = data["SkillName"].ToString(),
                TargetScript = data["TargetScript"].ToString(),//사용할 스크립트
                IconPath = data["SkillIconPath"].ToString(),//이미지 경로
                SkillType = (SkillData.SkillTypes)Enum.Parse(typeof(SkillData.SkillTypes), data["Skilltype"].ToString()),
                StatType = (SkillData.StatTypes)Enum.Parse(typeof(SkillData.StatTypes), data["StatType"].ToString()),
                //SkillType = Convert.ToInt32(data["SkillType"]), // 타입들 어떻게 사용할지
                //StatType = Convert.ToInt32(data["StatType"]),   // enum으로?
                StatValue = Convert.ToInt32(data["StatValue"]),
                BaseDamage = Convert.ToInt32(data["BaseDamage"]),
                DamageValue = Convert.ToInt32(data["DamageValue"]),
                UseingMP = Convert.ToInt32(data["UseingMP"]),
                NeedSkillPoint = Convert.ToInt32(data["NeedSkillPoint"]),
                MaxLevel = Convert.ToInt32(data["MaxLevel"]),
            };
            if (skillData != null)
            {
                Logger.Log($"{skillData.StatType} 저장됨");
                _SkillData.Add(skillData);
            }
        }
    }

    public SkillData GetSkillData(int id)
    {
        return _SkillData.FirstOrDefault(data => data.ID == id);
    }
    #endregion

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
            };
            if (itemData != null)
            {
                //Logger.Log($"{itemData.ID} 저장됨");
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
                //회복
                Value = Convert.ToInt32(data["Value"]),
                //쿨타임
                CoolTime = Convert.ToInt32(data["CoolTime"]),
                //지속 시간
                DurationTime = Convert.ToInt32(data["DurationTime"]),
                //소지 개수
                MaxAmount = Convert.ToInt32(data["MaxAmount"]),
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
                //Type = (ItemData.ItemType)Enum.Parse(typeof(ItemData.ItemType), data["ItemType"].ToString()),
                Type = ItemData.ItemType.Booty,
                LimitLevel = Convert.ToInt32(data["LimitLv"]),
                BuyingPrice = Convert.ToInt32(data["BuyingPrice"]),
                SellingPrice = Convert.ToInt32(data["SellingPrice"]),
                //설명 텍스트
                FlavorText = data["FlavorText"].ToString(),
                MaxAmount = Convert.ToInt32(data["MaxAmount"]),
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
        var parsedDropDataTable = CSVReader.Read($"{dataPath}/{monsterDropTable}");
        foreach (var data in parsedDropDataTable)
        {
            DropData dropData = null;
            dropData = new DropData
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
            if (dropData != null)
            {
                Logger.Log($"{dropData} 저장됨");
                _MonsterDropData.Add(dropData);
            }
        }
    }
    #endregion

    #region 던전 데이터테이블 함수
    void DungeonDataTable(string dataPath, string dungeonDataTable)
    {

        var parsedDungeonDataTable = CSVReader.Read($"{dataPath}/{dungeonDataTable}");
        foreach (var data in parsedDungeonDataTable)
        {
            DungeonData dungeonData = null;
            dungeonData = new DungeonData
            {
                //인덱스
                Index = Convert.ToInt32(data["Index"]),
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //던전 이름
                DungeonName = data["DungeonName"].ToString(),
                //몬스터 타입1
                MonsterType1 = Convert.ToInt32(data["MonsterType1"]),
                //몬스터 타입2
                MonsterType2 = Convert.ToInt32(data["MonsterType2"]),
                //몬스터 타입3
                MonsterType3 = Convert.ToInt32(data["MonsterType3"]),
            };
            if (dungeonData != null)
            {
                Logger.Log($"{dungeonData} 저장됨");
                _DungeonData.Add(dungeonData);
            }
        }
    }
    #endregion

    #region 몬스터 데이터테이블 함수
    void MonsterDataTable(string dataPath, string monsterDataTable)
    {
        var parsedDungeonDataTable = CSVReader.Read($"{dataPath}/{monsterDataTable}");
        foreach (var data in parsedDungeonDataTable)
        {
            MonsterData monsterData = null;
            monsterData = new MonsterData
            {
                //인덱스
                Index = Convert.ToInt32(data["Index"]),
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //던전 이름
                MonsterName = data["MonsterName"].ToString(),
                //몬스터 타입1
                MinSpawn = Convert.ToInt32(data["MinSpawnCount"]),
                //몬스터 타입2
                MaxSpawn = Convert.ToInt32(data["MaxSpawnCount"]),
                //몬스터 타입3
                MonsterType = Convert.ToInt32(data["MonsterType"]),
            };
            if (monsterData != null)
            {
                Logger.Log($"{monsterData} 저장됨");
                _MonsterData.Add(monsterData);
            }
        }
    }
    #endregion

    #region 퀘스트 데이터테이블 함수
    void QuestDataTable(string dataPath, string questDataTable)
    {
        var parsedQuestDataTable = CSVReader.Read($"{dataPath}/{questDataTable}");

        foreach (var data in parsedQuestDataTable)
        {
            QuestData questData = null;

            questData = new QuestData
            {
                ID = Convert.ToInt32(data["ID"]),
                Type = (Define.QuestType)Enum.Parse(typeof(Define.QuestType), data["QuestType"].ToString()),
                Name = data["QuestName"].ToString(),
                Info = data["QuestInfo"].ToString(),
                PlayerLevelRequirement = Convert.ToInt32(data["Requirement"]),
                TargetID = Convert.ToInt32(data["TargetID"]),
                TargetCount = Convert.ToInt32(data["TargetCount"]),
                RewardType1 = (QuestData.RewardType)Enum.Parse(typeof(QuestData.RewardType), data["QuestRewardType1"].ToString()),
                RewardValue1 = Convert.ToInt32(data["QuestRewardValue1"]),
                RewardType2 = (QuestData.RewardType)Enum.Parse(typeof(QuestData.RewardType), data["QuestRewardType2"].ToString()),
                RewardValue2 = Convert.ToInt32(data["QuestRewardValue2"]),
                RewardType3 = Convert.ToInt32(data["QuestRewardType3"]),
                RewardValue3 = Convert.ToInt32(data["QuestRewardValue3"]),
            };
            //Logger.LogError($"{data["QuestRewardType3"].ToString()}이게 뭔값이냐");
            //Logger.LogError($"{ItemData.ItemType.Potion}타입은뭐냐");
            //if (data["QuestRewardType3"].ToString() == ItemData.ItemType.Potion.ToString())
            //{
                /*questData.RewardType3 = QuestData.RewardType.Potion;
                Logger.LogError($"{questData.RewardType3}1번확인");
                int potionID = Convert.ToInt32(data["QuestRewardValue3"]);
                Logger.LogError($"{potionID}2번확인");
                var potionItem = _PotionItemData.Find(p => p.ID == potionID);
                Logger.LogError($"{potionItem}3번확인");
                questData.RewardValue3 = potionItem?.ID ?? 0;
                Logger.LogError($"{questData.RewardValue3 }4번확인");
                if (potionItem == null)
                {
                    Logger.LogError($"포션ID{potionID}를 찾을 수 없습니다.");
                }*/
            //}
            _QuestData.Add(questData);
        }
    }
    #endregion

    #region 조합 데이터테이블 함수
    void FusionDataTable(string dataPath, string FusionDataTable)
    {
        var parsedDungeonDataTable = CSVReader.Read($"{dataPath}/{FusionDataTable}");
        foreach (var data in parsedDungeonDataTable)
        {
            FusionData fusionData = null;
            fusionData = new FusionData
            {
                FusionItemID1 = Convert.ToInt32(data["FusionItemID1"]),
                FusionItemAmount1 = Convert.ToInt32(data["FusionItemAmount1"]),
                FusionItemID2 = Convert.ToInt32(data["FusionItemID2"]),
                FusionItemAmount2 = Convert.ToInt32(data["FusionItemAmount2"]),
                ResultItemID = Convert.ToInt32(data["ResultItemID"]),

            };
            if (fusionData != null)
            {
                Logger.Log($"{fusionData} 저장됨");
                _FusionData.Add(fusionData);
            }
        }
    }
    #endregion

    #region 몬스터 스텟데이터테이블 함수
    void MonsterStatDataTable(string dataPath, string monsterStatDataTable)
    {
        var parsedDungeonDataTable = CSVReader.Read($"{dataPath}/{monsterStatDataTable}");
        foreach (var data in parsedDungeonDataTable)
        {
            MonsterStatData monsterStatData = null;
            monsterStatData = new MonsterStatData
            {
                //아이디
                ID = Convert.ToInt32(data["ID"]),
                //몬스터 이름
                Name = data["Name"].ToString(),
                //최대 체력
                MaxHp = Convert.ToInt32(data["MaxHp"]),
                //공격력
                ATK = Convert.ToInt32(data["ATK"]),
                //방어력
                DEF = Convert.ToInt32(data["DEF"]),
                //이동 속도
                MoveSpeed = Convert.ToInt32(data["MoveSpeed"]),
                //공격 속도
                AtkSpeed = Convert.ToInt32(data["AtkDelay"]),
                //체력 회복 량
                RecoveryHP = Convert.ToInt32(data["RecoveryHP"]),//
                //마나
                MP = Convert.ToInt32(data["MP"]),
                //최대 마나
                MaxMP = Convert.ToInt32(data["MaxMP"]),
                //마나 회복 량
                RecoveryMP = Convert.ToInt32(data["RecoveryMP"]),
                //추적 범위
                ChaseRange = Convert.ToInt32(data["ChaseRange"]),
                //복귀 범위
                ReturnRange = Convert.ToInt32(data["ReturnRange"]),
                //공격 범위
                AttackRange = Convert.ToInt32(data["AttackRange"]),
                //이동 범위
                AwayRange = Convert.ToInt32(data["AwayRange"]),
            };
            if (monsterStatData != null)
            {
                Logger.Log($"{monsterStatData} 저장됨");
                _MonsterStatData.Add(monsterStatData);
            }
        }
    }
    #endregion

    #region
    void ShopDataTable(string dataPath, string shopDataTable)
    {
        var parsedShopDataTable = CSVReader.Read($"{dataPath}/{shopDataTable}");

        ShopUIData shopUIData = new ShopUIData { _itemCode = new List<(int, int)>() };
        foreach (var data in parsedShopDataTable)
        {
            int itemId = Convert.ToInt32(data["ItemID"]);
            int itemCount = Convert.ToInt32(data["ItemCount"]);
            shopUIData._itemCode.Add((itemId, itemCount));
        }
        if(shopUIData != null)
        {
            _ShopUIData.Add(shopUIData);
        }
    }

    public ShopUIData GetShopData()
    {
        return _ShopUIData.FirstOrDefault();
    }
    #endregion
}