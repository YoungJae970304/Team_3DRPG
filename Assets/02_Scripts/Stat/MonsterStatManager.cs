using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatManager : ITotalStat
{
    public MonsterStat _mStat;
    public MonsterStat _equipStat;
    public MonsterStat _buffStat;

    public int HP
    {
        get { return Mathf.Max(0, _mStat.HP); }
        set
        {
            _mStat.HP = value;
        }
    }

    public int MaxHP { get { return Mathf.Max(0, _mStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP); } set { _buffStat.MaxHP = value; } }

    public int ATK { get { return Mathf.Max(0, _mStat.ATK + _equipStat.ATK + _buffStat.ATK); } set { _buffStat.ATK = value; } }

    public int DEF { get { return Mathf.Max(0, _mStat.DEF + _equipStat.DEF + _buffStat.DEF); } set { _buffStat.DEF = value; } }

    public float MoveSpeed { get { return Mathf.Max(0, _mStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } set { _buffStat.MoveSpeed = value; } }

    public int RecoveryHP { get { return Mathf.Max(0, _mStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP); } set { _buffStat.RecoveryHP = value; } }

    public int MP { get { return Mathf.Max(0, _mStat.MP); } set { _mStat.MP = value; } }

    public int MaxMP { get { return Mathf.Max(0, _mStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } set { _buffStat.MaxMP = value; } }

    public int RecoveryMP { get { return Mathf.Max(0, _mStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP); } set { _buffStat.RecoveryMP = value; } }

    public float ChaseRange { get { return _mStat.ChaseRange; } set { ChaseRange = value; } }
    public float ReturnRange { get { return _mStat.ReturnRange; } set { _mStat.ReturnRange = value; } }
    public float AttackRange { get { return _mStat.AttackRange; } set { _mStat.AttackRange = value; } }
    public float AwayRange { get { return _mStat.AwayRange; } set { _mStat.AwayRange = value; } }

    public float AtkDelay { get { return _mStat.AtkDelay; } set { _mStat.AtkDelay = value; } }

}
