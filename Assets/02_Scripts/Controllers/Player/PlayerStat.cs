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

    public static PlayerStat operator +(PlayerStat stat1, PlayerStat stat2)
    {
        return new PlayerStat
        {
            _recoveryHp = stat1._recoveryHp + stat2._recoveryHp,
            _hp = stat1._hp + stat2._hp,
            _maxHp = stat1._maxHp + stat2._maxHp,
            _playerMp = stat1._playerMp + stat2._playerMp,
            _playerMaxMp = stat1._playerMaxMp + stat2._playerMaxMp,
            _recoveryMp = stat1._recoveryMp + stat2._recoveryMp,
            _dodgeSpeed = stat1._dodgeSpeed + stat2._dodgeSpeed,
            _atk = stat1._atk + stat2._atk,
            _def = stat1._def + stat2._def,
            _moveSpeed = stat1._moveSpeed + stat2._moveSpeed,
            _level = Mathf.Max(stat1._level, stat2._level),
            _maxExp = Mathf.Max(stat1._maxExp, stat2._maxExp),
            _exp = Mathf.Max(stat1._exp, stat2._exp),
        };
    }

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

            MaxHP += 50;
            PlayerMaxMP += 50;
            MaxEXP += 100;

            HP = MaxHP;
            PlayerMP = PlayerMaxMP;
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
