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

        RandSoundsPlay("Mage/mage_atk_voice_1", "Mage/mage_atk_voice_2");
    }

    public override void AreaDamage(float range, int damage)
    {
        Vector3 playerPos = Managers.Game._player.transform.position;

        for (int i = 0; i < Managers.Game._monsters.Count; i++)
        {
            if (Vector3.Distance(playerPos, Managers.Game._monsters[i].transform.position) < range)
            {
                if (Managers.Game._monsters[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.Damaged(damage);
                    _effectController.HitEffectsOn("MageSnowhit", Managers.Game._monsters[i].transform);
                    Managers.Sound.Play("Mage/mage_skill1_hit");
                }
            }
        }
    }

    public override void Special()
    {
        _playerCam.CamModeChange();
    }
}
