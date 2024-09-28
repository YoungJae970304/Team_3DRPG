using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    int _recoveryHp;

    int _playerMp;
    int _playerMaxMp = 100;
    int _recoveryMp;

    float _dodgeSpeed = 15f;

    int _level = 1;
    int _maxExp;

    int _sp;

    public int RecoveryHP { get { return _recoveryHp; } set { _recoveryHp = value; } }

    public int PlayerMP
    {
        get { return _playerMp; }
        set
        {
            _playerMp = value;

            _playerMp = Mathf.Clamp(_playerMp, 0, _playerMaxMp);
        }
    }
    public int PlayerMaxMP { get { return _playerMaxMp; } set { _playerMaxMp = value; } }
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

            HP = MaxHP;
            PlayerMaxMP = PlayerMaxMP;
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
            _sp = value;

            _sp = Mathf.Min(_sp, 0);
        } 
    }
}
