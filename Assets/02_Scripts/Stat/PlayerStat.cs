using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStat : Stat
{
    int _recoveryHp;

    int _mp;
    int _maxMp;
    int _recoveryMp;

    float _dodgeSpeed;

    //int _level = 1;
    //int _maxExp;

    //int _sp;
    //int _spAddAmount;

    public int RecoveryHP 
    { 
        get 
        { 
            return _recoveryHp; 
        } 
        set 
        {
            _recoveryHp = value;
        } 
    }

    public int MP
    {
        get { return _mp; }
        set
        {
            _mp = Mathf.Clamp(value, 0, _maxMp);
        }
    }
    public int MaxMP { get { return _maxMp; } set { _maxMp = value; } }
    public int RecoveryMP { get { return _recoveryMp; } set { _recoveryMp = value; } }

    public float DodgeSpeed { get { return _dodgeSpeed; } set { _dodgeSpeed = value; } }

    //public int Level
    //{
    //    get
    //    {
    //        return _level;
    //    }
    //    set
    //    {
    //        if (value < _level) return;

    //        _level = value;

    //        Managers.DataTable.PlayerLevelDataTable("CSVData", "Player_Level_Data_Table");

    //        HP = MaxHP;
    //        MP = MaxMP;
    //        SP += SpAddAmount;
    //    }
    //}

    //public override int EXP
    //{
    //    get
    //    {
    //        return _exp;
    //    }
    //    set
    //    {
    //        _exp = value;

    //        if (EXP >= MaxEXP)
    //        {
    //            EXP = EXP - MaxEXP;
    //            Level++;
    //        }
    //    }
    //}

    //public int MaxEXP { get { return _maxExp; } set { _maxExp = value; } }
    //public int SP
    //{
    //    get
    //    {
    //        return _sp;
    //    }
    //    set
    //    {
    //        _sp = Mathf.Max(value, 0);
    //    }
    //}

    //public int SpAddAmount
    //{
    //    get
    //    {
    //        return _spAddAmount;
    //    }
    //    set
    //    {
    //        _spAddAmount = Mathf.Max(value, 0);
    //    }
    //}
}
