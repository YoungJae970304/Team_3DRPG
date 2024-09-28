using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class Ork : Monster, IDamageAlbe
{
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void BaseState()
    {
        base.BaseState();
    }

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
