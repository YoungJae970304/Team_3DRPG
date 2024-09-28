using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : BaseState
{
    public PlayerSkillState(Player player, Monster monster, Stat stat) : base(player, monster, stat) { }

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

        _player._skillBase.SkillEnter(_stat);
    }

    public override void OnStateUpdate()
    {
        // 시간 지나면 스킬 상태 끝나게 (임시)
        _player.Skill();
        
        _player._skillBase.SkillStay(_stat);
    }

    public override void OnStateExit()
    {
        Logger.Log("스킬상태 Exit");

        _player._skillBase.SkillExit(_stat);
        _player._skillUsing = false;
    }
}
