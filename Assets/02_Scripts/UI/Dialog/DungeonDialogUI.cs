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

        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행ㅔ
        {
            //대화 시작 시 모든 유아이 닫아버리기
            Managers.UI.CloseAllOpenUI();
            StartCoroutine(DungeonNPC());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    public IEnumerator DungeonNPC()
    {
        Managers.Game._isActiveDialog = true;
        GetText((int)Texts.YesBtnTxt).text = "던전 선택";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenDungeonUI();
            Logger.Log("던전 에드 리스너 확인");
        });
        Managers.Game._isActiveDialog = false;
        yield return new WaitUntil(() => isOpen);
        ReomovedListeners();
        Logger.Log("던전 에드 리스너 리무브 확인");
        Managers.UI.CloseAllOpenUI();
    }

    #region 버튼 함수들
    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        DungeonUI dungeonUI = Managers.UI.GetActiveUI<DungeonUI>() as DungeonUI;

        if (dungeonUI == null)
        {
            Managers.UI.OpenUI<DungeonUI>(new BaseUIData());
        }
    }

    public void ReomovedListeners()
    {
        GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
    }

    #endregion
}