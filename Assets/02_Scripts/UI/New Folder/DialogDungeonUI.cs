using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogDungeonUI : BaseUI
{
    [SerializeField]
    DialogSystem[] _dialogSystem;
    
    enum Buttons
    {
        OpenBtn,
    }

    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Button>(typeof(Buttons));
        StartCoroutine(DialogStart());
    }

    public override void CloseUI(bool isCloseAll = false)
    {
        base.CloseUI(isCloseAll);
        StopAllCoroutines();
    }

    IEnumerator DialogStart()
    {
        //대사 시작
        GetButton((int)Buttons.OpenBtn).gameObject.SetActive(true);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        GetButton((int)Buttons.OpenBtn).onClick.AddListener(() => OpenDungeonUI());
        yield return new WaitForSeconds(0.2f);
        GetButton((int)Buttons.OpenBtn).gameObject.SetActive(false);
        Managers.UI.CloseUI(this);
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        DungeonType dungeonUi = Managers.UI.GetActiveUI<DungeonType>() as DungeonType;

        if(dungeonUi != null )
        {
            Managers.UI.CloseUI(dungeonUi);
        }
        else
        {
            Managers.UI.OpenUI<DungeonType>(new BaseUIData());
        }
    }
}
