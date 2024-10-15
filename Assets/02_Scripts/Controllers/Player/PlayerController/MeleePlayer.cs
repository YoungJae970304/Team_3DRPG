using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    protected override void Awake()
    {
        base.Awake();

        _playerType = Define.PlayerType.Melee;
    }

    public override void Attack()
    {
        ApplyDamage();
    }

    public override void Special()
    {
        AtkCount = 0;

        _playerInput.InputBufferInsert(AtkCount);
    }

    public override void SkillSetE()
    {
        // 추후 이곳에서 스킬 데이터를 받아서 SkillBase에 저장해주나?
        _skillBase = new MeleeSkill1();
    }

    public override void SkillSetR()
    {
        _skillBase = new MeleeSkill2();
    }
}
