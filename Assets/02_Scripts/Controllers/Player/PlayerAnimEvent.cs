using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    Player _player;

    private void Start()
    {
        _player = Managers.Game._player;
    }

    // 평타 애니메이션 시작부
    public void AttackStart()
    {
        _player._canAtkInput = false;
        _player._attacking = true;
    }

    // 평타 애니메이션 중반부
    public void CanAttackInput()
    {
        _player._canAtkInput = true;
    }

    // 평타 데미지 적용
    public void PlayerNormalAttack()
    {
        _player.Attack();
    }

    // 평타 애니메이션 후반부
    public void AttackEnd()
    {
        if (_player._playerInput._atkInput.Count < 1)
        {
            _player._attacking = false;
            _player._playerAnim.SetBool("isAttacking", false);
        }
    }

    // 스킬
    public void SkillEnd()
    {
        _player._skillUsing = false;
    }

    // 광역으로 일괄 데미지를 넣을 때 사용하는 이벤트 range로 범위 조절
    public void AreaDamage(float range)
    {
        Vector3 playerPos = Managers.Game._player.transform.position;

        for (int i = 0; i < Managers.Game._monsters.Count; i++)
        {
            if (Vector3.Distance(playerPos, Managers.Game._monsters[i].transform.position) < range)
            {
                if (Managers.Game._monsters[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }
        }
    }

    #region 원거리 플레이어 스킬
    // 1번째 스킬 ( 주위로 광역 데미지 )
    public void FirstSkillDamage()
    {
        Collider[] hitMobs = Physics.OverlapSphere(Managers.Game._player.transform.position, 30f, 1 << LayerMask.NameToLayer("Monster"));
        foreach (Collider col in hitMobs)
        {
            if (col.TryGetComponent<IDamageAlbe>(out var damageable))
            {
                damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
            }
        }
    }

    public void SecondSkillDamage()
    {
        Collider[] hitMobs = Physics.OverlapBox(Managers.Game._player.transform.position + (Vector3.forward * 2), new Vector3(1.5f,1f,1f), Quaternion.identity, 1 << LayerMask.NameToLayer("Monster"));
        foreach (Collider col in hitMobs)
        {
            if (col.TryGetComponent<IDamageAlbe>(out var damageable))
            {
                damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
            }
        }
    }

    public void ChainLightningDamage()
    {
        Vector3 playerPos = Managers.Game._player.transform.position;

        // 체인 라이트닝 데미지, 범위 내의 적들에게 플레이어와 가까운 순서대로 데미지를 가함
        Managers.Game.SortMonsterList();
        for (int i = 0; i < Managers.Game._monsters.Count; i++)
        {
            if (Vector3.Distance(playerPos, Managers.Game._monsters[i].transform.position) < 10)
            {
                if (Managers.Game._monsters[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    Logger.LogWarning(Managers.Game._monsters[i].name);
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }

            // 추후 다음 에너미에게 데미지를 가하기까지 딜레이를 줘야함

        }
    }
    #endregion

    #region 근거리 플레이어 스킬
    public void MeleeFirstSkillDamage()
    {
        _player.ApplyDamage();
    }

    public void MeleeSecondSkillDamage(float range)
    {
        // 주변을 넓게 베는 스킬, 발도같은 느낌
        Logger.LogWarning("근거리 플레이어 2번째 스킬");

        Vector3 playerPos = Managers.Game._player.transform.position;

        for (int i = 0; i < Managers.Game._monsters.Count; i++)
        {
            if (Vector3.Distance(playerPos, Managers.Game._monsters[i].transform.position) < range)
            {
                if (Managers.Game._monsters[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }
        }
    }

    public void MeleeThirdSkillDamage()
    {
        Logger.LogWarning("근거리 플레이어 3번째 스킬");
    }

    public void SwordAuraCreate()
    {
        MeleePlayer meleePlayer = (MeleePlayer) _player;

        // 검기 생성
        GameObject go = Managers.Resource.Instantiate("Player/SwordAura");
        go.transform.position = meleePlayer._swordAuraPos.position;
    }
    #endregion

    // 회피 애니메이션 시작부
    public void DodgeStart()
    {
        _player._invincible = true;
    }

    // 애니메이션 무적 해제부분
    public void InvincibleOff()
    {
        _player._invincible = false;
    }

    // 회피 애니메이션 회피 상태 해제부분
    public void DodgeEnd()
    {
        _player._dodgeing = false;
    }

    // 피격 애니메이션 피격 상태 해제부분
    public void HittingEnd()
    {
        _player._hitting = false;
    }
}
