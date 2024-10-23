using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawn : SpawnEnemy
{
    public override void Start()
    {
        base.Start();
        MonsterSpawn(1);
        _dungeonManager._startCheck = true;
    }

}
