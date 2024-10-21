using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : Interectable
{
    public enum NpcTypes
    {
        None,
        DungeonNpc,
        ShopNpc,
        QuestNpc,
    }

    public NpcTypes _npcType;

    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        switch (_npcType)
        {
            case NpcTypes.DungeonNpc:
                DungeonNpcDialog();
                break;
                case NpcTypes.ShopNpc:
                ShopNpcDialog();
                break;
                case NpcTypes.QuestNpc:
                break;
            default:
                Logger.LogError("맞지 않는 타입 입니다.");
                break;
        }
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
    }

    public virtual void DungeonNpcDialog()
    {
        DungeonDialogUI dungeonDialogUI = Managers.UI.GetActiveUI<DungeonDialogUI>() as DungeonDialogUI;

        if( dungeonDialogUI != null )
        {
            Managers.UI.CloseAllOpenUI();
            Managers.Game._isActiveDialog = false;
        }
        else
        {
            Managers.Game._player._isMoving = false;
            Managers.UI.OpenUI<DungeonDialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
        }
    }
    public virtual void ShopNpcDialog()
    {
        ShopDialogUI shopDialogUI = Managers.UI.GetActiveUI<ShopDialogUI>() as ShopDialogUI;

        if (shopDialogUI != null)
        {
            Managers.UI.CloseAllOpenUI();
            Managers.Game._isActiveDialog = false;
        }
        else
        {
            Managers.Game._player._isMoving = false;
            Managers.UI.OpenUI<ShopDialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
        }
    }
}
