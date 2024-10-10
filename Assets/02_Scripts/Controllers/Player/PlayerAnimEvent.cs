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
}
