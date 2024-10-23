using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkSpawn : SpawnEnemy
{
    public override void Start()
    {
        base.Start();
        MonsterSpawn(3);
        _dungeonManager._startCheck = true;
    }
}
