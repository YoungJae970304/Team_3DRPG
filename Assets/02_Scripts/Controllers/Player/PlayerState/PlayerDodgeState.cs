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
        Logger.Log("ȸ�� Exit");

        _player._cc.enabled = true;
    }

    void Dodge()
    {
        // ȸ�� ����
        _player._moveDir = _player._playerModel.transform.forward * _player._playerStat.DodgeSpeed * Time.deltaTime;
        // ȸ��
        _player.transform.position += _player._moveDir;
    }

    // ���� �ð� �� ȸ�� ���� ����
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
