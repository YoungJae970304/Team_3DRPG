using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogUIDungeon : DialogUI
{
    protected override IEnumerator DialogStart()
    {
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    protected override void OnButton()
    {
        OpenDungeonUI();
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