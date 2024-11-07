using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;



#region 플레이어 데이터 클래스

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
            var stat = player.GetOrAddComponent<PlayerStatManager>();
            //Logger.Log($"각 스텟 저장 전 확인 : {string.Join(", ", stat.MaxHP, stat.MaxMP, stat.HP, stat.MP)}");
            _level = stats.Level;
            _exp = stats.EXP;
            _maxExp = stats.MaxEXP;
            _sp = stats.SP;
            _gold = stats.Gold;
            _PlayerTypes = Managers.Game._playerType;
            Logger.Log($"현재 플레이 중인 타입 {_PlayerTypes}");
            string directory = Path.GetDirectoryName(_SavePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string playerJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, playerJson);
            //Logger.Log($"각 스텟 저장 후 확인 : {string.Join(", ", stat.MaxHP, stat.MaxMP, stat.HP, stat.MP)}");
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
            //Logger.Log("플레이어 데이터 로드 성공");
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
public class PlayerPosSetData
{
    public float _x;
    public float _y;
    public float _z;

    public static void PlayerPosSave(Vector3 pos)
    {
        PlayerPosSetData playerPosSet = new PlayerPosSetData();

        playerPosSet._x = pos.x;
        playerPosSet._y = pos.y;
        playerPosSet._z = pos.z;

        string playerPosJson = JsonUtility.ToJson(playerPosSet);

        File.WriteAllText($"{Application.persistentDataPath}/playerPosData.json", playerPosJson);
    }

    public static Vector3 PlayerPosSetLoad()
    {
        string path = $"{Application.persistentDataPath}/playerPosData.json";

        if (File.Exists(path))
        {
            string playerPosJson = File.ReadAllText(path);
            PlayerPosSetData savePos = JsonUtility.FromJson<PlayerPosSetData>(playerPosJson);
            return new Vector3(savePos._x, savePos._y, savePos._z);
        }
        else
        {
            //Logger.LogWarning($"저장된 위치가 없습니다 초기 위치로 보내드립니다");
            return Vector3.zero;
        }
    }
}

#endregion

#region 인벤토리 데이터 클래스
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
        try
        {
            if (_inventory == null)
            {
                Logger.LogWarning("인벤토리 널 값");
            }
            List<Inventory.ItemGroup> ItemGroup = new List<Inventory.ItemGroup>();
            foreach (var itemSaveType in Enum.GetValues(typeof(ItemData.ItemType)))
            {

                var itemType = (ItemData.ItemType)itemSaveType;
                if (itemType == ItemData.ItemType.DropData) continue;
                Inventory.ItemGroup itemGroup = _inventory.GetGroup((ItemData.ItemType)itemSaveType);
                if (ItemGroup.Contains(itemGroup)) { continue; }
                else { ItemGroup.Add(itemGroup); }
                //Logger.Log(ItemGroup.Count);
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
                        //Logger.Log($"{itemData}아이템 리스트에 들어가는지 확인");
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
            //Logger.LogError(invenJson);
            JsonUtility.FromJsonOverwrite(invenJson, this);
            //Logger.Log(_InvenItemList.Count);
            Logger.Log("인벤토리 데이터 로드");
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
        _InvenItemList.Clear();
        var player = Managers.Game._player;
        _inventory = player.GetOrAddComponent<Inventory>();

        if (_inventory == null)
        {
            player.AddComponent<Inventory>();
            Logger.LogError("인벤토리가 null 입니다.");
        }
    }
}
#endregion

#region 스킬 데이터 클래스
//딸각 로딩씬에서 켜준다음에 로드를 해주는방법으로 해보자
[Serializable]
public class SkillTreeItemData
{
    public int _id;
    public int _curLevel;
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
            List<SkillTreeItem> _skillTreeItem;
            SkillTree skillTree = Managers.UI.OpenUI<SkillTree>(new BaseUIData());
            _skillTreeItem = skillTree._skillTreeItems;

