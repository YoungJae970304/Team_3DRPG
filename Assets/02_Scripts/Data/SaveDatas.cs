using Data;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveDatas : IData
{
    public List<InventorySaveData> _InventorySaveDatas;
    public List<SkillSaveData> _SkillSaveDatas;
    public List<EquipmentSaveData> _EquipmentSaveDatas;
    public List<PlayerSaveData> _PlayerSaveDatas;
    //public List<QuestSaveData> _QuestSaveData;
    //[SerializeField] Transform _PlayerPosition;
    public Define.PlayerType _PlayerTypes;

    static readonly string _SavePath = $"{Application.dataPath}Data/SaveDatas.json";

    public bool LoadData()
    {
        if (!File.Exists(_SavePath))
        {
            Logger.LogWarning("저장된 데이터가 없습니다.");
            return false;
        }

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

    public bool SaveData()
    {
        try
        {
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(_SavePath, json);
            Logger.Log("게임 데이터 저장 성공");
            return true;
        }
        catch (Exception e)
        {
            Logger.LogError($"데이터 저장 실패: {e.Message}");
            return false;
        }
    }

    public void SetDefaultData()
    {
        //초기인벤토리
        _InventorySaveDatas = new List<InventorySaveData>();
        //초기 스킬
        _SkillSaveDatas = new List<SkillSaveData>();
        //초기 장비
        _EquipmentSaveDatas = new List<EquipmentSaveData>();
        //처음 플레이어의 정보
        PlayerSaveData playerData = new PlayerSaveData();
        _PlayerSaveDatas = new List<PlayerSaveData> { playerData};
        //선택한 플레이어의 타입은?
        _PlayerTypes =  Managers.Game._playerType;
        Logger.Log("처음부터 시작 되었습니다.");
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
    //플레이어 초기 위치 저장
    public int x;
    public int y;
    public int z;

    static readonly string _SavePath = $"{Application.dataPath}Data/SavePlayerData.json";

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
public class InventorySaveData : IData
{
    public int _id;
    public int _index;
    public int _amount;

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
