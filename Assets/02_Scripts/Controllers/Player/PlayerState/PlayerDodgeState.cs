using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : BaseState
{
    public PlayerDodgeState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    AnimatorStateInfo stateInfo;

    public override void OnStateEnter()
    {
        Logger.Log("회피 진입");
        //_player._playerAnim.SetBool("isDodge", true);
        //_player._playerAnim.SetTrigger("doDodge");
        _player._playerAnim.Play("Dodge");
        _player._playerAnim.SetBool("isAttacking", false);
        _player._playerAnim.SetBool("Run", false);
        _player._playerAnim.SetBool("ZoomMode", false);
        _player.AtkCount = 0;
        _player._dodgeing = true;
    }

    public override void OnStateUpdate()
    {
        //if (_player._invincible)
        stateInfo = _player._playerAnim.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Dodge"))
        {
            if (stateInfo.normalizedTime <= 0.3f)
            {
                Dodge();
            }
        }
    }

    public override void OnStateExit()
    {
        Logger.Log("회피 Exit");
        _player._canAtkInput = true;
        _player._attacking = false;
    }

    void Dodge()
    {
        // 카메라 전환
        if (_player._playerCam._cameraMode == Define.CameraMode.ZoomView)
        {
            _player._playerCam.DisableFL();
            _player._playerCam.CamModeChange();
        }

        if (_player._rotDir != Vector3.zero)
        {
            // 실제 회전 -> 아마 바로 돌아가는 느낌이 아니라 애니메이션 적용시 원하는 동작이 안나올수도
            _player._playerModel.rotation = Quaternion.LookRotation(_player._rotDir);
        }

        // 회피 방향
        _player._moveDir = _player._playerModel.transform.forward * _player._playerStatManager.DodgeSpeed * Time.deltaTime;
        // 회피
        //_player._cc.Move(_player._moveDir);
        _player._cc.Move(new Vector3(_player._moveDir.x, 0, _player._moveDir.z));
    }
}
