using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(Player player) : base(player) { }

    float _curTime;

    public override void OnStateEnter()
    {
        Debug.Log("ȸ�� ����");

        _player._dodgeing = true;
        _player._cc.enabled = false;
    }

    public override void OnStateUpdate()
    {
        Debug.Log("ȸ�� ������Ʈ");

        Dodge();
        DodgeTimer();
    }

    public override void OnStateExit()
    {
        Debug.Log("ȸ�� Ż��");

        _player._cc.enabled = true;
    }

    void Dodge()
    {
        // ȸ�� ����
        _player._moveDir = _player.transform.forward * _player._playerStat._dodgeSpeed * Time.deltaTime;
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
