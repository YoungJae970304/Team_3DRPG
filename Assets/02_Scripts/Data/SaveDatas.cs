using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
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

//딸각 로딩씬에서 켜준다음에 로드를 해주는방법으로 해보자
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

//딸각 로딩씬에서 켜준다음에 로드를 해주는방법으로 해보자
public class SkillTreeItemData
{
    public int _id;
    public int _curLevel;
    public int _maxLevel;
    public string _slotName;
}

[Serializable]
public class SkillSaveData : IData
{
    public List<SkillTreeItemData> _skillTreeItemDatas = new List<SkillTreeItemData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/SkillSaveData.json";
    }

    public void SaveData()
    {
        SetDefaultData();
        //Json파일 생성하고
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            _skillTreeItemDatas.Clear();
            foreach (var skill in Managers.DataTable._SkillData)
            {
                _skillTreeItemDatas.Add(new SkillTreeItemData
                {
                    _id = skill.ID,
                    _curLevel = skill.MaxLevel,
                    _maxLevel = skill.MaxLevel,
                    _slotName = skill.SkillName,
                });
            }
            string skillJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, skillJson);
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
            JsonUtility.FromJsonOverwrite(skillJson, this);
            foreach(var data  in _skillTreeItemDatas)
            {
                var skill = Managers.DataTable.GetSkillData(data._id);
                if(skill != null)
                {
                    skill.MaxLevel = data._curLevel;
                    skill.MaxLevel = data._maxLevel;
                    skill.SkillName = data._slotName;
                }
            }
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
        _skillTreeItemDatas.Clear();
    }
}


//딸각 로딩씬에서 켜준다음에 로드를 해주는방법으로 해보자
[Serializable]
public class EquipnetItemData
{
    public int _id;
    public string _slotName;
}

[Serializable]
public class EquipmentSaveData : IData
{
    public List<EquipnetItemData> _equipments = new List<EquipnetItemData>();

    Inventory _inventory;

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/EquipSaveData.json";
    }

    public void SaveData()
    {
        SetDefaultData();
        _equipments.Clear();
        string directory = Path.GetDirectoryName(_SavePath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        foreach(var slot in _inventory.EquipMents)
        {
            if(slot.Value != null)
            {
                _equipments.Add(new EquipnetItemData
                {
                    _id = slot.Value.Data.ID,
                    _slotName = slot.Key
                });
            }
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
            foreach (var equipment in _equipments)
            {
                var item = Item.ItemSpawn(equipment._id) as EquipmentItem;
                _inventory.EquipMents[equipment._slotName] = item;
            }
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
        _inventory = Managers.Game._player.GetComponent<Inventory>();
        var playerEquipUI = Managers.Game._player.GetComponent<Inventory>().EquipMents;
    }
}


[Serializable]
public class QuestItemData
{
    //퀘스트 데이터의 ID
    public int _id;
    //현재 진행중인 퀘스트의 이름
    public string _questName;
    //현재 진행중인 퀘스트의 카운트값
    public int _currCount;
}

[Serializable]
public class QuestSaveData : IData
{
    public List<QuestItemData> _questItemData = new List<QuestItemData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/QuestSaveData.json";
    }

    public void SaveData()
    {
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

    }
}