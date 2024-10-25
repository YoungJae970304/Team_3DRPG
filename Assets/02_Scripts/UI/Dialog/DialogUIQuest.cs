using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIQuest : DialogUI
{
    Dictionary<Buttons, Button> _Button = new Dictionary<Buttons, Button>();

    bool _isAccepted = false;
    bool _isRefuse = false;
    bool _isDone = false;

    protected override void Awake()
    {
        base.Awake();
        InitButtons();
    }

    protected override void OnClickedButton()
    {
        //이 추상클래스에서 퀘스트 수락로직 작성
        AcceptQuest();
    }

    void InitButtons()
    {
        _Button[Buttons.CheckBtn] = GetButton((int)Buttons.CheckBtn);
        _Button[Buttons.RefuseBtn] = GetButton((int)Buttons.RefuseBtn);

        _Button[Buttons.CheckBtn].onClick.AddListener(() => OnButtonClicked(Buttons.CheckBtn));
        _Button[Buttons.RefuseBtn].onClick.AddListener(() => OnButtonClicked(Buttons.RefuseBtn));
    }

    void OnButtonClicked(Buttons buttons)
    {
        switch (buttons)
        {
            case Buttons.CheckBtn:
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

    protected override IEnumerator DialogStart()
    {
        foreach (var dialog in _dialogSystem)
        {
            dialog.gameObject.SetActive(false);
        }
        _dialogSystem[0].gameObject.SetActive(true);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        _isAccepted = false; 
        _isDone = false;
        _isRefuse = false;
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
        GetButton((int)Buttons.CheckBtn).gameObject.SetActive(false);
        GetButton((int)Buttons.RefuseBtn).gameObject.SetActive(false);
    }

    //수락 버튼 눌렀을 때 퀘스트 창이 켜지는게아니라 퀘스트창 안에 리스트버튼이 생성
    public void AcceptQuest()
    {

    }
}