            skillTree.CloseUI();
            //Logger.Log(skillTree._skillTreeItems.Count);
            foreach (var skill in _skillTreeItem)
            {
                _skillTreeItemDatas.Add(new SkillTreeItemData
                {
                    _id = skill._skillId,
                    _curLevel = skill.SkillLevel,
                });
                //Logger.Log(_skillTreeItemDatas.Count);
            }
            string skillJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, skillJson);
            Logger.Log("스킬 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"저장할 스킬이 없습니다.{e.Message}");
        }
    }

    public bool LoadData()
    {
        SetDefaultData();
        try
        {
            string skillJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(skillJson, this);
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
#endregion

#region 장비 데이터 클래스
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
        string directory = Path.GetDirectoryName(_SavePath);

        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        foreach (var slot in _inventory.EquipMents)
        {
            if (slot.Value != null)
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
        SetDefaultData();
        try
        {
            string equipJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(equipJson, this);
            foreach (var equipment in _equipments)
            {
                var item = Item.ItemSpawn(equipment._id) as EquipmentItem;
                _inventory.EquipMents[equipment._slotName] = item;
            }
            Logger.Log("장비 데이터 로드");
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
        _equipments.Clear();
        _inventory = Managers.Game._player.GetComponent<Inventory>();
        var playerEquipUI = Managers.Game._player.GetComponent<Inventory>().EquipMents;
    }
}
#endregion

#region 메인UI퀵슬롯 데이터 클래스
[Serializable]
public class QuickItemSlotData
{
    public int _id;
    public int _slotIndex;
}

[Serializable]
public class QuickSkillSlotData
{
    public int _id;
    public int _slotIndex;
}

[Serializable]
public class QuickSlotSaveData : IData
{
    public List<QuickItemSlotData> _quickItemSlotData = new List<QuickItemSlotData>();

    public List<QuickSkillSlotData> _quickSkillSlotData = new List<QuickSkillSlotData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/QuickSlotSaveData.json";
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
            MainUI mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
            //메인UI가 널이 아니라면 
            if (mainUI != null)
            {
                QuickItemSlot[] quickItemSlots = mainUI.GetComponentsInChildren<QuickItemSlot>();

                for (int i = 0; i < quickItemSlots.Length; i++)
                {
                    QuickItemSlot quickItemSlot = quickItemSlots[i];
                    if (quickItemSlot.Item != null)
                    {
                        QuickItemSlotData quickSlotItemData = new QuickItemSlotData
                        {
                            _id = quickItemSlot.Item.Data.ID,
                            _slotIndex = i,
                        };
                        _quickItemSlotData.Add(quickSlotItemData);
                    }
                }

                //스킬트리아이템을 퀵슬롯에 등록을 했을테니까 그 정보를 리스트에 저장
                SkillQuickSlot[] skillQuickSlots = mainUI.GetComponentsInChildren<SkillQuickSlot>();
                List<SkillTreeItem> skillTreeItem;
                SkillTree skillTree = Managers.UI.OpenUI<SkillTree>(new BaseUIData());
                skillTreeItem = skillTree._skillTreeItems;
                skillTree.CloseUI();

                for (int i = 0; i < skillQuickSlots.Length; i++)
                {
                    SkillQuickSlot skillQuickSlot = skillQuickSlots[i];
                    if (skillQuickSlot.Skill != null)
                    {
                        //등록한 스킬하고 스킬트리에 있는 스킬이 같을경우 저장 그 스킬의 아이디를
                        //리스트에 저장
                        var matchingItem = skillTreeItem.Find(item => item.Skill == skillQuickSlot.Skill);

                        if (matchingItem != null)
                        {
                            QuickSkillSlotData quickSkillSlotData = new QuickSkillSlotData
                            {
                                _id = matchingItem._skillId,
                                _slotIndex = i
                            };
                            _quickSkillSlotData.Add(quickSkillSlotData);
                        }
                    }
                }
            }

            string quickSlotJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, quickSlotJson);
            Logger.Log("퀵슬롯 정보 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"MainQuickSlotUI를 찾을 수 없습니다{e.Message}");
        }
    }

    public bool LoadData()
    {
        SetDefaultData();
        try
        {
            string quickSlotJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(quickSlotJson, this);
            //확인했음
            //Logger.Log(_quickSkillSlotData.Count);
            Logger.Log("퀵슬롯 로드 성공");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"퀵슬롯데이터 로드 실패{e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        _quickItemSlotData.Clear();
        _quickSkillSlotData.Clear();
    }
}
#endregion

#region 퀘스트 데이터 클래스
[Serializable]
public class QuestItemData
{
    // 퀘스트 아이디
    public int _id;
    // 퀘스트 진행 값
    public int _progressInfo;
    // 퀘스트 진행 중 여부 (int로 저장, 0: 미진행, 1: 진행 중)
    public int _isProgress;
    // 퀘스트 완료 여부 (int로 저장, 0: 미완료, 1: 완료)
    public int _isFinished;
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
        SetDefaultData();

        var questManager = Managers.QuestManager;

        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            QuestUI acceptedQuestUI = Managers.UI.OpenUI<QuestUI>(new BaseUIData());
            acceptedQuestUI._questInput = Define.QuestInput.Q;

            if (acceptedQuestUI)
            {
                // 진행 중인 퀘스트들만 저장
                foreach (int questId in questManager._progressQuest)
                {
                    int progressInfo = 0;

                    // _countCheck에 해당 퀘스트 ID가 있는지 확인
                    if (!questManager._countCheck.TryGetValue(questId, out progressInfo))
                    {
                        // 키가 없으면 기본값 0으로 처리
                        progressInfo = 0;
                    }

                    var questData = new QuestItemData
                    {
                        _id = questId,
                        _progressInfo = progressInfo,
                        _isProgress = 1,
                        _isFinished = questManager._completeQuest.Contains(questId) ? 1 : 0,
                    };
                    _questItemData.Add(questData);
                }
                acceptedQuestUI.CloseUI();
            }
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
        SetDefaultData();
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
        _questItemData.Clear();
    }
}
#endregion

