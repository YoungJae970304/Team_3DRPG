using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropDataListWrapper
{
    public List<DropData> DropDataList = new List<DropData>();
}

[Serializable]
public class DropData
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] int _dropType1;
    [SerializeField] int _startValue1;
    [SerializeField] int _endValue1;
    [SerializeField] int _dropType2;
    [SerializeField] int _startValue2;
    [SerializeField] int _endValue2;
    [SerializeField] int _dropType3;
    [SerializeField] int _startValue3;
    [SerializeField] int _endValue3;
    [SerializeField] int _dropType4;
    [SerializeField] int _startValue4;
    [SerializeField] int _endValue4;
    [SerializeField] int _itemType5;
    [SerializeField] int _value5;
    [SerializeField] int _itemType6;
    [SerializeField] int _value6;



    public int ID;
    public string Name;
    public int DropType1;
    public int StartValue1;
    public int EndValue1;
    public int DropType2;
    public int StartValue2;
    public int EndValue2;
    public int DropType3;
    public int StartValue3;
    public int EndValue3;
    public int DropType4;
    public int StartValue4;
    public int EndValue4;
    public int ItemValue5;
    public int Value5;
    public int ItemValue6;
    public int Value6;

    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");
        bool result = false;
        try
        {
            string key = "DropData_" + ID;
            string dropDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, dropDataJson);
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
            string key = "DropData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string dropDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(dropDataJson, this);
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
        ID = _id;
        Name = _name;
        DropType1 = _dropType1;
        StartValue1 = _startValue1;
        EndValue1 = _endValue1;
        DropType2 = _dropType2;
        StartValue2 = _startValue2;
        EndValue3 = _endValue3;
        DropType4 = _dropType4;
        StartValue4 = _startValue4;
        EndValue4 = _endValue4;
        ItemValue5 = _itemType5;
        Value5 = _value5;
        ItemValue6 = _itemType6;
        Value6 = _value6;
    }
}
