using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogDungeonUI : BaseUI
{
    [SerializeField]
    DialogSystem[] _dialogSystem;
    public Button _dungeonUIOpenBtn;


    public override void Init(Transform anchor)
    {
        base.Init(anchor);
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
        _dungeonUIOpenBtn.gameObject.SetActive(true);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        yield return new WaitForSeconds(0.2f);
        _dungeonUIOpenBtn.gameObject.SetActive(false);
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
