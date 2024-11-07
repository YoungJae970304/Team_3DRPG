using SkillModule;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThunderSlash : SkillBase
{
    //private const int SKILL_ID = 1;

    public ThunderSlash(int skillId) : base (skillId)
    {
        Enter = new ThunderSlashEnter();
        Stay = new ThunderSlashStay();
        Exit = new ThunderSlashExit();
        Passive = new NoneSkillPassive();
    }
}

public class ThunderSlashEnter : SkillEnter
{
    // "Skill1"이라는 이름의 BoxCollider를 찾음
    BoxCollider _thunderSlashCol = Managers.Game._player._atkColliders.OfType<BoxCollider>().FirstOrDefault(col => col.gameObject.name == "Skill1");

    // 시작과 끝 중심 및 크기 정의 (초기값)
    Vector3 _startCenter = new Vector3(0, 0, 2);
    Vector3 _startSize = new Vector3(3, 2, 8);

    public void Enter(ITotalStat stat, SkillData skillData, int level = 0)
    {
        // 콜라이더를 초기 크기와 위치로 복원
        if (_thunderSlashCol != null)
        {
            _thunderSlashCol.center = _startCenter;
            _thunderSlashCol.size = _startSize;
        }

        Managers.Game._player.SetColActive("Skill1");

        Managers.Game._player._playerAnim.Play("Skill1");

        stat.MoveSpeed = 10;
    }
}

public class ThunderSlashStay : SkillStay
{
    Animator _anim = Managers.Game._player._playerAnim;
    bool _damageApply = false;
    BoxCollider _thunderSlashCol = Managers.Game._player._atkColliders.OfType<BoxCollider>().FirstOrDefault(col => col.gameObject.name == "Skill1");

    // 시작과 끝 중심 및 크기 정의
    Vector3 _startCenter = new Vector3(0, 0, 2);
    Vector3 _endCenter = new Vector3(0, 0, 0);
    Vector3 _startSize = new Vector3(3, 2, 8);
    Vector3 _endSize = new Vector3(3, 2, 2);

    public void Stay(ITotalStat stat, SkillData skillData, int level = 0)
    {
        // 애니메이션 진행도 8&에서 30% 시점까지는 빠른 이동
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Skill1"))
        {
            float normalizedTime = _anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

            // 애니메이션 진행도에 따라 콜라이더 크기와 중심 조정 (0.4까지 줄어듦)
            if (normalizedTime <= 0.4f && _thunderSlashCol != null)
            {
                // 진행도에 따라 콜라이더 크기와 위치를 선형 보간 (Lerp)
                _thunderSlashCol.center = Vector3.Lerp(_startCenter, _endCenter, normalizedTime / 0.4f);
                _thunderSlashCol.size = Vector3.Lerp(_startSize, _endSize, normalizedTime / 0.4f);
            }
            // 8% 진행 지점에서 이벤트 트리거
            if (normalizedTime >= 0.08f && normalizedTime <= 0.3f)
            {
                Managers.Game._player._cc.Move(Managers.Game._player._playerModel.forward * Managers.Game._player._playerStatManager.MoveSpeed * Time.deltaTime);
            }
            if (normalizedTime >= 0.4f && normalizedTime <= 0.42f && !_damageApply)
            {
                Managers.Game._player.ApplyDamage(stat.ATK);
                _damageApply = true;
            }
        }
    }

    public void End(ITotalStat stat, SkillData skillData, int level = 0)
    {
        _damageApply = false;
    }
}

public class ThunderSlashExit : SkillExit
{
    public void Exit(ITotalStat stat, SkillData skillData, int level = 0)
    {
        Managers.Game._player.SetColActive("Katana");
        Managers.Game._player._hitMobs.Clear();
        // 증가된 속도 복구
        stat.MoveSpeed = -10;
    }
}
