using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    protected override void Start()
    {
        base.Start();
        _playerType = Define.PlayerType.Melee;
    }

    public override void Skill()
    {
        SkillOffTimer(1f);
    }

    public override void Special()
    {
        AtkCount = 0;

        _playerInput.InputBufferInsert(AtkCount);
        ChangeState(PlayerState.Attack);
    }
}
