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
        Debug.Log("무브상태 진행중");
        Move();
    }

    public override void OnStateFixedUpdate()
    {
        Debug.Log("무브상태 진행중(Fixed)");
        
    }

    public override void OnStateExit()
    {
        Debug.Log("무브상태 끝");
    }

    void Move()
    {
        _player._cc.Move(_player._playerInput._moveDir);
    }
}