#region 라지맵 데이터 클래스

[Serializable]
public class LargeMapItemData
{
    public string sceneName;
    public byte[] fogTextureData;
}

[Serializable]
public class LargeMapData : IData
{
    public List<LargeMapItemData> _largeMapItemData = new List<LargeMapItemData>();

    string _SavePath;

    public void Init()
    {
        _SavePath = $"{Application.persistentDataPath}/LargeMapData.json";
    }

    public void SaveData()
    {
        SetDefaultData();
        LargeMapUI largeMapUI = Managers.UI.OpenUI<LargeMapUI>(new BaseUIData());
        largeMapUI.CloseUI();
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        foreach (var sceneFog in largeMapUI._sceneFogTextures)
        {
            Texture2D texture = sceneFog.Value;
            //Texture2D를 PNG 형식으로 변환하여 저장하는 법
            byte[] textureData = texture.EncodeToPNG();
            _largeMapItemData.Add(new LargeMapItemData
            {
                sceneName = sceneFog.Key,
                fogTextureData = textureData,
            });
        }
        try
        {
            string mapJson = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, mapJson);
            Logger.Log("맵 정보 세이브");
        }
        catch (Exception e)
        {
            Logger.LogError($"저장할 맵이 없습니다.{e.Message}");
        }
    }
    public bool LoadData()
    {
        SetDefaultData();
        string directory = Path.GetDirectoryName(_SavePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        try
        {
            string mapJson = File.ReadAllText(_SavePath);
            JsonUtility.FromJsonOverwrite(mapJson, this);
            Logger.Log("맵 로드 성공");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"로드 할 맵이 없습니다{e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        _largeMapItemData.Clear();
    }
}
#endregion





#region 기본 데이터 클래스
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
//    //퀘스트 초기 값 저장 데이터
//    public List<QuestSaveData> _QuestSaveData;

//    string _SavePath;

//    public void Init()
//    {
//        _SavePath = $"{Application.persistentDataPath}/SaveDatas.json";

//        if (string.IsNullOrEmpty(_SavePath))
//        {
//            return;
//        }
//        string directory = Path.GetDirectoryName(_SavePath);
//        if (!Directory.Exists(directory))
//        {
//            Directory.CreateDirectory(directory);
//        }
//        _InventorySaveDatas = new List<InventorySaveData>();
//        _SkillSaveDatas = new List<SkillSaveData>();
//        _EquipmentSaveDatas = new List<EquipmentSaveData>();
//        _QuestSaveData = new List<QuestSaveData>();
//        SetDefaultData();
//        Logger.Log($"저장 경로 확인 {_SavePath}");
//    }

//    public void SaveData()
//    {
//        try
//        {
//            _InventorySaveDatas ??= new List<InventorySaveData>();
//            _SkillSaveDatas ??= new List<SkillSaveData>();
//            _QuestSaveData ??= new List<QuestSaveData>();
//            _EquipmentSaveDatas ??= new List<EquipmentSaveData>();
//            string directory = Path.GetDirectoryName(_SavePath);
//            if (!Directory.Exists(directory))
//            {
//                Directory.CreateDirectory(directory);
//            }
//            string json = JsonUtility.ToJson(this, true);
//            File.WriteAllText(_SavePath, json);
//        }
//        catch (Exception e)
//        {
//            Logger.LogError($"데이터 저장 실패: {e.Message}");
//        }
//    }

//    public bool LoadData()
//    {
//        try
//        {
//            string json = File.ReadAllText(_SavePath);
//            JsonUtility.FromJsonOverwrite(json, this);
//            Logger.Log("게임 데이터 로드 성공");
//            return true;
//        }
//        catch (Exception e)
//        {
//            Logger.LogError($"데이터 로드 실패: {e.Message}");
//            return false;
//        }
//    }

//    public void SetDefaultData()
//    {
//        // 초기 데이터 세팅
//        _InventorySaveDatas = new List<InventorySaveData> { new InventorySaveData() };
//        _QuestSaveData = new List<QuestSaveData> { new QuestSaveData() };
//        _EquipmentSaveDatas = new List<EquipmentSaveData> { new EquipmentSaveData() };
//        Logger.Log("기본 데이터 설정 완료");
//    }
//}
#endregion