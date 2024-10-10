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
    public string Name;
    public string Info;
    public int PlayerLevelRequirement;  
    public int TargetID;
    public int TargetCount;
    public bool IsComplate;
    public RewardType Type;
    public RewardValueType ValType1;
    public RewardValueType ValType2;
    public RewardValueType ValType3;

    //퀘스트 아이디
   [SerializeField] int _id;
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
    //완료 조건 충족 불 변수
    [SerializeField] bool _isComplate = false;
    //리워드 타입
    [SerializeField] RewardType _rewardType;
    //리워드 밸류타입
    [SerializeField] RewardValueType _rewardValueType1;
    [SerializeField] RewardValueType _rewardValueType2;
    [SerializeField] RewardValueType _rewardValueType3;
    
    [Serializable]
    public enum RewardType
    {
        //단일
        Type1 = 1,
        //반복
        Type2,
    }
    [Serializable]
    //경험치 + 골드 + 알파 라서 타입을 3개가지로 해서 가중치 입력
    public enum RewardValueType
    {
        //보상 없음
        NonRewarded,
        //골드
        Gold,
        //경험치
        Exp,
        //장비
        Equipment,
        //포션
        Potion,
    }

    public void SetDefaultData()
    {
        ID = _id;
        Name = _name;
        Info = _info;
        PlayerLevelRequirement = _playerLevelRequirement;
        TargetID = _targetID;
        TargetCount = _targetCount;
        IsComplate = _isComplate;
        Type = _rewardType;
        ValType1 = _rewardValueType1;
        ValType2 = _rewardValueType2;
        ValType3 = _rewardValueType3;
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
