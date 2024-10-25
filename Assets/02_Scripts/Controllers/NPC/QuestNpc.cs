using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        NpcDialog<DialogUIQuest>();
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
        
    }
}
