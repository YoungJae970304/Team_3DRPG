using UnityEngine;

public class NpcController : Interectable
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        NpcDialog();
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
    }

    public virtual void NpcDialog()
    {
        DungeonDialogUI dungeonDialogUI = Managers.UI.GetActiveUI<DungeonDialogUI>() as DungeonDialogUI;

        if (dungeonDialogUI != null)
        {
            Managers.UI.CloseAllOpenUI();
            Managers.Game._isActiveDialog = false;
            Managers.Game._player._isMoving = true;
           
        }
        else
        {
            Managers.UI.OpenUI<DungeonDialogUI>(new BaseUIData());
            Managers.Game._player._isMoving = false;
            Managers.Game._isActiveDialog = true;
        }
    }
}
