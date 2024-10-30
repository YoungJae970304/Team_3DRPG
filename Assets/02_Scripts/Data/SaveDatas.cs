using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

//[Serializable]
//public class SaveDatas : IData
//{
//    //인벤토리 저장 데이터
//    public List<InventorySaveData> _InventorySaveDatas;
//    //스킬 저장 데이터
//    public List<SkillSaveData> _SkillSaveDatas;
//    //장비 저장 데이터
//    public List<EquipmentSaveData> _EquipmentSaveDatas;
//    //플레이어 초기 값 저장 데이터
//    public List<PlayerSaveData> _PlayerSaveDatas;
//    //public List<QuestSaveData> _QuestSaveData;
//    //선택한 플레이어타입
//    public Define.PlayerType _PlayerTypes;

//    static readonly string _SavePath = $"{Application.dataPath}Data/SaveDatas.json";

//    public bool LoadData()
//    {
//        if (!File.Exists(_SavePath))
//        {
//            Logger.LogWarning("저장된 데이터가 없습니다.");
//            return false;
//        }
//        try
//        {
//            string json = File.ReadAllText(_SavePath);
//            JsonUtility.FromJsonOverwrite(json, this);
//            if (_InventorySaveDatas == null) _InventorySaveDatas = new List<InventorySaveData>();
//            if (_PlayerSaveDatas == null) _PlayerSaveDatas = new List<PlayerSaveData>();
//            Logger.Log("인벤토리 로드");
//            Logger.Log("게임 데이터 로드 성공");
//            return true;
//        }
//        catch (Exception e)
//        {
//            Logger.LogError($"데이터 로드 실패: {e.Message}");
//            return false;
//        }
//    }

  
//    public bool SaveData()
//    {
//        try
//        {
//            string directory = Path.GetDirectoryName(_SavePath);
//            if (!Directory.Exists(directory))
//            {
//                Directory.CreateDirectory(directory);
//            }
//            string json = JsonUtility.ToJson(this, true);
//            File.WriteAllText(_SavePath, json);
//            //초기인벤토리
//            _InventorySaveDatas = new List<InventorySaveData>();
//            //처음 플레이어의 정보
//            PlayerSaveData playerData = new PlayerSaveData();
//            _PlayerSaveDatas = new List<PlayerSaveData> { playerData };
//            //선택한 플레이어의 타입
//            _PlayerTypes = Managers.Game._playerType;
//            Logger.Log($"현재 {_PlayerTypes} 타입 입니다.");
//            Logger.Log("처음부터 시작 되었습니다.");
//            Logger.Log("게임 데이터 저장 성공");
//            return true;
//        }
//        catch (Exception e)
//        {
//            Logger.LogError($"데이터 저장 실패: {e.Message}");
//            return false;
//        }
//    }

//    public void SetDefaultData()
//    {
//    }
//}

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
    static readonly string _SavePath = $"{Application.dataPath}Data/SavePlayerData.json";

    public bool SaveData()
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
            //현재 0 1 0 으로 저장되는데 실제 위치는 뭐 600어쩌구임;;
            Logger.Log($"현재 플레이어 저장 위치 확인{_x}{_y}{_z}");
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string playerJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, playerJson);

            Logger.Log("플레이어 데이터 저장");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"플레이어 데이터 저장 실패: {e.Message}");
            return false;
        }
    }

    public bool LoadData()
    {
        if (!File.Exists(_SavePath))
        {
            Logger.LogWarning("저장된 플레이어의 데이터가 없습니다.");
            return false;
        }

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

    public Inventory _inventory;

    static readonly string _SavePath = $"{Application.dataPath}/Data/InvenSaveData.Json";

    public bool SaveData()
    {
        try
        {
            _InvenItemList.Clear();
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            foreach (var itemSaveType in Enum.GetValues(typeof(ItemData.ItemType)))
            {
                var itemType = (ItemData.ItemType)itemSaveType;
                int _maxGroupSize = _inventory.GetGroupSize(itemType);
                for (int index = 0; index < _maxGroupSize; index++)
                {
                    var item = _inventory.GetItem(index, itemType);
                    if (item != null)
                    {
                        InventoryItemData itemData = new InventoryItemData
                        {
                            _id = item.Data.ID,
                            _amount = item is CountableItem ? ((CountableItem)item).GetCurrentAmount() : 1,
                            _index = index,
                            _type = (int)itemType
                        };
                        _InvenItemList.Add(itemData);
                    }
                }
            }
            string invenJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, invenJson);
            Logger.Log("인벤토리 세이브");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"저장된 아이템이 없습니다.{e.Message}");
            return false;
        }
    }

    public bool LoadData()
    {
        if (!File.Exists(_SavePath))
        {
            Logger.LogWarning("저장된 아이템이 없습니다.");
            return false;
        }
        try
        {
            string invenJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(invenJson, this);
            foreach (var itemData in _InvenItemList)
            {
                //아이템 검증
                Item newSaveItem = _inventory.GetItemToId(itemData._id);
                if (newSaveItem == null)
                {
                    Logger.LogWarning($"유효하지 않은 아이템 ID: {itemData._id}");
                    
                    continue;
                }

                if (newSaveItem is CountableItem countableItem)
                {
                    countableItem.AddAmount(itemData._amount);
                }

                // 빈 슬롯인지 확인 후 아이템 설정
                if (_inventory.GetItem(itemData._index, (ItemData.ItemType)itemData._type) == null)
                {
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
        _inventory = player.GetOrAddComponent<Inventory>();
    }
}

[Serializable]
public class SkillSaveData : IData
{
    public string _name;
    public int _level;
    public int _type;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class EquipmentSaveData : IData
{
    public int _id;
    public int _type;

    static readonly string _SavePath = $"{Application.dataPath}Data/SaveEquipData.json";

    public bool SaveData()
    {
        try
        {
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string playerJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, directory);

            Logger.Log("플레이어 데이터 저장");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"플레이어 데이터 저장 실패: {e.Message}");
            return false;
        }
    }

    public bool LoadData()
    {
        if (!File.Exists(_SavePath))
        {
            Logger.LogWarning("저장된 플레이어의 데이터가 없습니다.");
            return false;
        }

        try
        {
            string equipJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(equipJson, this);
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
public class QuestSaveData : IData
{
    public string _name;
    public int _amount1;
    public int _amount2;

    public bool SaveData()
    {
        throw new NotImplementedException();
    }

    public bool LoadData()
    {
        throw new NotImplementedException();
    }

    public void SetDefaultData()
    {
        throw new NotImplementedException();
    }
}
