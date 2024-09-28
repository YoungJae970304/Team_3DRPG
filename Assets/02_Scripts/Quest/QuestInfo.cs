using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfo
{
    /// <summary>
    ///퀘스트창에서 보여질 내용을 작성한다.
    /// 진행중인 퀘스트를 화면에서 보여줄 내용
    /// 퀘스트 창에서는 Reward를 보여줘야함
    /// RewardType은 화면에 보여지지 않는다.
    /// 퀘스트 시작 레벨 요구 사항은 굳이 보여줄 필요가 없다.
    /// 퀘스트 이름에 타입 까지 적어서 표시
    /// </summary>

    public enum RewardType
    {
        Gold,
        Exp,
        Weapon,
        Armor,
        Accessroies,
        Potion,
    }

    //퀘스트 종류
    public Define.QuestType _questType;
    //퀘스트 제목
    public string _displayName;
    //퀘스트 시작 레벨 요구사항
    public int _playerLevelRequirement;
    //진행 중인 퀘스트
    public GameObject[] _questSteps;
    //퀘스트 보상
    public RewardType _rewardType;
}
