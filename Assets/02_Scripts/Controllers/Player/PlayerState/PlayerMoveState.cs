using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        Logger.Log("������� ����");
    }

    public override void OnStateUpdate()
    {
        
    }

    public override void OnStateFixedUpdate()
    {
        Logger.Log("������� ������(Fixed)");
        Move();
    }

    public override void OnStateExit()
    {
        Logger.Log("������� ��");
        MoveStop();
    }

    void Move()
    {
        if (_player._rotDir != Vector3.zero)
        {
            // ȸ�� ���� ����ȭ
            _player._rotDir.Normalize();

            // ���� ȸ��
            Quaternion targetRot = Quaternion.LookRotation(_player._rotDir);
            _player._playerModel.rotation = Quaternion.Slerp(_player._playerModel.rotation, targetRot, _player._rotSpeed);

            // �̵� ����
            _player._moveDir = _player._playerModel.forward * _player._playerStat.MoveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            // ���ߴ� ����
            _player._moveDir = Vector3.zero;
        }

        // ���� ���� �̵�
        _player._cc.Move(_player._moveDir);
    }

    void MoveStop()
    {
        _player._moveDir = Vector3.zero ;
    }
}
