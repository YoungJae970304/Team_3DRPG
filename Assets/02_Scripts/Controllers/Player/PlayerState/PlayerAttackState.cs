using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseState
{
    public PlayerAttackState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        Logger.Log("공격 상태 진입");
        _player._playerAnim.SetBool("isAttacking", true);

        _player._curAtkCount = _player._playerInput._atkInput.Dequeue();

        switch (_player._curAtkCount)
        {
            case 0:
                Logger.Log("강공격");
                break;
            case 1:
                Logger.Log("기본공격 1타");
                _player._playerAnim.SetTrigger("doCombo1");
                //_player.SetColActive("Combo1");
                break;
            case 2:
                Logger.Log("기본공격 2타");
                _player._playerAnim.SetTrigger("doCombo2");
                //_player.SetColActive("Combo2");
                break;
            case 3:
                Logger.Log("기본공격 3타");
                _player._playerAnim.SetTrigger("doCombo3");
                //_player.SetColActive("Combo3");
                break;
            default:
                Logger.LogError("지정한 공격이 아님");
                break;
        }
            
        _player.ApplyDamage();
    }

    public override void OnStateUpdate()
    {
        Logger.Log("공격상태 업데이트");
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 상태 Exit ");

        //_player.AtkCount = 0;

        // 큐 초기화
        //_player._playerInput._atkInput.Clear();
    }
}
