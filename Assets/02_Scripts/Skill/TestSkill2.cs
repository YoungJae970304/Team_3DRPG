using SkillModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkill2 : SkillBase
{
    public override SkillEnter Enter { get; set; } = new TestSkill2Enter();
    public override SkillStay Stay { get; set; } = new TestSkill2Stay();
    public override SkillExit Exit { get; set; } = new TestSkill2Exit();
    public override SkillPassive Passive { get; set; } = new TestSkill2Passive();

    public TestSkill2()
    {
        skillType = Define.SkillType.Normal;
        delay = 2f;
    }

    public class TestSkill2Enter : SkillEnter
    {
        public void Enter(ITotalStat stat)
        {
            Managers.Game._player._playerAnim.Play("Skill2");

            // 전방에 범위 데미지
        }
    }

    public class TestSkill2Stay : SkillStay
    {
        public void Stay(ITotalStat stat)
        {

        }

        public void End(ITotalStat stat)
        {

        }
    }

    public class TestSkill2Exit : SkillExit
    {
        public void Exit(ITotalStat stat)
        {

        }
    }

    public class TestSkill2Passive : SkillPassive
    {
        public void Passive(ITotalStat stat)
        {
            Debug.Log("TestSkill 패시브 효과");

            PlayerStatManager pStat = (PlayerStatManager)stat;
            pStat._buffStat.MaxHP += 50;
            pStat._buffStat.ATK += 30;
        }
    }
}
