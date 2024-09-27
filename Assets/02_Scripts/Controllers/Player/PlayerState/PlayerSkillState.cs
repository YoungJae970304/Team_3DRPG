using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    public PlayerSkillState(Player player) : base(player) { }

    public override void OnStateEnter()
    {
        _player._skillUsing = true;
        switch(_player._skillIndex)
        {
            case 1:
                Logger.Log("E스킬 발동");
                break;
            case 2:
                Logger.Log("R스킬 발동");
                break;
        }
    }

    public override void OnStateUpdate()
    {
        // 시간 지나면 스킬 상태 끝나게 (임시)
        _player.Skill();
    }

    public override void OnStateExit()
    {
        Logger.Log("스킬상태 Exit");
        _player._skillUsing = false;
    }
}
