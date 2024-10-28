using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill : SkillBase
{
    public override SkillEnter Enter { get; set; } = new TestSkillEnter();
    public override SkillStay Stay { get; set; } = new TestSkillStay();
    public override SkillExit Exit { get; set; } = new TestSkillExit();
    public override SkillPassive Passive { get; set; } = new TestSkillPassive();

    public TestSkill()
    {
        skillType = Define.SkillType.Normal;
        delay = 2f;
    }

    // 추후 아래의 Enter, Stay, Exit, Passive 들은 스킬마다 스크립트로 새로 제작
    // 그리고 각 스크립트의 Enter, Stay, Exit, Passive 들을 따로 가져와 위에서 조립?하는 식으로 사용할 수 있게 되어있음. ( 전략 패턴 )
    /* 예시) 이런식으로 조합이 가능함
    public override SkillEnter Enter { get; set; } = new NoneSkillPassive();
    public override SkillStay Stay { get; set; } = new TestSkillStay();
    public override SkillExit Exit { get; set; } = new TestSkillExit();
    public override SkillPassive Passive { get; set; } = new NoneSkillPassive();
     */
}
public class TestSkillEnter : SkillEnter
{
    public void Enter(ITotalStat stat, int level = 0)
    {
        Managers.Game._player._playerAnim.Play("Skill1");

        stat.ATK = 10;
    }
}

public class TestSkillStay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;

    public void Stay(ITotalStat stat, int level = 0)
    {
        // 애니메이션 진행도 8&에서 30% 시점까지는 빠른 이동
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1"))
        {
            float normalizedTime = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            // 8% 진행 지점에서 이벤트 트리거
            if (normalizedTime >= 0.44f && normalizedTime <= 0.46f && !_damageApply)
            {
                Managers.Game._player.AreaDamage(15f, stat.ATK);

                _damageApply = true;
            }
        }
    }

    public void End(ITotalStat stat, int level = 0)
    {
        _damageApply = false;
    }
}

public class TestSkillExit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
        // 증가된 공격력 복구
        stat.ATK = -10;
    }
}

public class TestSkillPassive : SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
        Debug.Log("TestSkill 패시브 효과");

        stat.MaxHP = 50;
    }
}
