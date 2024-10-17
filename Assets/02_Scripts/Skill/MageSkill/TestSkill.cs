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

        // 상준님한테 피드백 받아보기
        PlayerStatManager pStat = (PlayerStatManager)stat;
        pStat._buffStat.ATK += 10;

        //Managers.Game._player._playerStatManager._buffStat.ATK += 10;

        //Logger.LogError(" 데미지 확인 : " + stat.ATK);

        // 자기 주위로 광역 데미지
        /* 애니메이션 이벤트로 옮김
        Collider[] hitMobs = Physics.OverlapSphere(Managers.Game._player.transform.position, 30f, 1 << LayerMask.NameToLayer("Monster"));
        foreach (Collider col in hitMobs)
        {
            if (col.TryGetComponent<IDamageAlbe>(out var damageable))
            {
                damageable.Damaged(stat.ATK);
            }
        }
        */
    }
}

public class TestSkillStay : SkillStay
{
    public void Stay(ITotalStat stat, int level = 0)
    {

    }

    public void End(ITotalStat stat, int level = 0)
    {

    }
}

public class TestSkillExit : SkillExit
{
    public void Exit(ITotalStat stat, int level = 0)
    {
        // 증가된 공격력 복구

        PlayerStatManager pStat = (PlayerStatManager)stat;
        pStat._buffStat.ATK -= 10;

        //Managers.Game._player._playerStatManager._buffStat.ATK -= 10;
    }
}

public class TestSkillPassive : SkillPassive
{
    public void Passive(ITotalStat stat, int level = 0)
    {
        Debug.Log("TestSkill 패시브 효과");

        PlayerStatManager pStat = (PlayerStatManager)stat;
        pStat._buffStat.MaxHP += 50;

        //Managers.Game._player._playerStatManager._buffStat.MaxHP += 50;
    }
}
