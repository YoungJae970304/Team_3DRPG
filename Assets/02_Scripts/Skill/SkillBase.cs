using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase//전략 패턴 사용
{
    protected SkillData _skillData;

    public SkillBase(int skillId)
    {
        // 생성자로 스킬 데이터 초기화
        // 데이터테이블로 받은 데이터들 이곳에서 초기화
        _skillData = Managers.DataTable.GetSkillData(skillId);

        _skillName = _skillData.SkillName;
        _skillType = _skillData.SkillType;
        _statType = _skillData.StatType;
        _usingMP = _skillData.UseingMP;
        _needSP = _skillData.NeedSkillPoint;
        _maxLevel = _skillData.MaxLevel;
    }

    public SkillModule.SkillEnter Enter { get; set;} //스킬 시전시 효과
    public SkillModule.SkillStay Stay { get; set; } //스킬 시전중 효과
    public SkillModule.SkillExit Exit { get; set; } //스킬 종료시 효과
    public SkillModule.SkillPassive Passive { get; set; }//스킬 습득시 적용되는 효과

    public SkillData.SkillTypes _skillType;  //스킬의 타입ex) 액티브스킬, 패시브 스킬
    public SkillData.StatTypes _statType;   // 패시브 스킬의 스탯 타입

    public Sprite _icon;             //스킬 아이콘

    public string _skillName;
    public int _usingMP;
    public int _needSP;
    public int _level;
    public int _maxLevel;

    public int _damage;

    public int _prevLevel;

    public string _skillInfo;

    // 플레이어의 공격력 스탯이 오를때, 스킬의 레벨이 오를때마다 호출 -> 하는줄 알았으니 그냥 이곳 Enter에서 데미지를 미리 계산하면 그럴 필요가 없어짐
    public void UpdateSkill(ITotalStat stat)
    {
        // 마나 소모
        stat.MP -= _usingMP;

        //_damage = (int)(stat.ATK * ((_skillData.BaseDamage + (_level * _skillData.DamageValue)) * 0.01f));
        const int SCALE_FACTOR = 100; // 0.01을 곱하는 대신 100으로 나누기 위한 스케일 팩터

        // 모든 계산을 정수로 수행
        int tempDamage = stat.ATK * (_skillData.BaseDamage + (_level * _skillData.DamageValue));

        _damage = tempDamage / SCALE_FACTOR;

        Logger.Log($"스킬 초기화 확인 : {_skillName}, {_skillType}, {_statType}, {_usingMP}, {_needSP}, {_maxLevel}");
    }

    public virtual void SkillEnter(ITotalStat stat) {
        Enter.Enter(stat, _skillData, _level);
        UpdateSkill(stat);
    }//스킬 시전시
    public virtual void SkillStay(ITotalStat stat) {
        Stay.Stay(stat, _skillData, _level);
    }//스킬 시전도중

    public virtual void SkillExit(ITotalStat stat) {
        Stay.End(stat, _skillData, _level);
        Exit.Exit(stat, _skillData, _level);
    }//스킬 시전종료시

    public virtual void PassiveEffect(ITotalStat stat) {
        Passive.Passive(stat, _skillData, _level, _prevLevel);
    }//패시브 효과
}


namespace SkillModule {//전략
    public interface SkillEnter {
        // 이곳에 BaseDamage, DamageValue 매개변수 추가 ( Enter, Stay, Exit )
        public void Enter(ITotalStat stat, SkillData skillData, int level = 0);
    }
    public interface SkillStay
    {
        public void Stay(ITotalStat stat, SkillData skillData, int level = 0);
        public void End(ITotalStat stat, SkillData skillData, int level = 0);

    }
    public interface SkillExit
    {
        public void Exit(ITotalStat stat, SkillData skillData, int level = 0);
    }
    public interface SkillPassive
    {
        // 여긴 StatType, StatValue 매개변수 추가, 패시브는 switch문으로 statType을 받게? 그럼 하나로 다 관리 가능?
        public void Passive(ITotalStat stat, SkillData skillData, int curLevel, int prevLevel);
    }
}