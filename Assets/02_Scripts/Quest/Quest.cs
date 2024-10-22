using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    //퀘스트 데이터의 퀘스트 정보
    public QuestData _QuestData { get; set; }
   
    //현재 퀘스트 진행 상황
    public int _currentProgress {  get; set; }

    //현재 퀘스트의 상태
    QuestState.State _currentState;

    //새로운 퀘스트를 초기화하는 생성자
    public Quest(QuestData questData)
    {
        _QuestData = questData;
        //초기 진행 상황
        _currentProgress = 0;
        //초기 상태를 불가능 상태로 설정
        _currentState = QuestState.State.RequirementNot;
    }

    //현재 상태를 가져올 함수
    public QuestState.State GetSatus()
    {
        return _currentState;
    }

    //현재 상태를 설정하는 함수
    public void SetSatus(QuestState.State newState)
    {
        _currentState = newState;
    }

    //퀘스트 완료여부를 판단해줄 함수
    public bool IsCompleted()
    {
        return _currentProgress >= _QuestData.TargetCount;
    }

    //서브퀘스트 반복퀘스트되도록 리셋
    public void ResetSubQuest()
    {
        if(_QuestData.Type == Define.QuestType.Sub)
        {
            _currentProgress = 0;
            SetSatus(QuestState.State.CanStart);
        }
    }

    public static Quest CreateQuest(int id)
    {
        DataTableManager dataTableManager = Managers.DataTable;
        QuestData questData = dataTableManager._QuestData.Find(q => q.ID == id);

        if(questData == null)
        {
            Logger.LogError($"해당 {id} 를 찾을 수 없습니다");
            return null;
        }
        return new Quest(questData);
    }
}
