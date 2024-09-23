using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        Debug.Log("������� ����");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("������� ������");
        Move();
    }

    public override void OnStateFixedUpdate()
    {
        Debug.Log("������� ������(Fixed)");
        
    }

    public override void OnStateExit()
    {
        Debug.Log("������� ��");
    }

    void Move()
    {
        _player._cc.Move(_player._playerInput._moveDir);
    }
}
