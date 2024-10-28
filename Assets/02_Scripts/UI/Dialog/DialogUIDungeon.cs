using System.Collections;
using UnityEngine;

public class DialogUIDungeon : DialogUI
{
    protected override IEnumerator DialogStart()
    {
        ActiveBtns(Buttons.CheckBtn);
        ActiveBtns(Buttons.RefuseBtn);
        yield return new WaitUntil(() => _dialogSystem[0].UpdateDialog());
        _isOpenUI = false;
        yield return new WaitUntil(() => _isOpenUI);
        Managers.UI.CloseUI(this);
    }

    protected override void OnClickedButton()
    {
        OpenDungeonUI();
    }

    //던전 UI 오픈 함수 버튼 클릭시 생성
    public void OpenDungeonUI()
    {
        if (!Managers.UI.IsActiveUI<DungeonUI>())
        {
            Managers.UI.OpenUI<DungeonUI>(new BaseUIData());
            //_isOpenUI = true;
        }
    }
}