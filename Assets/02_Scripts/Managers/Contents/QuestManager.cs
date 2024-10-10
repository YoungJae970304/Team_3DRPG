using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class QuestManager
{
    //현재 플레이어 레벨
    public int _currPlayerLevel;
    //퀘스트 이벤트 참조
    public QuestEvents _questEvents = Managers.QuestEvents;
    //현재 활성화된 퀘스트 리스트
    public List<Quest> _ActiveQuests = new List<Quest>();
    //전체 퀘스트를 일단 가지고있는 리스트
    public List<QuestData> _AllQuestData = new List<QuestData>();
    //퀘스트 상태 관리 딕셔너리
    public Dictionary<int, QuestState.State> _QuestStates = new Dictionary<int, QuestState.State>();

    private void OnEnable()
    {
        //시작 이벤트 구독
        _questEvents._onStartQuest += OnStartQuest;
        //진행 이벤트 구독
        _questEvents._onAdvanceQuest += OnAdvanceQuest;
        //완료 이벤트 구독
        _questEvents._onFinishQuest += OnFinishQuest;
        //상태 변경 이벤트 구독
        _questEvents._onQuestStateChange += OnQuestStateChange;
        //스텝 상태 변경 이벤트 구독
        _questEvents._onQuestStepStateChange += OnQuestStepStateChange;

        LoadQuestData();
    }

    private void OnDisable()
    {
        //시작 이벤트 구독 해지
        _questEvents._onStartQuest -= OnStartQuest;
        //진행 이벤트 구독 해지
        _questEvents._onAdvanceQuest -= OnAdvanceQuest;
        //완료 이벤트 구독 해지
        _questEvents._onFinishQuest -= OnFinishQuest;
        //상태 변경 이벤트 구독 해지
        _questEvents._onQuestStateChange -= OnQuestStateChange;
        //스텝 상태 변경 이벤트 구독 해지
        _questEvents._onQuestStepStateChange -= OnQuestStepStateChange;
    }

    void LoadQuestData()
    {
        var questData = Managers.DataTable._QuestData;

        _AllQuestData.AddRange(questData);
        Logger.Log("퀘스트 데이터 리스트 가져오기 확인" + questData);
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
}