using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonDialogUI : BaseUI
{
    #region BIND
    enum Buttons
    {
        YesBtn,
    }

    enum Texts
    {
        YesBtnTxt,
        ExitBtnTxt,
    }

    #endregion

    public DialogSystem[] _dialogSystem;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        //대화 시작 시 모든 유아이 닫아버리기
        Managers.UI.CloseAllOpenUI();
        StartCoroutine(DungeonDialog());
    }

    IEnumerator DungeonDialog()
    {
        Managers.Game._isActiveDialog = true;
        Managers.Game._player._isMoving = false;
        GetButton((int)Buttons.YesBtn).interactable = false;
        GetText((int)Texts.YesBtnTxt).text = "던전 선택";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitForSeconds(0.2f);
        GetButton((int)Buttons.YesBtn).interactable = true;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() => { OpenDungeonUI(); });
        Managers.Game._isActiveDialog = false;
        Managers.Game._player._isMoving = true;
        Managers.UI.CloseUI(this);
    }

    #region 버튼 함수들
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
        }
    }
    #endregion
}