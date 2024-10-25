using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogUI : BaseUI
{
    public enum Buttons
    {
        CheckBtn,//확인
        RefuseBtn,//거절
        SynthesisBtn,//합성
    }

    public DialogSystem[] _dialogSystem;

    [HideInInspector]
    public bool _isOpenUI = false;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        //모든 버튼 시작할땐 꺼두기
        foreach (Buttons btns in Enum.GetValues(typeof(Buttons)))
        {
            var btn = GetButton((int)btns);
            if (btn != null)
                btn.gameObject.SetActive(false);
        }
        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(DialogStart());
    }

    protected virtual void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    protected void ActiveBtns(Buttons btns)
    {
        var btn = GetButton((int)btns);
        if (btn != null)
            btn.gameObject.SetActive(true);
    }

    protected virtual void OnEnable()
    {
        GetButton((int)Buttons.CheckBtn).onClick.AddListener(() =>
        {
            OnClickedButton();
            _isOpenUI = true;
        });

        GetButton((int)Buttons.RefuseBtn).onClick.AddListener(() =>
        {
            CloseUI(this);
        });
    }

    protected virtual void OnDisable()
    {
        GetButton((int)Buttons.CheckBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.RefuseBtn).onClick.RemoveAllListeners();
       Managers.Game._cantInputKey = false;
    }

    protected abstract void OnClickedButton();
    protected abstract IEnumerator DialogStart();
}
