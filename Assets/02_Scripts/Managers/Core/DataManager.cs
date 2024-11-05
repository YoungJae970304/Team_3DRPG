using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    //public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    public Dictionary<Type, IData> _IDataDict = new Dictionary<Type, IData>();

    public void Init()
    {
        InitializeGameState();
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

    void DataTypes()
    {
        //IData를 상속받고있는 타입 설정
        var dataType = typeof(IData);
        Logger.Log($"{dataType.Name}타입 입니다.");
        //실행중인 어셈블리 가져오고
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //어셈블리 내의 모든 타입을 탐색
        foreach (var assembly in assemblies)
        {
            //어셈블리 타입을 가져와서
            var types = assembly.GetTypes();
            //각 타입에서
            foreach (var type in types)
            {
                //IData 를 상속받는 클래스또는 추상클래스인지 확인
                if (dataType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                {
                    Logger.Log($"타입 찾기{type.Name}");
                    //딕셔너리에 해당 타입의 인스턴스가 없을 경우
                    if (!_IDataDict.ContainsKey(type))
                    {
                        try
                        {
                            _IDataDict[type] = (IData)Activator.CreateInstance(type);
                            Logger.Log($"타입 인스턴스 생성 및 추가 성공: {type.Name}");
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError($"{type.Name} 인스턴스 생성 실패: {ex.Message}");
                        }
                    }
                }
            }
        }
    }

    void InitializeGameState()
    {
        DataTypes();
        Logger.Log("타입 체크");
        if (TitleCanvasUI._isNewGame)
        {
            SaveData<SaveDatas>();
            Logger.Log("첫 시작 입니다.");
        }
        else
        {
            Logger.Log("이어하기 입니다.");
        }
        GetData<SaveDatas>()?.Init();
        GetData<InventorySaveData>()?.Init();
        GetData<EquipmentSaveData>()?.Init();
        GetData<PlayerSaveData>()?.Init();
        GetData<SkillSaveData>()?.Init();
        GetData<QuestSaveData>()?.Init();
        Logger.Log("각 데이터 Init 실행 확인");
    }

    //Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    //{
    //    TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
    //    return JsonUtility.FromJson<Loader>(textAsset.text);
    //}

    public void SaveAllData()
    {
        SaveData<PlayerSaveData>();
        SaveData<InventorySaveData>();
        SaveData<EquipmentSaveData>();
        SaveData<SkillSaveData>();
        SaveData<QuestSaveData>();
    }

    public void LoadAllData()
    {
        LoadData<PlayerSaveData>();
        LoadData<InventorySaveData>();
        LoadData<SkillSaveData>();
        LoadData<EquipmentSaveData>();
        //LoadData<QuestSaveData>();
    }


    public void SaveData<T>() where T : class, IData
    {
        T dataToSave = GetData<T>();
        if (dataToSave != null)
        {
            dataToSave.SaveData();
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

    public T GetData<T>() where T : class, IData
    {
        Type type = typeof(T);

        if (_IDataDict.TryGetValue(type, out IData dataInstance))
        {
            return dataInstance as T;
        }
        Logger.LogError($"{type.Name}의 타입이 등록되지 않았습니다.");
        return null;
    }
}