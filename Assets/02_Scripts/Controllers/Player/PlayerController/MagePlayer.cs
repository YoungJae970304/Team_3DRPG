using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePlayer : Player
{
    [Header("평타 투사체 생성 위치")]
    public Transform _mageBallPos;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game._playerType = Define.PlayerType.Mage;
    }

    public override void Attack()
    {
        GameObject go = Managers.Resource.Instantiate("Player/MageBall");
        go.transform.forward = _playerModel.forward;
        go.transform.position = _mageBallPos.position;

        int randVal = Random.Range(1, 3);

        if (randVal == 1)
        {
            Managers.Sound.Play("Mage/mage_atk_voice_1", Define.Sound.Effect);
        }
        else
        {
            Managers.Sound.Play("Mage/mage_atk_voice_2", Define.Sound.Effect);
        }
    }

    public override void Special()
    {
        _playerCam.CamModeChange();
    }
}
