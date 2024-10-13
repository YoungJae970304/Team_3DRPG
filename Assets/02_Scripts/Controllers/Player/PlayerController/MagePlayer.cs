using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePlayer : Player
{
    [Header("평타 투사체 생성 위치")]
    public Transform _mageBallPos;

    protected override void Start()
    {
        base.Start();
        _playerType = Define.PlayerType.Mage;
    }

    public override void Attack()
    {
        GameObject go = Managers.Resource.Instantiate("Player/MageBall");
        go.transform.position = _mageBallPos.position;
    }

    public override void Skill()
    {
        
    }

    public override void Special()
    {
        _playerCam.CamModeChange();
    }
}
