using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterDataListWrapper
{
    public List<MonsterData> MonsterDataList = new List<MonsterData>();
}

public class MonsterData
{
    [SerializeField] int _index;
    [SerializeField] int _id;
    [SerializeField] string _monsterName;
    [SerializeField] int _minSpawn;
    [SerializeField] int _maxSpawn;
    [SerializeField] int _monsterType;

    public int Index;
    public int ID;
    public string MonsterName;
    public int MinSpawn;
    public int MaxSpawn;
    public int MonsterType;

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");
        bool result = false;
        try
        {
            string key = "MonsterData_" + ID;
            string monsterDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, monsterDataJson);
            PlayerPrefs.Save();
            result = true;

        }
        catch (Exception e)
        {
            Logger.Log($"Load failed (" + e.Message + ")");
        }
        return result;
    }

    public bool SaveData()
    {
        Logger.Log($"{GetType()}::Save Data");

        bool result = false;
        try
        {
            string key = "MonsterData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string monsterDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(monsterDataJson, this);
                result = true;
            }
            else
            {
                Logger.LogWarning("저장된 데이터가 없음");
            }
        }
        catch (Exception e)
        {
            Logger.Log("Save failed(" + e.Message + ")");
        }
        return result;
    }

    public void SetDefaultData()
    {
        Index = _index;
        ID = _id;
        MonsterName = _monsterName;
        MinSpawn = _minSpawn;
        MaxSpawn = _maxSpawn;
        MonsterType = _monsterType;

    }
}
