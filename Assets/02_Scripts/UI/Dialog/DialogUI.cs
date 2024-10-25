using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogUI : BaseUI
{
    public enum Buttons
    {
        YesBtn,
        RefuseBtn,
    }

    public DialogSystem[] _dialogSystem;

    [HideInInspector]
    public bool _isOpenUI = false;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(DialogStart());
    }

    protected virtual void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }

    protected virtual void OnEnable()
    {
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            OnButton();
            _isOpenUI = true;
        });
    }

    protected virtual void OnDisable()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        Managers.Game._cantInputKey = false;
    }

    protected abstract void OnButton();
    protected abstract IEnumerator DialogStart();
}
