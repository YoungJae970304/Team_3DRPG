using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        Managers.Sound.RandSoundsPlay("NPC/dungeon_talk_1", "NPC/dungeon_talk_2");
        NpcDialog<DialogUIDungeon>();
    }
}
