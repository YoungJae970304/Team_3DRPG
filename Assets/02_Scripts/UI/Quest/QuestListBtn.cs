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

    QuestLogUI _questLogUI;

    QuestData _questData = new QuestData();

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _questLogUI = GetComponentInParent<QuestLogUI>();
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        GetButton((int)Buttons.QuestListBtn).onClick.AddListener(() => _questLogUI.OpenSetInfo());
        //퀘스트의 데이터를 가지고있는애를 가져와서 오픈해줄거임
        GetButton((int)Buttons.QuestListBtn).onClick.AddListener(() => _questLogUI.QuestInfoSet(_questData.ID));
    }

    //선택 처리 인터페이스함수
    public void OnSelect(BaseEventData eventData)
    {
        _onSelectAction();
    }

    //버튼 초기화 함수
    public void Initialize(string questInfoTilte, UnityAction selectAction)
    {
        questInfoTilte = _questData.Name;
        GetText((int)Texts.QuestListTxt).text = questInfoTilte;
        _onSelectAction = selectAction;
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

    public void SetQeustType(QuestData questData)
    {
        switch (questData.Type)
        {
            case Define.QuestType.Main:
                break;
            case Define.QuestType.Sub:
                break;
        }
    }
}
