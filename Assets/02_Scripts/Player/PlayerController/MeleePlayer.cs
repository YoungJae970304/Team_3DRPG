using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override void Attack()
    {
        Debug.Log("근거리 캐릭터 공격");
    }
}
