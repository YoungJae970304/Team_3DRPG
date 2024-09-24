using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        Debug.Log("무브상태 진입");
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        Debug.Log("무브상태 진행중(Fixed)");
        Move();
    }

    public override void OnStateExit()
    {
        Debug.Log("무브상태 끝");
        MoveStop();
    }

    void Move()
    {
        if (_player._rotDir != Vector3.zero)
        {
            // 회전 방향 정규화
            _player._rotDir.Normalize();

            // 실제 회전
            Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
            _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation, targetRot, _player._rotSpeed);

            // 이동 방향
            _player._moveDir = _player.transform.forward * _player._playerStat._moveSpeed * Time.fixedDeltaTime;
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
