using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    int _playerHp;
    int _playerMaxHp = 100;
    int _recoveryHp;

    int _playerMp;
    int _playerMaxMp = 100;
    int _recoveryMp;

    int _atk;
    int _def;

    float _moveSpeed = 10f;
    float _dodgeSpeed = 15f;

    int _level = 1;
    int _exp;
    int _maxExp;

    int _sp;
    int _gold;

    public int PlayerHP 
    {
        get { return _playerHp;}
        set
        {
            _playerHp = value;

            _playerHp = Mathf.Clamp(_playerHp, 0, _playerMaxHp);
        }
    }
    public int PlayerMaxHP { get { return _playerMaxHp; } set { _playerMaxHp = value; } }
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

    public int ATK { get { return _atk; } set { _atk = value; } }
    public int DEF { get { return _def; } set { _def = value; } }

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
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

            PlayerHP = PlayerMaxHP;
            PlayerMP = PlayerMaxMP;
            SP += 5;
        } 
    }

    public int EXP 
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
    public int SP { get { return _sp; } set { _sp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
}
