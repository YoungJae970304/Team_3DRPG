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
        Debug.Log($"npc 타입은: {_npcType}"); 
        base.Interection(gameObject);

        switch (_npcType)
        {
            case NpcTypes.None:
                Logger.LogError("맞지 않는 타입 입니다.");
                break;
            case NpcTypes.DungeonNpc:
                DungeonNpcDialog();
                break;
                case NpcTypes.ShopNpc:
                ShopNpcDialog();
                break;
                case NpcTypes.QuestNpc:
                //퀘스트는 조건(PlayerLevel >= QuestStartLVRequire) 달성됐을 때 대화한 후 수락 또는 거절하기 처리
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

        if (dungeonDialogUI == null)
        {
            Managers.UI.OpenUI<DungeonDialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
        }

        //if( dungeonDialogUI != null )
        //{
        //    Managers.UI.CloseUI(dungeonDialogUI);
        //    Managers.Game._isActiveDialog = false;
        //}
        //else
        //{
        //    Managers.Game._isActiveDialog = true;
        //    Managers.UI.OpenUI<DungeonDialogUI>(new BaseUIData());
        //}
    }

    public virtual void ShopNpcDialog()
    {
        ShopDialogUI shopDialogUI = Managers.UI.GetActiveUI<ShopDialogUI>() as ShopDialogUI;

        if(shopDialogUI == null)
        {
            Managers.UI.OpenUI<ShopDialogUI>(new BaseUIData());
            Managers.Game._isActiveDialog = true;
        }


        //if (shopDialogUI != null)
        //{
        //    Managers.UI.CloseUI(shopDialogUI);
        //    Managers.Game._isActiveDialog = false;
        //}
        //else
        //{
        //    Managers.Game._isActiveDialog = true;
        //    Managers.UI.OpenUI<ShopDialogUI>(new BaseUIData());
        //}
    }
}
