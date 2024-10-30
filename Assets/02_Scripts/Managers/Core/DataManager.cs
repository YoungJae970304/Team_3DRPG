using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();


    public SaveDatas _saveDatas = new SaveDatas();
    public InventorySaveData _inventorySaveData = new InventorySaveData();
    public PlayerSaveData _playerSaveData = new PlayerSaveData();

    public void Init()
    {
        InitializeGameState();
        _saveDatas.Init();
        _inventorySaveData.Init();
        _playerSaveData.Init();
        //StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
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
        SaveData<SaveDatas>();
        Logger.Log("처음 시작 데이터 저장");
    }


    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public void SaveData<T>() where T : class, IData
    {
        T dataToSave = GetData<T>();
        if (dataToSave != null)
        {
            bool success = dataToSave.SaveData();
            if (success)
            {
                Logger.Log($"{typeof(T).Name} 저장");
            }
            else
            {
                Logger.LogError($"{typeof(T).Name} 저장 실패");
            }
        }
        else
        {
            Logger.LogWarning($"{typeof(T).Name} 저장할 데이터가 없음");
        }
    }

    public void LoadData<T>() where T : class, IData
    {
        T dataToLoad = GetData<T>();
        if (dataToLoad != null)
        {
            bool success = dataToLoad.LoadData();
            if (success)
            {
                Logger.Log($"{typeof(T).Name} 로드");
            }
            else
            {
                Logger.LogError($"{typeof(T).Name} 로드 실패");
            }
        }
        else
        {
            Logger.LogWarning($"{typeof(T).Name} 로드할 데이터가 없음");
        }
    }

    T GetData<T>() where T : class, IData
    {
        if(typeof(T) == typeof(SaveDatas))
        {
            if(_saveDatas == null)
            {
                _saveDatas = new SaveDatas();
            }
            return _saveDatas as T;
        }else if (typeof(T) == typeof(InventorySaveData))
        {
            if (_inventorySaveData == null)
            {
                _inventorySaveData = new InventorySaveData();
            }

            return _inventorySaveData as T;
        }
        else if (typeof(T) == typeof(PlayerSaveData))
        {
            if (_playerSaveData == null)
            {
                _playerSaveData = new PlayerSaveData();
            }

            return _playerSaveData as T;
        }
        return null;
    }

    void LoadedData<T>(List<T> values)
    {
        foreach(var value in values)
        {
            if(value is PlayerSaveData playerData)
            {
                _playerSaveData = playerData;
                Logger.Log("플레이어 데이터 적용");
            }else if (value is InventorySaveData inventorySaveData)
            {
                _inventorySaveData = inventorySaveData;
                Logger.Log("인벤토리 데이터 적용");
            }
        }
    }
}