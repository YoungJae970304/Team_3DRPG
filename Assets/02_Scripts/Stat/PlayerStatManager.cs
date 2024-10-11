using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : ITotalStat
{
    public PlayerStat _originStat;
    public PlayerStat _equipStat;
    public PlayerStat _buffStat;

    public int HP
    {
        get { return Mathf.Max(0, _originStat.HP); }
        set
        {
            _originStat.HP = value;
        }
    }

    public int MaxHP { get { return Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP); } set { _buffStat.MaxHP = value; } }

    public int ATK { get { return Mathf.Max(0, _originStat.ATK + _equipStat.ATK + _buffStat.ATK); } set { _buffStat.ATK = value; } }

    public int DEF { get { return Mathf.Max(0, _originStat.DEF + _equipStat.DEF + _buffStat.DEF); } set { _buffStat.DEF = value; } }

    public float MoveSpeed { get { return Mathf.Max(0, _originStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } set { _buffStat.MoveSpeed = value; } }

    public int RecoveryHP { get { return Mathf.Max(0, _originStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP); } set { _buffStat.RecoveryHP = value; } }

    public int MP { get { return Mathf.Max(0, _originStat.MP); } set { _originStat.MP = value; } }

    public int MaxMP { get { return Mathf.Max(0, _originStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } set { _buffStat.MaxMP = value; } }

    public int RecoveryMP { get { return Mathf.Max(0, _originStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP); } set { _buffStat.RecoveryMP = value; } }

    public float DodgeSpeed { get { return Mathf.Max(0, _originStat.DodgeSpeed + _equipStat.DodgeSpeed + _buffStat.DodgeSpeed); } }
}
