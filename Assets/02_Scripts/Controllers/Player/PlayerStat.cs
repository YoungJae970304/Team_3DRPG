using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStat : Stat
{
    public int _recoveryHp;

    public int _mp;
    public int _maxMp = 100;
    public int _recoveryMp;

    public float _dodgeSpeed = 15f;

    public int _level = 1;
    public int _maxExp;

    public int _sp;

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

            _maxHp += 50;
            _maxMp += 50;
            MaxEXP += 100;

            _hp = _maxHp;
            _mp = _maxMp;
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
