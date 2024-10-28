using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonNpc : NpcController
{
    public override void Interection(GameObject gameObject)
    {
        base.Interection(gameObject);
        NpcDialog<DialogUIDungeon>();
    }
}
