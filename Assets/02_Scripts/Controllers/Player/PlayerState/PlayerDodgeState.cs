using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(Player player) : base(player) { }

    float _curTime;

    public override void OnStateEnter()
    {
        _player._dodgeing = true;
        _player._cc.enabled = false;
    }

    public override void OnStateUpdate()
    {
        Dodge();
        DodgeTimer();
    }

    public override void OnStateExit()
    {
        Logger.Log("회피 Exit");

        _player._cc.enabled = true;
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
        _player._moveDir = _player._playerModel.transform.forward * _player._playerStat.DodgeSpeed * Time.deltaTime;
        // 회피
        _player.transform.position += _player._moveDir;
    }

    // 일정 시간 후 회피 상태 해제
    void DodgeTimer()
    {
        _curTime += Time.deltaTime;
        if (_curTime > _player._dodgeTime)
        {
            _player._dodgeing = false;
            _curTime = 0;
        }
    }
}
