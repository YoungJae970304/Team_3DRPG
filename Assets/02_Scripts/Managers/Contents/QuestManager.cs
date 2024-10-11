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

    private void OnEnable()
    {
        //시작 이벤트 구독
        Managers.QuestEvents._onStartQuest += OnStartQuest;
        //진행 이벤트 구독
        Managers.QuestEvents._onAdvanceQuest += OnAdvanceQuest;
        //완료 이벤트 구독
        Managers.QuestEvents._onFinishQuest += OnFinishQuest;
        //상태 변경 이벤트 구독
        Managers.QuestEvents._onQuestStateChange += OnQuestStateChange;
        //스텝 상태 변경 이벤트 구독
        Managers.QuestEvents._onQuestStepStateChange += OnQuestStepStateChange;
    }

    private void OnDisable()
    {
        //시작 이벤트 구독 해지
        Managers.QuestEvents._onStartQuest -= OnStartQuest;
        //진행 이벤트 구독 해지
        Managers.QuestEvents._onAdvanceQuest -= OnAdvanceQuest;
        //완료 이벤트 구독 해지
        Managers.QuestEvents._onFinishQuest -= OnFinishQuest;
        //상태 변경 이벤트 구독 해지
        Managers.QuestEvents._onQuestStateChange -= OnQuestStateChange;
        //스텝 상태 변경 이벤트 구독 해지
        Managers.QuestEvents._onQuestStepStateChange -= OnQuestStepStateChange;
    }

    void LoadQuestData()
    {
        var questData = Managers.DataTable._QuestData;

        foreach (var quest in questData)
        {
            _AllQuestData.AddRange(questData);
            Logger.Log($"퀘스트 데이터 ID: {quest.ID} 이름: {quest.Name}");
        }
    }

    //시작 메서드
  public  void OnStartQuest(int id)
    {
        
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

    //퀘스트 맵을 생성하는 함수
    //Dictionary<int, Quest> CreateQuestMap()
    //{


    //}

    //퀘스트 아이디 가져오는 함수
    //Quest GetQuestByID(int id)
    //{

    //}

    //시작 가능을 확인할 bool 함수
    //bool CheckRequirementMet()
    //{

    //}
}