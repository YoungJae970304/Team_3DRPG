using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    //퀘스트 데이터의 퀘스트 정보
    public QuestData _info;
    //현재 퀘스트 상태
    public QuestState.State _state;
    //현재 퀘스트 단계
    int _currentQuestStepIdx;
    //각 ㅜ케스트 단계의 상태를 저장하는 배열
    QuestStepState[] _questStepStates;

    //새로운 퀘스트를 초기화하는 생성자
    public Quest(QuestData questData)
    {
        this._info = questData;
        this._state = QuestState.State.RequirementNot;
        this._currentQuestStepIdx = 0;
        this._questStepStates = new QuestStepState[questData.TargetCount];
        for (int i = 0; i < this._questStepStates.Length; i++)
        {
            _questStepStates[i] = new QuestStepState();
        }
    }

    //저장된 퀘스트 데이터를 로드하는 생성자
    public Quest(QuestData info, QuestState.State state, int currentQuestStepIdx, QuestStepState[] questStepStates)
    {
        this._info = info;
        this._state = state;
        this._currentQuestStepIdx = currentQuestStepIdx;
        this._questStepStates = questStepStates;
    }

    //다음 퀘스트 단계로 이동
    public void MoveToNextQuestStep()
    {
        _currentQuestStepIdx++;
    }
}
