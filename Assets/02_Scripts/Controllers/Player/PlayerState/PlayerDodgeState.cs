using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    public PlayerDodgeState(Player player) : base(player) { }

    float _curTime;

    public override void OnStateEnter()
    {
        Logger.Log("회피 진입");

        _player._dodgeing = true;
        _player._cc.enabled = false;
    }

    public override void OnStateUpdate()
    {
        Logger.Log("회피 업데이트");

        Dodge();
        DodgeTimer();
    }

    public override void OnStateExit()
    {
        Logger.Log("회피 탈출");

        _player._cc.enabled = true;
    }

    void Dodge()
    {
        // 회피 방향
        _player._moveDir = _player._playerModel.transform.forward * _player._playerStat.DodgeSpeed * Time.deltaTime;
        // 회피
        _player.transform.position += _player._moveDir;
    }

    // 일정 시간 후 회피 상태 해제
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
