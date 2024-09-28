using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseState
{
    public PlayerIdleState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        Logger.Log("Idle Exit");
    }
}
