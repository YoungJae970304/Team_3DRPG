using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager
{
    public PlayerStat _playerStat;
    public PlayerStat _equipStat;
    public PlayerStat _buffStat;

    public int HP { get { return Mathf.Max(0, _playerStat._hp + _equipStat._hp + _buffStat._hp); } }

    public int MaxHP { get { return Mathf.Max(0, _playerStat._maxHp + _equipStat._maxHp + _buffStat._maxHp); } }

    public int ATK { get { return Mathf.Max(0, _playerStat._atk + _equipStat._atk + _buffStat._atk); } }

    public int DEF{ get { return Mathf.Max(0, _playerStat._def + _equipStat._def + _buffStat._def); } }

    public float MoveSpeed { get { return Mathf.Max(0, _playerStat._moveSpeed + _equipStat._moveSpeed + _buffStat._moveSpeed); } }

    public int RecoveryHP { get { return Mathf.Max(0, _playerStat._recoveryHp + _equipStat._recoveryHp + _buffStat._recoveryHp); } }

    public int MP { get { return Mathf.Max(0, _playerStat._mp + _equipStat._mp + _buffStat._mp); } }

    public int MaxMP { get { return Mathf.Max(0, _playerStat._maxMp + _equipStat._maxMp + _buffStat._maxMp); } }

    public int RecoveryMP { get { return Mathf.Max(0, _playerStat._recoveryMp + _equipStat._recoveryMp + _buffStat._recoveryMp); } }

    public float DodgeSpeed { get { return Mathf.Max(0, _playerStat._dodgeSpeed + _equipStat._dodgeSpeed + _buffStat._dodgeSpeed); } }
}
