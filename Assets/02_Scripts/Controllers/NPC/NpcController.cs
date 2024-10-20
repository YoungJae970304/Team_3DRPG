using UnityEngine;

public class NpcController : Interectable
{
    public enum NpcTypes
    {
        DungeonNpc,
        QuestNpc,
        ShopNpc,
    }

   public NpcTypes _npcType;

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
        DialogUI dialogUI = Managers.UI.GetActiveUI<DialogUI>() as DialogUI;

        if (dialogUI == null)
        {
            Managers.UI.OpenUI<DialogUI>(new BaseUIData());
            Managers.Game._player._isMoving = false;
            Managers.Game._isActiveDialog = true;

        }
        else
        {
            Managers.UI.CloseAllOpenUI();
            Managers.Game._isActiveDialog = false;
            Managers.Game._player._isMoving = true;
        }
    }
}
