using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Interectable
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        DungeonNpcDialog();
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
    }

    public virtual void DungeonNpcDialog()
    {
        DialogDungeonUI dialogDungeonUI = Managers.UI.GetActiveUI<DialogDungeonUI>() as DialogDungeonUI;
        if (dialogDungeonUI == null)
        {
            Managers.UI.OpenUI<DialogDungeonUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
            Managers.Game._player._isMoving = false;
        }
        else
        {
            Managers.UI.CloseCurrFrontUI(dialogDungeonUI);
            Managers.Game._isActiveDialog = true;
        }
    }
}
