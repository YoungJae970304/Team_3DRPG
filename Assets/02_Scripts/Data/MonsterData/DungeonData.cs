using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DungeonDataListWrapper
{
    public List<DungeonData> DungeonDataList = new List<DungeonData>();
}
[Serializable]
public class DungeonData
{
    [SerializeField] int _index;
    [SerializeField] int _id;
    [SerializeField] string _dungeonName;
    [SerializeField] int _monsterType1;
    [SerializeField] int _monsterType2;
    [SerializeField] int _monsterType3;

    public int Index;
    public int ID;
    public string DungeonName;
    public int MonsterType1;
    public int MonsterType2;
    public int MonsterType3;

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");
        bool result = false;
        try
        {
            string key = "DungeonData_" + ID;
            string dungeonDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, dungeonDataJson);
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
            string key = "DungeonData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string dungeonDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(dungeonDataJson, this);
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
        DungeonName = _dungeonName;
        MonsterType1 = _monsterType1;
        MonsterType2 = _monsterType2;
        MonsterType3 = _monsterType3;
        
    }
}
