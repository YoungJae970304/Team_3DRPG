using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatManager : MonoBehaviour, ITotalStat
{
    public MonsterStat _mStat;
    public MonsterStat _buffStat;

    public int HP
    {
        get { return Mathf.Max(0, _mStat.HP); }
        set
        {
            _mStat.HP = value;
        }
    }

    public int MaxHP { get { return Mathf.Max(0, _mStat.MaxHP  + _buffStat.MaxHP); } }

    public int ATK { get { return Mathf.Max(0, _mStat.ATK + _buffStat.ATK); } }

    public int DEF { get { return Mathf.Max(0, _mStat.DEF + _buffStat.DEF); } }

    public float MoveSpeed { get { return Mathf.Max(0, _mStat.MoveSpeed + _buffStat.MoveSpeed); } }

    public int RecoveryHP { get { return Mathf.Max(0, _mStat.RecoveryHP + _buffStat.RecoveryHP); } }

    public int MP { get { return Mathf.Max(0, _mStat.MP); } set { _mStat.MP = value; } }

    public int MaxMP { get { return Mathf.Max(0, _mStat.MaxMP + _buffStat.MaxMP); } }

    public int RecoveryMP { get { return Mathf.Max(0, _mStat.RecoveryMP + _buffStat.RecoveryMP); } }

    public int EXP { get { return _mStat.EXP; }  set { _mStat.EXP = value; } }

    public int Gold { get { return _mStat.Gold; } set { _mStat.Gold = value; } }

    public float ChaseRange { get { return _mStat.ChaseRange; } set { _mStat.ChaseRange = value; } }
    public float ReturnRange { get { return _mStat.ReturnRange; } set { _mStat.ReturnRange = value; } }
    public float AttackRange { get { return _mStat.AttackRange; } set { _mStat.AttackRange = value; } }
    public float AwayRange { get { return _mStat.AwayRange; } set { _mStat.AwayRange = value; } }

    public float AtkDelay { get { return _mStat.AtkDelay; } set { _mStat.AtkDelay = value; } }
}
