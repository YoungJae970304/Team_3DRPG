using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class SkillBase//전략 패턴 사용
{
    public abstract SkillModule.SkillEnter Enter { get; set;} //스킬 시전시 효과
    public abstract SkillModule.SkillStay Stay { get; set; } //스킬 시전중 효과
    public abstract SkillModule.SkillExit Exit { get; set; } //스킬 종료시 효과
    public abstract SkillModule.SkillPassive Passive { get; set; }//스킬 습득시 적용되는 효과

    public Define.SkillType skillType;  //스킬의 타입ex) 키다운or기본스킬

    protected Sprite _icon;             //스킬 아이콘

    public float delay;                 //스킬의 후딜레이
    public virtual void SkillEnter(ITotalStat stat) {
        Enter.Enter(stat);
    }//스킬 시전시
    public virtual void SkillStay(ITotalStat stat) {
        Stay.Stay(stat);
    }//스킬 시전도중

    public virtual void SkillExit(ITotalStat stat) {
        Stay.End(stat);
        Exit.Exit(stat);
    }//스킬 시전종료시

    public virtual void PassiveEffect(ITotalStat stat) {
        Passive.Passive(stat);
    }//패시브 효과

}


namespace SkillModule {//전략
    public interface SkillEnter {
        public void Enter(ITotalStat stat);
    }
    public interface SkillStay
    {
        public void Stay(ITotalStat stat);
        public void End(ITotalStat stat);

    }
    public interface SkillExit
    {
        public void Exit(ITotalStat stat);
    }
    public interface SkillPassive
    {
        public void Passive(ITotalStat stat);
    }
}