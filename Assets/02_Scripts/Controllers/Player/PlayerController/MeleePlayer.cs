using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MeleePlayer : Player
{
    [Header("검기 생성 위치")]
    public Transform _swordAuraPos;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game._playerType = Define.PlayerType.Melee;
    }

    public override void ApplyDamage(int damage)
    {
        if (_hitMobs.Count == 0) return;

        foreach (var mob in _hitMobs)
        {
            if (mob.TryGetComponent<IDamageAlbe>(out var damageable))
            {
                damageable.Damaged(damage);
                _effectController.HitEffectsOn("MeleeNormalHit", mob.transform);
            }
        }

        _hitMobs.Clear();
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
                    _effectController.HitEffectsOn("MeleeNormalHit", Managers.Game._monsters[i].transform);
                }
            }
        }
    }

    public override void Attack()
    {
        ApplyDamage(_playerStatManager.ATK);
    }

    public override void Special()
    {
        AtkCount = 0;

        _playerInput.InputBufferInsert(AtkCount);
    }
}
