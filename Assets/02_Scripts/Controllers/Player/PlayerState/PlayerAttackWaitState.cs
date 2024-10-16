using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackWaitState : BaseState
{
    public PlayerAttackWaitState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {

    }

    public override void OnStateUpdate()
    {
        Logger.Log("공격 대기 상태 Update");

        AnimatorStateInfo stateInfo = _player._playerAnim.GetCurrentAnimatorStateInfo(0);

        switch (_player._playerType)
        {
            case Define.PlayerType.Melee:
                // 현재 애니메이션이 공격 애니메이션인지 확인
                if (IsAttackAnimation(stateInfo))
                {
                    if (stateInfo.normalizedTime <= 0.25f)
                    {
                        // 부드러운 회전
                        Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
                        _player._playerModel.rotation = Quaternion.Slerp(_player._playerModel.rotation, targetRot, _player._rotSpeed);

                        // 약간 전진
                        _player._moveDir = _player._playerModel.forward * _player._playerStatManager.MoveSpeed * Time.fixedDeltaTime;
                        _player._cc.Move(new Vector3(_player._moveDir.x, 0, _player._moveDir.z));
                    }
                }
                break;
            case Define.PlayerType.Mage:
                if (IsAttackAnimation(stateInfo))
                {
                    if (stateInfo.normalizedTime <= 0.25f)
                    {
                        // 부드러운 회전
                        Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
                        _player._playerModel.rotation = Quaternion.Slerp(_player._playerModel.rotation, targetRot, _player._rotSpeed);
                    }
                }
                break;
            default:
                break;
        }


        
    }

    public override void OnStateExit()
    {
        Logger.Log("공격 대기 상태 Exit");
        if (_player._playerInput._atkInput.Count < 1)
        {
            _player._attacking = false;
            _player._playerAnim.SetBool("isAttacking", false);
        }
    }

    private bool IsAttackAnimation(AnimatorStateInfo stateInfo)
    {
        // 공격 애니메이션의 이름 또는 해시값을 확인
        return stateInfo.IsName("SpAttack") || stateInfo.IsName("Combo1") || stateInfo.IsName("Combo2") || stateInfo.IsName("Combo3");
    }
}
