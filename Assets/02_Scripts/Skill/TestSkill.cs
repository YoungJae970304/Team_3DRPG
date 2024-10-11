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
    public class TestSkillEnter : SkillEnter
    {
        public void Enter(ITotalStat stat)
        {
            Debug.Log("TestSkill 시작");

            

            Managers.Game._player._playerAnim.Play("Skill1");

            Managers.Game._player._playerStatManager._buffStat.ATK += 10; // 임시로 공격력 증가

            // 자기 주위로 광역 데미지
            Collider[] hitMobs = Physics.OverlapSphere(Managers.Game._player.transform.position, 30f, 1 << LayerMask.NameToLayer("Monster"));
            foreach (Collider col in hitMobs)
            {
                if (col.TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    Logger.Log(" 데미지 확인 : " + Managers.Game._player._playerStatManager.ATK);
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }
        }
    }

    public class TestSkillStay : SkillStay
    {
        public void Stay(ITotalStat stat)
        {
            Debug.Log("TestSkill 지속중");
        }

        public void End(ITotalStat stat)
        {
            Debug.Log("TestSkill 지속 효과 종료");
        }
    }

    public class TestSkillExit : SkillExit
    {
        public void Exit(ITotalStat stat)
        {
            Debug.Log("TestSkill 종료");
            Managers.Game._player._playerStatManager._buffStat.ATK -= 10; // 증가된 공격력 복구
        }
    }

    public class TestSkillPassive : SkillPassive
    {
        public void Passive(ITotalStat stat)
        {
            Debug.Log("TestSkill 패시브 효과");
            Managers.Game._player._playerStatManager._buffStat.MaxHP += 50;
        }
    }

}
