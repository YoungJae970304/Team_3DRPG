using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        Move();
    }

    public override void OnStateExit()
    {
        Logger.Log("Move Exit");
        MoveStop();
    }

    void Move()
    {
        if (_player._rotDir != Vector3.zero)
        {
            switch (_player._playerCam._cameraMode)
            {
                case Define.CameraMode.QuarterView:
                    // 회전 방향 정규화  
                    _player._rotDir.Normalize();

                    // 실제 회전
                    Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
                    _player._playerModel.rotation = Quaternion.Slerp(_player._playerModel.rotation, targetRot, _player._rotSpeed);

                    // 이동 방향
                    _player._moveDir = _player._playerModel.forward * _player._playerStat.MoveSpeed * Time.fixedDeltaTime;
                    break;

                case Define.CameraMode.ZoomView:
                    _player._rotDir.Normalize();

                    _player._moveDir = _player._rotDir * _player._playerStat.MoveSpeed * Time.fixedDeltaTime;
                    break ;
            }
        }
        else
        {
            // 멈추는 조건
            _player._moveDir = Vector3.zero;
        }

        // 실제 최종 이동
        _player._cc.Move(_player._moveDir);
    }

    void MoveStop()
    {
        _player._moveDir = Vector3.zero ;
    }
}
