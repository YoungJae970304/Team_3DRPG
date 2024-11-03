using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public class SaveDatas : IData
{
    //인벤토리 저장 데이터
    public List<InventorySaveData> _InventorySaveDatas;
    //스킬 저장 데이터
    public List<SkillSaveData> _SkillSaveDatas;
    //장비 저장 데이터
    public List<EquipmentSaveData> _EquipmentSaveDatas;
    //플레이어 초기 값 저장 데이터
    public List<PlayerSaveData> _PlayerSaveDatas;
    //퀘스트 초기 값 저장 데이터
    public List<QuestSaveData> _QuestSaveData;
    //선택한 플레이어타입
    public Define.PlayerType _PlayerTypes;

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/SaveDatas.json";

        if (string.IsNullOrEmpty(_SavePath))
        {
            return;
        }
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        _InventorySaveDatas = new List<InventorySaveData>();
        _SkillSaveDatas = new List<SkillSaveData>();
        _EquipmentSaveDatas = new List<EquipmentSaveData>();
        _PlayerSaveDatas = new List<PlayerSaveData>();
        _QuestSaveData = new List<QuestSaveData>();
        SetDefaultData();
        Logger.Log($"저장 경로 확인 {_SavePath}");
    }

    public void SaveData()
    {
        try
        {
            _PlayerSaveDatas ??= new List<PlayerSaveData>();
            _InventorySaveDatas ??= new List<InventorySaveData>();
            _SkillSaveDatas ??= new List<SkillSaveData>();
            _QuestSaveData ??= new List<QuestSaveData>();
            _EquipmentSaveDatas ??= new List<EquipmentSaveData>();
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, json);
        }
        catch (Exception e)
        {
            Logger.LogError($"데이터 저장 실패: {e.Message}");
        }
    }

    public bool LoadData()
    {
        try
        {
            string json = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(json, this);
            Logger.Log("게임 데이터 로드 성공");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"데이터 로드 실패: {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        // 초기 데이터 세팅
        _InventorySaveDatas = new List<InventorySaveData> { new InventorySaveData() };
        _PlayerSaveDatas = new List<PlayerSaveData> { new PlayerSaveData() };
        _QuestSaveData = new List<QuestSaveData> { new QuestSaveData() };
        _EquipmentSaveDatas = new List<EquipmentSaveData> { new EquipmentSaveData() };
        _PlayerTypes = Managers.Game._playerType;
        Logger.Log($"현재 플레이어 타입: {_PlayerTypes}");
        Logger.Log("기본 데이터 설정 완료");
    }
}

[Serializable]
public class PlayerSaveData : IData
{
    //플레이어 레벨
    public int _level;
    //플레이어 현재 경험치
    public int _exp;
    //플레이어 최대 경험치
    public int _maxExp;
    //플레이어 스킬포인트
    public int _sp;
    //현재 골드
    public int _gold;
    float _x;
    float _y;
    float _z;
    //선택한 플레이어타입
    public Define.PlayerType _PlayerTypes;

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/SavePlayerData.json";
    }

    public void SaveData()
    {
        try
        {
            var stats = Managers.Game._player._playerStatManager;
            var player = Managers.Game._player;
            _level = stats.Level;
            _exp = stats.EXP;
            _maxExp = stats.MaxEXP;
            _sp = stats.SpAddAmount;
            _gold = stats.Gold;
            _x = player.transform.position.x;
            _y = player.transform.position.y;
            _z = player.transform.position.z;
            _PlayerTypes = Managers.Game._playerType;
            Logger.Log($"현재 플레이 중인데 타입 {_PlayerTypes}");
            Logger.Log($"현재 플레이어 저장 위치 확인{_x}{_y}{_z}");

            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string playerJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, playerJson);

            Logger.Log("플레이어 데이터 저장");
        }
        catch (Exception e)
        {
            Logger.LogError($"플레이어 데이터 저장 실패: {e.Message}");
        }
    }

    public bool LoadData()
    {
        try
        {
            string playerJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(playerJson, this);
            Logger.Log("플레이어 데이터 로드 성공");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"플레이어 데이터로드 실패: {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {

    }
}

