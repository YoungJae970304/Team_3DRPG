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
        UITypeOpen<DungeonUI>();
    }
}