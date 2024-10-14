using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager
{
    //현재 플레이어 레벨
    public int _currPlayerLevel;
    //현재 활성화된 퀘스트 리스트
    public List<Quest> _ActiveQuests = new List<Quest>();
    //전체 퀘스트를 일단 가지고있는 리스트
    public List<QuestData> _AllQuestData = new List<QuestData>();
    //퀘스트 상태 관리 딕셔너리
    public Dictionary<int, QuestState.State> _QuestStates = new Dictionary<int, QuestState.State>();
    public void Init()
    {
        LoadQuestData();
    }
    void LoadQuestData()
    {
        var questData = Managers.DataTable._QuestData;

        foreach (var quest in questData)
        {
            _AllQuestData.AddRange(questData);
            Logger.Log($"퀘스트 데이터 ID: {quest.ID}");
        }
    }

    //시작 메서드
    public void OnStartQuest(int id)
    {
        QuestData questData = _AllQuestData.Find(q => q.ID == id);
        CheckUnlockQuest();
        if (questData != null)
        {
            if (questData.IsUnlock)
            {
                // 퀘스트 시작 로직
                Logger.Log("퀘스트를 수락");
                _ActiveQuests.Add(new Quest(questData));
            }
        }
    }

    //시작 가능 체크
    public void CheckUnlockQuest()
    {
        int playeLevel = Managers.Game._player._playerStatManager._originStat.Level;

        foreach (var quest in _AllQuestData)
        {
            if(playeLevel >= quest.PlayerLevelRequirement && !quest.IsUnlock)
            {
                //퀘스트 로그 UI 업데이트
                //NPC를 퀘스트 수락 포인트로 지정 해서 머리에 표시까지
                //퀘스트 로그 UI 제작...
                quest.IsUnlock = true;
            } 
        }
    }

    //진행 메서드
    public void OnAdvanceQuest(int id)
    {

    }

    //완료 메서드
    public void OnFinishQuest(int id)
    {

    }

    //상태 전환 메서드
    public void OnQuestStateChange(Quest quest)
    {

    }

    //진행 상태 전환 메서드
    public void OnQuestStepStateChange(int id, int stepIndex, QuestStepState state)
    {

    }
}