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

    static readonly string _SavePath = $"{Application.persistentDataPath}/SaveData.json";

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
        _InventorySaveDatas = new List<InventorySaveData>();
        _SkillSaveDatas = new List<SkillSaveData>();
        _EquipmentSaveDatas = new List<EquipmentSaveData>();
        _PlayerTypes =  Managers.Game._playerType;
        Logger.Log("새로하기 되었습니다.");
    }
}

public class PlayerSaveData : IData
{
    int x;
    int y;
    int z;
    int _level;
    int _exp;
    int _maxExp;
    int _sp;
    int _gold;

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

public class InventorySaveData : IData
{
    int _id;
    int _index;
    int _amount;

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

public class SkillSaveData : IData
{
    string _name;
    int _level;
    int _type;

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

public class EquipmentSaveData : IData
{
    int _id;
    int _type;

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

public class QuestSaveData : IData
{
    string _name;
    int _amount1;
    int _amount2;

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
