using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblem : Monster, IDamageAlbe
{
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 66)
        {
            NomalAttack();
        }
        else
        {
            SkillAttack();
        }
    }
    public void NomalAttack()
    {
        Logger.Log("NomalAttack");
        AttackPlayer();
    }
    public void SkillAttack()
    {
        Logger.Log("SkillAttack");
        AttackPlayer();
    }
}
