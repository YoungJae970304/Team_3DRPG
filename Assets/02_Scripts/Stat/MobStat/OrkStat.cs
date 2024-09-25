using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkStat : MonsterStat
{
    // Start is called before the first frame update
    void Start()
    {
        ChaseRange = 20;
        ReturnRange = 20;
        AttackRange = 2;
        AwayRange = 15;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
