using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestListBtn : MonoBehaviour, ISelectHandler
{
    public Button _button {  get; private set; }

    Text _listBtnText;
    UnityAction _onSelectAction;
   
    //선택 처리 인터페이스함수
    public void OnSelect(BaseEventData eventData)
    {
        _onSelectAction();
    }

    //버튼 초기화 함수
    public void Initialize(string questInfoTilte, UnityAction selectAction)
    {
        _button = this.GetComponent<Button>();
        _listBtnText = this.GetComponentInChildren<Text>();

        _listBtnText.text = questInfoTilte;
        _onSelectAction = selectAction;
    }

    public void SetState(QuestState.State state)
    {
        switch (state)
        {
            case QuestState.State.RequirementNot:
            case QuestState.State.CanStart:
                _listBtnText.color = Color.red;
                break;
            case QuestState.State.InProgress:
            case QuestState.State.CanFinish:
                _listBtnText.color = Color.yellow;
                break;
            case QuestState.State.Finished:
                _listBtnText.color = Color.green;
                break;
            default:
                Logger.LogWarning("존재하지 않는 퀘스트 상태입니다");
                break;
        }
    }
}
