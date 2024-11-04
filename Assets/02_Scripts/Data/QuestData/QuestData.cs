using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class QuestData
{
    //퀘스트 아이디
    public int ID;
    //퀘스트 타입
    public Define.QuestType Type;
    //퀘스트 제목
    public string Name;
    //퀘스트 정보
    public string Info;
    //시작 가능 레벨
    public int PlayerLevelRequirement;
    //목표 대상의 ID(즉, 처치냐 모으기냐의 따른 ID 분류)
    public int TargetID;
    //목표 수량(즉, 처치 마릿수, 기타아이템 수집 개수)
    public int TargetCount;
    //보상 타입에 따른 보상 수량
    public int RewardValue1;
    public int RewardValue2;
    public int RewardValue3;
    //리워드 밸류타입(1 = 골드)
    public RewardType RewardType1;
    public RewardType RewardType2;
    public RewardType RewardType3;

    //퀘스트의 리워드 타입
    [Serializable]
    public enum RewardType
    {
        //골드
        Gold = 1,
        //경험치
        Exp,
        //포션
        Potion,
    }
}
