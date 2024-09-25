using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
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
    float _dodgeSpeed = 10f;

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
}
