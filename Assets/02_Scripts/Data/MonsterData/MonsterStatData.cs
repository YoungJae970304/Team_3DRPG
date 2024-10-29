using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MonsterStatDataListWrapper
{
    public List<MonsterStatData> MonsterStatDataList = new List<MonsterStatData>();
}
public class MonsterStatData
{
    [SerializeField] int _id;
    [SerializeField] string _name;
    [SerializeField] int _maxHp;
    [SerializeField] int _atk;
    [SerializeField] int _def;
    [SerializeField] int _moveSpeed;
    [SerializeField] int _atkSpeed;
    [SerializeField] int _recoveryHP;//
    [SerializeField] int _mp;
    [SerializeField] int _maxMP;
    [SerializeField] int _recoveryMP;
    [SerializeField] int _chaseRange;
    [SerializeField] int _returnRange;
    [SerializeField] int _attackRange;
    [SerializeField] int _awayRange;

    public int ID;
    public string Name;
    public int MaxHp;
    public int ATK;
    public int DEF;
    public int MoveSpeed;
    public int AtkSpeed;
    public int RecoveryHP;
    public int MP;
    public int MaxMP;
    public int RecoveryMP;
    public int ChaseRange;
    public int ReturnRange;
    public int AttackRange;
    public int AwayRange;
    public bool LoadData()
    {
        Logger.Log($"{GetType()}::LoadData");
        bool result = false;
        try
        {
            string key = "MonsterStatData_" + ID;
            string monsterStatDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, monsterStatDataJson);
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
            string key = "MonsterStatData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string monsterStatDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(monsterStatDataJson, this);
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
        MaxHp = _maxHp;
        ATK = _atk;
        DEF = _def;
        MoveSpeed = _moveSpeed;
        AtkSpeed = _atkSpeed;
       RecoveryHP = _recoveryHP;//
       MP = _mp;
       MaxMP = _maxMP;
       RecoveryMP = _recoveryMP;
       ChaseRange = _chaseRange;
       ReturnRange = _returnRange;
       AttackRange = _attackRange;
       AwayRange = _awayRange;
    }
}
