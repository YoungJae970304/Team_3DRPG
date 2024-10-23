using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDialogUI : BaseUI
{
    enum Buttons
    {
        YesBtn,
    }

    public DialogSystem[] _dialogSystem;

    bool _isOpenUI = false;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(DungeonDialog());
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
    }
    private void OnEnable()
    {
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            OpenDungeonUI();
            _isOpenUI = true;
        });
    }

    private void OnDisable()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
    }

    IEnumerator DungeonDialog()
    {
        Managers.Game._player._isMoving = false;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        DungeonUI dungeonUI = Managers.UI.GetActiveUI<DungeonUI>() as DungeonUI;

        if (dungeonUI != null)
        {
            Managers.UI.CloseUI(dungeonUI);
        }
        else
        {
            Managers.UI.OpenUI<DungeonUI>(new BaseUIData());
            _isOpenUI = true;
        }
    }
}