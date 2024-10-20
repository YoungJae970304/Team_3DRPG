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
        DialogUI dialogUI = Managers.UI.GetActiveUI<DialogUI>() as DialogUI;
        if (dialogUI == null)
        {
            Managers.UI.OpenUI<DialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
        }
        else
        {
            Managers.UI.CloseAllOpenUI();
            Managers.Game._isActiveDialog = false;
        }
    }
}
