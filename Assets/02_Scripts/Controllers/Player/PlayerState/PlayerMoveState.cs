using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    public PlayerMoveState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player.AtkCount = 0;
        _player._playerAnim.SetBool("Run", true);

        switch (_player._playerCam._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                _player._playerAnim.SetBool("ZoomMode", false);
                break;

            case Define.CameraMode.ZoomView:
                _player._playerAnim.SetBool("ZoomMode", true);
                break;
        }
    }

    public override void OnStateUpdate()
    {
        switch (_player._playerCam._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                _player._playerAnim.SetFloat("PosX", _player._cc.velocity.magnitude);
                break;

            case Define.CameraMode.ZoomView:
                _player._playerAnim.SetFloat("PosX", _player._rotDir.x);
                _player._playerAnim.SetFloat("PosY", _player._rotDir.z);
                break;
        }
    }

    public override void OnStateFixedUpdate()
    {
        Move();
    }

    public override void OnStateExit()
    {
        Logger.Log("무브상태 Exit");
        MoveStop();
    }

    void Move()
    {
        if (_player._rotDir != Vector3.zero)
        {
            switch (_player._playerCam._cameraMode)
            {
                case Define.CameraMode.QuarterView:
                    // 실제 회전
                    Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
                    _player._playerModel.rotation = Quaternion.Slerp(_player._playerModel.rotation, targetRot, _player._rotSpeed);

                    // 이동 방향
                    _player._moveDir = _player._playerModel.forward * _player._playerStat.MoveSpeed * Time.fixedDeltaTime;
                    break;

                case Define.CameraMode.ZoomView:
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
        _player._moveDir = Vector3.zero;
        _player._playerAnim.SetBool("Run", false);
    }
}
