using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIQuest : DialogUI
{
    bool _isAccepted = false;
    bool _isRefuse = false;
    bool _isDone = false;


    protected override void OnClickedButton()
    {
        //이 추상클래스에서 퀘스트 수락로직 작성
        AcceptQuest();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GetButton((int)Buttons.RefuseBtn).onClick.RemoveAllListeners();
        InitButtons();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    void InitButtons()
    {
        GetButton((int)Buttons.CheckBtn).onClick.AddListener(() => _isAccepted = true);
        GetButton((int)Buttons.RefuseBtn).onClick.AddListener(() => _isRefuse = true);
    }

    protected override IEnumerator DialogStart()
    {
        foreach (var dialog in _dialogSystem)
        {
            dialog.gameObject.SetActive(false);
        }
        _dialogSystem[0].gameObject.SetActive(true);
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isAccepted = false;
        _isDone = false;
        _isRefuse = false;
        yield return new WaitUntil(() => _isAccepted || _isRefuse);
        HideBtn();
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
        Managers.QuestManager._questInput = Define.QuestInput.Dialog;
        UITypeOpen<QuestUI>();
        
    }
}
