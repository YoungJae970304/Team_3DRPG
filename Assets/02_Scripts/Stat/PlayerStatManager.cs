using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : ITotalStat
{
    public PlayerStat _originStat;
    public PlayerStat _equipStat;
    public PlayerStat _buffStat;

    /*
    public int Level
    {
        get { return Mathf.Max(0, _originStat.Level); }
        set
        {
            _originStat.Level = value;
        }
    }
    */
    int _level = 1;
    int _maxExp;

    int _sp;
    int _spAddAmount;


    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            if (value < _level) return;

            _level = value;
        }
    }
    public int SP
    {
        get
        {
            return _sp;
        }
        set
        {
            _sp = Mathf.Max(value, 0);
        }
    }

    public int SpAddAmount
    {
        get
        {
            return _spAddAmount;
        }
        set
        {
            _spAddAmount = Mathf.Max(value, 0);
        }
    }

    int _exp;
    public int EXP
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;

            if (EXP >= MaxEXP)
            {
                EXP = EXP - MaxEXP;
                Level++;
                Managers.DataTable.PlayerStatUpdate();
            }
        }
    }
    public int MaxEXP { get { return _maxExp; } set { _maxExp = value; Logger.LogError("최대경험치 : " + MaxEXP.ToString()); } }
    //public int Level;
    //public int MaxEXP;
    //public int SpAddAmount;

    public int HP
    {
        get { return Mathf.Max(0, _originStat.HP); }
        set
        {
            _originStat.HP = value;
        }
    }

    public int MaxHP { get { return Mathf.Max(0, _originStat.MaxHP + _equipStat.MaxHP + _buffStat.MaxHP); } }

    public int ATK { get { return Mathf.Max(0, _originStat.ATK + _equipStat.ATK + _buffStat.ATK); } }

    public int DEF { get { return Mathf.Max(0, _originStat.DEF + _equipStat.DEF + _buffStat.DEF); } }

    public float MoveSpeed { get { return Mathf.Max(0, _originStat.MoveSpeed + _equipStat.MoveSpeed + _buffStat.MoveSpeed); } }

    public int RecoveryHP { get { return Mathf.Max(0, _originStat.RecoveryHP + _equipStat.RecoveryHP + _buffStat.RecoveryHP); } }

    public int MP { get { return Mathf.Max(0, _originStat.MP); } set { _originStat.MP = value; } }

    public int MaxMP { get { return Mathf.Max(0, _originStat.MaxMP + _equipStat.MaxMP + _buffStat.MaxMP); } }

    public int RecoveryMP { get { return Mathf.Max(0, _originStat.RecoveryMP + _equipStat.RecoveryMP + _buffStat.RecoveryMP); } }

    //public int EXP { get { return Mathf.Max(0, _originStat.EXP); } set { _originStat.EXP = value; } }

    public int Gold { get { return Mathf.Max(0, _originStat.Gold); } set { _originStat.Gold = value; } }

    //public int MaxEXP { get { return Mathf.Max(0, _originStat.MaxEXP); } set { _originStat.MaxEXP = value; } }

    public float DodgeSpeed { get { return Mathf.Max(0, _originStat.DodgeSpeed + _equipStat.DodgeSpeed + _buffStat.DodgeSpeed); } }

    //public int SpAddAmount { get { return Mathf.Max(0, _originStat.SpAddAmount); } set { _originStat.SpAddAmount = value; } }
}
