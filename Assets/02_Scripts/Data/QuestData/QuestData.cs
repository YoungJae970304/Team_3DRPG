using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class QuestData : IData
{
    public int ID { get { return _id; } set { value = _id; } }
    public string Name { get { return _name; } set { value = _name; } }
    public string Info { get { return _info; } set { value = _info; } }
    public int PlayerLevelRequirement { get { return _playerLevelRequirement; } set { value = _playerLevelRequirement; } }
    public int TargetID { get { return _targetID; } set { value = _targetID; } }
    public int TargetCount { get { return _targetCount; } set { value = _targetCount; } }
    public bool IsComplate { get { return _isComplate; } set { value = false; } }

    //퀘스트 아이디
    int _id;
    //퀘스트 제목
    string _name;
    //퀘스트 정보
    string _info;
    //시작 가능 레벨
    int _playerLevelRequirement;
    //목표 대상의 ID(즉, 처치냐 모으기냐의 따른 ID 분류)
    int _targetID;
    //목표 수량(즉, 처치 마릿수, 기타아이템 수집 개수)
    int _targetCount;
    //완료 조건 충족 불 변수
    bool _isComplate = false;
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
    public enum RewardValueType1
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
    [Serializable]
    //경험치 + 골드 + 알파 라서 타입을 3개가지로 해서 가중치 입력
    public enum RewardValueType2
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
    [Serializable]
    //경험치 + 골드 + 알파 라서 타입을 3개가지로 해서 가중치 입력
    public enum RewardValueType3
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

    }

    public bool LoadData()
    {
        bool result = false;

        return result;
    }

    public bool SaveData()
    {
        bool result = false;

        return result;
    }
}