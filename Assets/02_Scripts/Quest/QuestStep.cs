using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    //현재 퀘스트 단계가 완료되었는지
    bool _isFinished = false;
    //이 퀘스트의 단계가 속한 퀘스트의 ID
    int _questID;
    //이 퀘스트 단계의 인덱스
    int _stepIdx;

    //각 퀘스트 단계마다 고유의 상태를 설정하는 추상함수
    protected abstract void SetQuestStepSate(string state);

    //퀘스트 단계 초기화
    protected void InitializeQuestStep(int questID, int stepIdx, string questStepState)
    {
        _questID = questID;
        _stepIdx = stepIdx;
        QuestData questData = Managers.DataTable._QuestData.Find(q => q.ID == questID);
        if(questData.Type == Define.QuestType.Main)
        {

        }else if(questData.Type == Define.QuestType.Sub)
        {

        }
        if(!string.IsNullOrEmpty(questStepState))
        {
            SetQuestStepSate(questStepState);
        }
    }

    protected void FinishQuestStep()
    {
        if (!_isFinished)
        {
            _isFinished = true;
            //퀘스트가 진행 되었음을 알리는 이벤트
            Managers.QuestEvents.AdvanceQuest(_questID);
            //이 이벤트 삭제
            Destroy(gameObject);
        }
    }

    protected void ChangeState(string newState, string newStatus)
    {
        Managers.QuestEvents.QuestStepStateChange(
            _questID,
            _stepIdx,
            new QuestStepState(newState, newStatus)
            );
    }
}
