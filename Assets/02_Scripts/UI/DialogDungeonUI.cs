using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogDungeonUI : BaseUI
{
    enum Buttons
    {
        OpenBtn,
    }

    enum DialogSystems
    {
        DungeonDialog,
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<DialogSystem>(typeof(DialogSystems));

        //GetButton((int)Buttons.OpenBtn).onClick.AddListener(() => OpenDungeonUI());

        //StartCoroutine(DialogStart());
    }

    IEnumerator Start()
    {
        //대사 시작
        yield return new WaitUntil(() => Get<DialogSystem>((int)DialogSystems.DungeonDialog).UpdateDialog());
        GetButton((int)Buttons.OpenBtn).gameObject.SetActive(true);
        Managers.Game._isActiveDialog = false;
        yield return new WaitForSeconds(0.2f);
        Managers.UI.CloseUI(this);
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        //DungeonType dungeonUi = Managers.UI.GetActiveUI<DungeonType>() as DungeonType;

        //if(dungeonUi != null )
        //{
        //    Managers.UI.CloseUI(dungeonUi);
        //}
        //else
        //{
        //    Managers.UI.OpenUI<DungeonType>(new BaseUIData());
        //}
    }
}
