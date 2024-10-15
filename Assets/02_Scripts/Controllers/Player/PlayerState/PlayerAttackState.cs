using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    public PlayerAttackState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        Logger.Log("공격 상태 진입");
        _player._playerAnim.SetBool("isAttacking", true);
        //_player.SetColActive("Combo3");

        _player._curAtkCount = _player._playerInput._atkInput.Dequeue();

        switch (_player._curAtkCount)
        {
            case 0:
                Logger.Log("강공격");
                _player._playerAnim.SetTrigger("doSpAttack");
                break;
            case 1:
                Logger.Log("기본공격 1타");
                _player._playerAnim.SetTrigger("doCombo1");
                break;
            case 2:
                Logger.Log("기본공격 2타");
                _player._playerAnim.SetTrigger("doCombo2");
                break;
            case 3:
                Logger.Log("기본공격 3타");
                _player._playerAnim.SetTrigger("doCombo3");
                break;
            default:
                Logger.LogError("지정한 공격이 아님");
                break;
        }
    }

    public override void OnStateUpdate()
    {
        Logger.Log("공격상태 업데이트");
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 상태 Exit ");
    }
}
