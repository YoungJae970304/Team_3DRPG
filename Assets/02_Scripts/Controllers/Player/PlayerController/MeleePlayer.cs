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
