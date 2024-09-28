using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoSO
{
    //퀘스트 종류
    public Define.QuestType _questType;
    //퀘스트 제목
    public string _displayName;
    //퀘스트 요구 사항
    public int _playerLevelRequirement;

    //진행 중인 퀘스트
    public GameObject[] _questSteps;
    //퀘스트 보상
    public int _goldReward;
}
