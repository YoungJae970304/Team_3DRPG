using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        Managers.Sound.RandSoundsPlay("NPC/quest_talk_1", "NPC/quest_talk_2");
        NpcDialog<DialogUIQuest>();
    }

    public override void UIPopUp(bool active)
    {
        base.UIPopUp(active);
        
    }
}
