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

    public DialogSystem[] _dialogSystem;

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        if (!Managers.Game._isActiveDialog) // 대사가 진행 중이지 않을 때만 실행
        {
            StartCoroutine(DialogStart());
        }
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.OpenBtn).onClick.AddListener(() => OpenDungeonUI());
    }

    IEnumerator DialogStart()
    {
        //대사 시작
        Managers.Game._isActiveDialog = true;
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        GetButton((int)Buttons.OpenBtn).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Managers.Game._isActiveDialog = false;
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
