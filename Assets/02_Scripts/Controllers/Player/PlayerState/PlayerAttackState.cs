using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(Player player) : base(player) 
    {
        _player = player;
    }

    public override void OnStateEnter()
    {
        Logger.Log("���� ���� ����");
        _player._attacking = true;
    }

    public override void OnStateUpdate()
    {
        Logger.Log("���� ���� ������Ʈ");
        _player.Attack();
    }

    public override void OnStateExit()
    {
        Logger.Log("���� ���� Exit");
    }
}
