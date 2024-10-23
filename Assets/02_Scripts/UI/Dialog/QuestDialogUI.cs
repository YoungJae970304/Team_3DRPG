using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogUI : BaseUI
{
    enum Buttons
    {
        YesBtn,
    }

    public DialogSystem[] _dialogSystem;

    bool _isAccepted = false;
    bool _isRefuse = false;

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
    }

    private void OnEnable()
    {
        ActiveBtn();
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            QuestListAdd();
            _isAccepted = true;
        });
        _exitBtn.onClick.AddListener(() =>
        {
            _isRefuse = true;
        });
    }

    private void OnDisable()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        _exitBtn.onClick.RemoveListener(() =>  _isRefuse = false);
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
    }

    IEnumerator QuestDialog()
    {
        Managers.Game._player._isMoving = false;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());

        yield return new WaitUntil(() => _isAccepted || _isRefuse);
        //거절 버튼을 눌렀을경우 다이얼 로그 인덱스 번호 1번 실행 후 유아이 닫기
        if (_isAccepted)
        {
            yield return new WaitUntil(() => _dialogSystem[1].UpdateDialog());
            HideBtn();
            Managers.UI.CloseUI(this);
        }
        //거절 버튼을 눌렀을경우 다이얼 로그 인덱스 번호 2 번 실행 후 유아이 닫기
        else if (_isRefuse)
        {
            HideBtn();
            yield return new WaitUntil(() => _dialogSystem[2].UpdateDialog());
            Managers.UI.CloseUI(this);
        }
    }

    void HideBtn()
    {
        GetButton((int)Buttons.YesBtn).gameObject.SetActive(false);
        _exitBtn.gameObject.SetActive(false);
    }

    void ActiveBtn()
    {
        GetButton((int)Buttons.YesBtn).gameObject.SetActive(true);
        _exitBtn.gameObject.SetActive(true);
    }


    //수락 버튼 눌렀을 때 발행 해줄거  구독 시켜줄거  퀘스트 창이 켜지는게아니라 퀘스트창 안에 리스트버튼이 생성 되어야하니까...
    public void QuestListAdd()
    {
        //QuestLogUI questLogUI;
        
    }
}
