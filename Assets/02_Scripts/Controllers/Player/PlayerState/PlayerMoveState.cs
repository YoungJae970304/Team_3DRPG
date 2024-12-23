using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : BaseState
{
    public PlayerMoveState(Player player, Monster monster, ITotalStat stat) : base(player, monster, stat) { }

    public override void OnStateEnter()
    {
        _player.AtkCount = 0;
        _player._playerAnim.SetBool("Run", true);
    }

    public override void OnStateUpdate()
    {
        switch (_player._playerCam._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                _player._playerAnim.SetFloat("PosX", _player._cc.velocity.magnitude);
                break;

            case Define.CameraMode.ZoomView:
                // 애니메이션 전환은 GetAxis가 자연스러워서 GetAxis 채용
                _player._playerAnim.SetFloat("PosX", Input.GetAxis("Horizontal"));
                _player._playerAnim.SetFloat("PosY", Input.GetAxis("Vertical"));
                break;
        }

        Move();
    }

    public override void OnStateExit()
    {
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
                    _player._moveDir = _player._playerModel.forward * _player._playerStatManager.MoveSpeed * Time.deltaTime;
                    break;

                case Define.CameraMode.ZoomView:
                    _player._moveDir = _player._rotDir * _player._playerStatManager.MoveSpeed * Time.deltaTime;
                    break ;
            }
        }
        else
        {
            // 멈추는 조건
            _player._moveDir = Vector3.zero;
        }

        // 실제 최종 이동
        _player._cc.Move(new Vector3(_player._moveDir.x, 0, _player._moveDir.z));
    }

    void MoveStop()
    {
        _player._moveDir = Vector3.zero;
        _player._playerAnim.SetBool("Run", false);
    }
}
