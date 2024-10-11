using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedState : BaseState
{
    public PlayerDamagedState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        // 이후 몬스터의 넉백 공격, 기절 공격 등 다른 조건에 따라 이 상태로 진입
        switch (_player._playerHitState)
        {
            case PlayerHitState.StunAttack:
                // 스턴 애니메이션 재생
                _player._playerAnim.SetTrigger("doStun");
                _player.InvincibleDelay();
                Logger.Log("플레이어 스턴");
                break;
            case PlayerHitState.SkillAttack:
                // 피격 애니메이션 재생
                _player._playerAnim.SetTrigger("doDamaged");
                Logger.Log("플레이어 움찔");
                break;
        }
    }
    public override void OnStateUpdate()
    {

    }

    public override void OnStateExit()
    {
        _player._hitting = false;
        _player._invincible = false;
    }
}
