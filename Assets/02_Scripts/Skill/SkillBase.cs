using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase//전략 패턴 사용
{
    public SkillBase()
    {
        // 생성자로 스킬 데이터 초기화
        // 데이터테이블로 받은 데이터들 이곳에서 초기화
    }
    public virtual SkillModule.SkillEnter Enter { get; set;} //스킬 시전시 효과
    public virtual SkillModule.SkillStay Stay { get; set; } //스킬 시전중 효과
    public virtual SkillModule.SkillExit Exit { get; set; } //스킬 종료시 효과
    public virtual SkillModule.SkillPassive Passive { get; set; }//스킬 습득시 적용되는 효과

    public Define.SkillType skillType;  //스킬의 타입ex) 키다운or기본스킬

    public Sprite _icon;             //스킬 아이콘

    public float delay;                 //스킬의 후딜레이

    public int _level;

    // 데이터 테이블로 받을 변수들 추가 -> 이후 위의 생성자에서 초기화

    public virtual void SkillEnter(ITotalStat stat) {
        Enter.Enter(stat, _level);
    }//스킬 시전시
    public virtual void SkillStay(ITotalStat stat) {
        Stay.Stay(stat, _level);
    }//스킬 시전도중

    public virtual void SkillExit(ITotalStat stat) {
        Stay.End(stat, _level);
        Exit.Exit(stat, _level);
    }//스킬 시전종료시

    public virtual void PassiveEffect(ITotalStat stat) {
        Passive.Passive(stat, _level);
    }//패시브 효과

}


namespace SkillModule {//전략
    public interface SkillEnter {
        // 이곳에 BaseDamage, DamageValue 매개변수 추가 ( Enter, Stay, Exit )
        public void Enter(ITotalStat stat,int level=0);
    }
    public interface SkillStay
    {
        public void Stay(ITotalStat stat, int level = 0);
        public void End(ITotalStat stat, int level = 0);

    }
    public interface SkillExit
    {
        public void Exit(ITotalStat stat, int level = 0);
    }
    public interface SkillPassive
    {
        // 여긴 StatType, StatValue 매개변수 추가, 패시브는 switch문으로 statType을 받게? 그럼 하나로 다 관리 가능?
        public void Passive(ITotalStat stat, int level = 0);
    }
}