using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestEvents
{
    //퀘스트 시작 액션
    public event Action<int> _onStartQuest;
    //퀘스트 진행 액션
    public event Action<int> _onAdvanceQuest;
    //퀘스트 완료 액션
    public event Action<int> _onFinishQuest;
    //퀘스트 상태 변경 액션
    public event Action<Quest> _onQuestStateChange;
    //퀘스트 단계의 상태를 변경하는 액션
    public event Action<int, int, QuestStepState> _onQuestStepStateChange;

    //퀘스트 시작 트리거 함수
    public void StartQuest(int id)
    {
         if(_onStartQuest != null)
        {
            //퀘스트 id로 퀘스트 시작 알림 액션 이벤트
            //즉, 현재 퀘스트아이디가 80001인 퀘스트가 시작되었으니 그 퀘스트의 정보들을 가져옴
            _onStartQuest(id);
        }
    }

    public void AdvanceQuest(int id)
    {
        if(_onAdvanceQuest != null)
        {
            //퀘스트 진행 액션 이벤트
            _onAdvanceQuest(id);
        }
    }

    public void FinishQuest(int id)
    {
        if(_onFinishQuest != null)
        {
            //퀘스트 완료 액션 이벤트
            _onFinishQuest(id);
        }
    }

    public void QuestStateChange(Quest quest)
    {
        if(_onQuestStateChange != null)
        {
            //퀘스트 상태 변경 액션 이벤트
            _onQuestStateChange(quest);
        }
    }

    public void QuestStepStateChange(int id, int stepIndex, QuestStepState questStepState)
    {
        if(_onQuestStepStateChange != null)
        {
            //퀘스트 단계(즉, 메인퀘스트의 다음 단계 정도의 변경 액션 이벤트
            _onQuestStepStateChange(id, stepIndex, questStepState);
        }
    }
}
