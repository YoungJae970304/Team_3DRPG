using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStat : Stat
{
    int _recoveryHp;

    int _mp;
    int _maxMp;
    int _recoveryMp;

    float _dodgeSpeed;

    int _level = 1;
    int _maxExp;

    int _sp;

    public int RecoveryHP 
    { 
        get 
        { 
            return _recoveryHp; 
        } 
        set 
        {
            _recoveryHp = value;

            while (true)
            {
                //HP += RecoveryHP;
            }
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

            MaxHP += 50;
            MaxMP += 50;
            MaxEXP += 100;

            HP = MaxHP;
            MP = MaxMP;
            SP += 5;
        }
    }

    public override int EXP
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;

            if (_exp >= MaxEXP)
            {
                _exp = _exp - MaxEXP;
                Level++;
            }
        }
    }

    public int MaxEXP { get { return _maxExp; } set { _maxExp = value; } }
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


}
