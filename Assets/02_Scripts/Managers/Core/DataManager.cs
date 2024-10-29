using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict {  get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        //StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        InitializeGameState();
        // Json을 사용하기 위한 타입은 TextAsset
        //TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/StatData");
        //StatData data = JsonUtility.FromJson<StatData>(textAsset.text);

        //StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();

        /*
        foreach ( Stat stat in data.stats )
        {
            StatDict.Add(stat.level, stat);
        }
        */
    }

    void InitializeGameState()
    {
        SaveDatas saveDatas = new SaveDatas();
        PlayerSaveData saveplayerData = new PlayerSaveData();
        Logger.Log("데이터 저장 Init");
        saveDatas.SetDefaultData();
        saveDatas.SaveData();
        saveplayerData.SetDefaultData();
        saveplayerData.SaveData();
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}

