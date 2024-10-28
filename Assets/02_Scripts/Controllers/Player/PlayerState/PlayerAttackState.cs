using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    public PlayerAttackState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player._playerAnim.SetBool("isAttacking", true);

        _player._curAtkCount = _player._playerInput._atkInput.Dequeue();

        switch (_player._curAtkCount)
        {
            case 0:
                _player._playerAnim.SetTrigger("doSpAttack");
                break;
            case 1:
                _player._playerAnim.SetTrigger("doCombo1");
                break;
            case 2:
                _player._playerAnim.SetTrigger("doCombo2");
                break;
            case 3:
                _player._playerAnim.SetTrigger("doCombo3");
                break;
            default:
                Logger.LogError("지정한 공격이 아님");
                break;
        }
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}
