using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemSpawn : SpawnEnemy
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        MonsterSpawn(2);
    }

}
