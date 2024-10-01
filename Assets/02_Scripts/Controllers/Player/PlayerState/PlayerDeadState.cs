using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : BaseState
{
    public PlayerDeadState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }
    public override void OnStateEnter()
    {
        Logger.Log("플레이어 사망");
    }

    public override void OnStateUpdate()
    {
        Logger.Log("사망상태 Update");
        // 임시로 1.5초후 비활성화
        DeadTimer(1.5f);
    }

    public override void OnStateExit()
    {
        Logger.Log("사망상태 Exit");
    }


    float curtime = 0;
    void DeadTimer(float targetTime)
    {
        curtime += Time.deltaTime;

        if (curtime > targetTime)
        {
            curtime = 0;

            _player.gameObject.SetActive(false);
        }
    }
}
