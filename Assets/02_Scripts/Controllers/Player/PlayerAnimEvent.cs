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
