using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo_", menuName = "QuestInfoSO/Quest")]
public class QuestInfoSO : ScriptableObject
{
    //퀘스트 종류
    public Define.QuestType _questType;
    //퀘스트 제목
    public string _displayName;
    //퀘스트 요구 사항
    public int _playerLevelRequirement;
    //퀘스트진행중인 스크립터블 오브젝트 배열
    public QuestInfoSO[] _questRequirenet;

    //진행 중인 퀘스트
    public GameObject[] _questSteps;
    //퀘스트 보상
    public int _goldReward;
}
