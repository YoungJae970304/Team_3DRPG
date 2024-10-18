using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestListBtn : BaseUI, ISelectHandler
{
    #region BIND

    enum Buttons
    {
        QuestListBtn,
    }

    enum Texts
    {
        QuestListTxt,
    }

    #endregion

    UnityAction _onSelectAction;

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetButton((int)Buttons.QuestListBtn).onClick.AddListener(OpenQuestInfo);
    }

    //선택 처리 인터페이스함수
    public void OnSelect(BaseEventData eventData)
    {
        _onSelectAction();
    }

    //버튼 초기화 함수
    public void Initialize(string questInfoTilte, UnityAction selectAction)
    {
        QuestData questData = new QuestData();

        questInfoTilte = questData.Info;

        GetText((int)Texts.QuestListTxt).text = questInfoTilte;
        _onSelectAction = selectAction;
    }

    public void OpenQuestInfo()
    {
        
    }


    public void SetState(QuestState.State state)
    {
        switch (state)
        {
            case QuestState.State.RequirementNot:
            case QuestState.State.CanStart:
                
                break;
            case QuestState.State.InProgress:
            case QuestState.State.CanFinish:
              
                break;
                //완료된 퀘스트 표시 안함
            case QuestState.State.Finished:
                
                break;
            default:
                Logger.LogWarning("존재하지 않는 퀘스트 상태입니다");
                break;
        }
    }
}
