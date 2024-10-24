using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogUI : BaseUI
{
    enum Buttons
    {
        YesBtn,
        RefuseBtn,
    }

    Dictionary<Buttons, Button> _Button = new Dictionary<Buttons, Button>();

    public DialogSystem[] _dialogSystem;

    bool _isAccepted = false;
    bool _isRefuse = false;
    bool _isDone = false;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();


        StartCoroutine(QuestDialog());
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        InitButtons();
    }

    private void OnEnable()
    {
        ActiveBtn();
    }

    void InitButtons()
    {
        _Button[Buttons.YesBtn] = GetButton((int)Buttons.YesBtn);
        _Button[Buttons.RefuseBtn] = GetButton((int)Buttons.RefuseBtn);

        _Button[Buttons.YesBtn].onClick.AddListener(() => OnButtonClicked(Buttons.YesBtn));
        _Button[Buttons.RefuseBtn].onClick.AddListener(() => OnButtonClicked(Buttons.RefuseBtn));
    }


    private void OnDisable()
    {
        Managers.Game._cantInputKey = false;
        //Managers.Game._player._isMoving = true;
    }

    IEnumerator QuestDialog()
    {
        foreach (var dialog in _dialogSystem)
        {
            dialog.gameObject.SetActive(false);
        }
        //Managers.Game._player._isMoving = false;
        _dialogSystem[0].gameObject.SetActive(true);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());

        _isAccepted = false; _isDone = false; _isRefuse = false;
        yield return new WaitUntil(() => _isAccepted || _isRefuse);
        //거절 버튼을 눌렀을경우 다이얼 로그 인덱스 번호 1번 실행 후 유아이 닫기
        if (_isAccepted)
        {
            _dialogSystem[0].gameObject.SetActive(false);
            yield return RunDialog(1);
        }
        //거절 버튼을 눌렀을경우 다이얼 로그 인덱스 번호 2 번 실행 후 유아이 닫기
        else if (_isRefuse)
        {
            _dialogSystem[0].gameObject.SetActive(false);
            yield return RunDialog(2);
        }
        yield return new WaitUntil(() => _isDone);
        Managers.UI.CloseUI(this);
    }
    void OnButtonClicked(Buttons buttons)
    {
        switch (buttons)
        {
            case Buttons.YesBtn:
                _isAccepted = true;
                break;
            case Buttons.RefuseBtn:
                _isRefuse = true;
                break;
            default:
                Logger.Log("버튼이 없습니다. 버튼을 추가해주세요");
                break;
        }
    }

    IEnumerator RunDialog(int idx)
    {
        HideBtn();
        _dialogSystem[idx].gameObject.SetActive(true);
        yield return new WaitUntil(() => _dialogSystem[idx].UpdateDialog());
        _dialogSystem[idx].gameObject.SetActive(false);
        _isDone = true;
    }

    void HideBtn()
    {
        GetButton((int)Buttons.YesBtn).gameObject.SetActive(false);
        GetButton((int)Buttons.RefuseBtn).gameObject.SetActive(false);
    }

    void ActiveBtn()
    {
        GetButton((int)Buttons.YesBtn).gameObject.SetActive(true);
        GetButton((int)Buttons.RefuseBtn).gameObject.SetActive(true);
    }

    //수락 버튼 눌렀을 때 발행 해줄거  구독 시켜줄거  퀘스트 창이 켜지는게아니라 퀘스트창 안에 리스트버튼이 생성 되어야하니까...
    public void AcceptQuest()
    {
        //QuestLogUI questLogUI;

    }
}
