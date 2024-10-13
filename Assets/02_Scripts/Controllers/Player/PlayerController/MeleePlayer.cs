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

    public override void Attack()
    {
        
    }

    public override void Skill()
    {
        // 추후 이곳에서 스킬 데이터를 받아서 SkillBase에 저장해주나?

    }

    public override void Special()
    {
        AtkCount = 0;

        _playerInput.InputBufferInsert(AtkCount);
        ChangeState(PlayerState.Attack);
    }
}
