using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : BaseUI
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

    enum Dialogues
    {
        DungeonDialog,
        QuestDialog,
        ShopDialog,
    }

    #endregion

    public override void Init(Transform anchor)
    {
        base.Init(anchor);

        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행
        {
            Managers.UI.CloseAllOpenUI();
            GetButton((int)Buttons.YesBtn).onClick.RemoveAllListeners();
            Logger.LogWarning("모든 에드리스너 리무브되는거 확인");
            StartCoroutine(DungeonNPC());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }

    private void OnEnable()
    {
        Bind<DialogSystem>(typeof(Dialogues));
    }

    IEnumerator DungeonNPC()
    {
        Managers.Game._isActiveDialog = true;
        GetText((int)Texts.YesBtnTxt).text = "던전 선택";
        GetText((int)Texts.ExitBtnTxt).text = "아니?";
        yield return new WaitUntil(() => Get<DialogSystem>((int)Dialogues.DungeonDialog).UpdateDialog());
        bool isOpen = false;
        GetButton((int)Buttons.YesBtn).onClick.AddListener(() =>
        {
            isOpen = true;
            OpenDungeonUI();
            Logger.Log("던전 에드 리스너 확인");
        });
        Managers.Game._isActiveDialog = false;
        yield return new WaitUntil(() => isOpen);
        GetButton((int)Buttons.YesBtn).onClick.RemoveListener(() => OpenDungeonUI());
        Logger.Log("던전 에드 리스너 리무브 확인");
        Managers.UI.CloseUI(this);
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        DungeonUI dungeonUI = Managers.UI.GetActiveUI<DungeonUI>() as DungeonUI;

        if (dungeonUI == null)
        {
            Managers.UI.OpenUI<DungeonUI>(new BaseUIData());
        }
    }
}