[Serializable]
public class InventoryItemData
{
    //얻은 아이템의 id
    public int _id;
    //얻었던 아이템의 갯수(포션, 기타아이템 등, 장비는 1개 가 99개임 1칸에 Max99해놓았음)
    public int _amount;
    //인덱스
    public int _index;
    //타입
    public int _type;
}

[Serializable]
public class InventorySaveData : IData
{
    public List<InventoryItemData> _InvenItemList = new List<InventoryItemData>();

    Inventory _inventory;

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/InvenSaveData.json";
        
        //PubAndSub.Subscrib("InvenSave", SaveData);
    }

    public void SaveData()
    {
        SetDefaultData();
        _InvenItemList.Clear();
        try
        {
            if (_inventory == null)
            {
                Logger.LogWarning("인벤토리 널 값");
            }
            foreach (var itemSaveType in Enum.GetValues(typeof(ItemData.ItemType)))
            {
                var itemType = (ItemData.ItemType)itemSaveType;
                if (itemType == ItemData.ItemType.DropData) continue;
                int maxGroupSize = _inventory.GetGroupSize(itemType);

                for (int index = 0; index < maxGroupSize; index++)
                {
                    var item = _inventory.GetItem(index, itemType);
                    if (item != null)
                    {
                        InventoryItemData itemData = new InventoryItemData
                        {
                            _id = item.Data.ID,
                            _amount = item is CountableItem countableItem ? countableItem.GetCurrentAmount() : 1,
                            _index = index,
                            _type = (int)itemType,
                        };
                        Logger.Log($"{itemData}아이템 리스트에 들어가는지 확인");
                        _InvenItemList.Add(itemData);
                    }
                }
            }
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory) && directory != null)
            {
                Directory.CreateDirectory(directory);
            }

            string invenJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, invenJson);
            Logger.Log("인벤토리 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"인벤토리 세이브 실패 : .{e.Message}");
        }
    }

    public bool LoadData()
    {
        SetDefaultData();
        try
        {
            string invenJson = File.ReadAllText(_SavePath);
            Logger.LogError(invenJson);
            JsonUtility.FromJsonOverwrite(invenJson, this);
            foreach (var itemData in _InvenItemList)
            {

                //아이템 검증
                Item newSaveItem = Item.ItemSpawn(itemData._id, itemData._amount);

                if (newSaveItem == null)
                {
                    Logger.LogWarning($"유효하지 않은 아이템 ID: {itemData._id}");

                    continue;
                }
                
                // 빈 슬롯인지 확인 후 아이템 설정
                if (_inventory.GetItem(itemData._index, newSaveItem.Data.Type) == null)
                {
                    //Logger.LogError("543");
                    _inventory.Setitem(itemData._index, newSaveItem);
                }
                else
                {
                    Logger.LogWarning($"인벤토리 인덱스 {itemData._index}에 이미 아이템이 존재합니다.");
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"인벤토리 아이템 데이터 로드 실패: {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        var player = Managers.Game._player;
        _inventory = player.GetComponent<Inventory>();
        
        if (_inventory == null)
        {
            player.AddComponent<Inventory>();
            Logger.LogError("인벤토리가 null 입니다.");
        }
    }
}

[Serializable]
public class SkillSaveData : IData
{
    public List<SkillData> _skills = new();
    public List<SkillData> _quickSlotSkills = new();
    public List<int> _skillAddID = new();
    public List<int> _skillRemoveID = new();
    string _SavePath;
    SkillBase _skill;
    public SkillBase Skill { get => _skill; }
    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/SkillSaveData.json";
    }

    public void SaveData()
    {
        //SetDefaultData();
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            Logger.Log($"{_skillAddID.Count}개 추가할 스킬 ID 확인");
            foreach (int skillId in _skillAddID)
            {
                Logger.Log($"추가할 스킬 ID: {skillId}");
                AddSkill(skillId);
                AddQuickSlotSkill(skillId);
            }
            Logger.Log($"{_skills.Count}개 현재 스킬 목록 확인");
            Logger.Log($"{_quickSlotSkills.Count}개 현재 퀵슬롯 스킬 목록 확인");
            foreach (int skillId in _skillRemoveID)
            {
                Logger.Log($"제거할 스킬 ID: {skillId}");
                PubAndSub.Publish<SkillBase>("QuickSlotRemove", Skill);
            }
            var skillSave = new SaveSkillDataContainer
            {
                _Skills = _skills,
                _QuickSlotSkills = _quickSlotSkills
            };
            string skillJson = JsonUtility.ToJson(skillSave, true);
            File.WriteAllText(_SavePath, skillJson);
            Logger.Log($"스킬 세이브{_skills.Count}개, {_quickSlotSkills.Count}개");
        }
        catch (Exception e)
        {
            Logger.LogError($"저장할 스킬이 없습니다.{e.Message}");
        }
    }

    public bool LoadData()
    {
        try
        {
            string skillJson = File.ReadAllText(_SavePath);
            var saveSkillData = JsonUtility.FromJson<SaveSkillDataContainer>(skillJson);
            _skills = saveSkillData._Skills;
            _quickSlotSkills = saveSkillData._QuickSlotSkills;
            Logger.Log("스킬 데이터 로드");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"스킬 데이터 로드 실패 : {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        _skills.Clear();
        _skills.AddRange(Managers.DataTable._SkillData);
        _quickSlotSkills.Clear();
    }

    [Serializable]
    private class SaveSkillDataContainer
    {
        public List<SkillData> _Skills;
        public List<SkillData> _QuickSlotSkills;
    }
    public void AddSkill(int skillId)
    {
        // 이미 존재하는지 확인
        if (!_skills.Exists(skill => skill.ID == skillId))
        {
            SkillData skill = Managers.DataTable.GetSkillData(skillId);
            if (skill != null)
            {
                _skills.Add(skill);
                Logger.Log($"스킬 추가{skill.ID} - {skill.SkillName}");
            }
        }
    }
    public void AddQuickSlotSkill(int skillId)
    {
        if (!_quickSlotSkills.Exists(skill => skill.ID == skillId))
        {
            // SkillData를 데이터 테이블에서 가져오기
            SkillData skill = Managers.DataTable.GetSkillData(skillId);
            if (skill != null)
            {
                _quickSlotSkills.Add(skill);
                Logger.Log($"퀵슬롯 등록{skill.ID} - {skill.SkillName}");
            }
        }
    }
    public void RemoveQuickSlotSkill(int skillId)
    {
        // 해당 ID의 스킬이 있는지 확인 후 제거
        SkillData skillToRemove = _quickSlotSkills.Find(skill => skill.ID == skillId);
        if (skillToRemove != null)
        {
            _quickSlotSkills.Remove(skillToRemove);
            Logger.Log($"퀵슬롯 해제{skillToRemove.ID} - {skillToRemove.SkillName}");
        }
    }
}

[Serializable]
public class EquipmentSaveData : IData
{
    public List<EquipmentItemData> _equipmentItemDatas = new List<EquipmentItemData>();
    public List<PotionItemData> _potionItemDatas = new List<PotionItemData>();
    public List<GoodsItemData> _goodsItemData = new List<GoodsItemData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/EquipSaveData.json";
    }

    public void SaveData()
    {
        SetDefaultData();
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            string equipJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, equipJson);
            Logger.Log("아이템 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"저장할 아이템이 없습니다.{e.Message}");
        }
    }

    public bool LoadData()
    {
        try
        {
            string equipJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(equipJson, this);
            Logger.Log("아이템 데이터 로드");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"아이템 데이터 로드 실패 : {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        _equipmentItemDatas.AddRange(Managers.DataTable._EquipeedItemData);
        _potionItemDatas.AddRange(Managers.DataTable._PotionItemData);
        _goodsItemData.AddRange(Managers.DataTable._GoodsItemData);
    }
}

[Serializable]
public class QuestSaveData : IData
{
    public List<QuestData> _questDatas = new List<QuestData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/QuestSaveData.json";
    }

    public void SaveData()
    {
        SetDefaultData();
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            string questJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, questJson);
            Logger.Log("퀘스트 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"저장할 퀘스트 없습니다.{e.Message}");
        }
    }

    public bool LoadData()
    {
        try
        {
            string questJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(questJson, this);
            Logger.Log("퀘스트 데이터 로드");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"퀘스트 데이터 로드 실패 : {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        _questDatas.AddRange(Managers.DataTable._QuestData);
    }
}