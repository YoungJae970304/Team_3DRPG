using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPlayer : Player
{
    protected override void Start()
    {
        base.Start();
        _playerType = Define.PlayerType.Ranged;
    }

    public override void Skill()
    {
        SkillOffTimer(1f);
    }

    public override void Special()
    {
        _playerCam.CamModeChange();
    }
}
