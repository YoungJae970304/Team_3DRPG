using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class QuestDataListWrapper
{
    //퀘스트 데이터를 담는 리스트
    public List<QuestData> QuestDataList = new List<QuestData>();
}

[Serializable]
public class QuestData : IData
{
    public int ID;
    public Define.QuestType Type;
    public string Name;
    public string Info;
    public int PlayerLevelRequirement;
    public int TargetID;
    public int TargetCount;
    public int RewardValue1;
    public int RewardValue2;
    public int RewardValue3;
    public RewardType ValType1;
    public RewardType ValType2;
    public RewardType ValType3;

    //퀘스트 아이디
   [SerializeField] int _id;
    //퀘스트 타입
    [SerializeField] Define.QuestType _questType;
    //퀘스트 제목
    [SerializeField] string _name;
    //퀘스트 정보
    [SerializeField] string _info;
    //시작 가능 레벨
    [SerializeField] int _playerLevelRequirement;
    //목표 대상의 ID(즉, 처치냐 모으기냐의 따른 ID 분류)
    [SerializeField] int _targetID;
    //목표 수량(즉, 처치 마릿수, 기타아이템 수집 개수)
    [SerializeField] int _targetCount;
    //보상 타입에 따른 보상 수량
    [SerializeField] int _rewardValue1;
    [SerializeField] int _rewardValue2;
    [SerializeField] int _rewardValue3;
    //리워드 밸류타입
    [SerializeField] RewardType _rewardType1;
    [SerializeField] RewardType _rewardType2;
    [SerializeField] RewardType _rewardType3;
    
    //퀘스트의 리워드 타입
    [Serializable]
    public enum RewardType
    {
        //장비 1번중 선택
        Equipped = 1,
        //포션
        Potion,
        //골드
        Gold,
        //경험치
        Exp,
    }

    public void SetDefaultData()
    {
        ID = _id;
        Type = _questType;
        Name = _name;
        Info = _info;
        TargetID = _targetID;
        TargetCount = _targetCount;
        PlayerLevelRequirement = _playerLevelRequirement;
        RewardValue1 = _rewardValue1;
        RewardValue2 = _rewardValue2;
        RewardValue3 = _rewardValue3;
        ValType1 = _rewardType1;
        ValType2 = _rewardType2;
        ValType3 = _rewardType3;
    }

    public bool SaveData()
    {
        bool result = false;
        try
        {
            string key = "QusetData_" + ID;
            string questDataJson = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(key, questDataJson);
            PlayerPrefs.Save();
            result = true;

        }
        catch (Exception e)
        {
            Logger.Log($"저장 실패(" + e.Message + ")");
        }
        return result;
    }

    public bool LoadData()
    {
        bool result = false;
        try
        {
            string key = "QusetData_" + ID;
            if (PlayerPrefs.HasKey(key))
            {
                string questDataJson = PlayerPrefs.GetString(key);
                JsonUtility.FromJsonOverwrite(questDataJson, this);
                result = true;
            }
            else
            {
                Logger.LogWarning("저장된 데이터가 없음");
            }
        }
        catch (Exception e)
        {
            Logger.Log("로드 실패(" + e.Message + ")");
        }
        return result;
    }
}
