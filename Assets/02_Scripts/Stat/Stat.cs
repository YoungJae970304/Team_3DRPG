using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Stat : MonoBehaviour
{
    public int _hp;
    public int _maxHp;

    public int _atk;
    public int _def;

    public float _moveSpeed;

    public int _exp;
    public int _gold;

    public virtual int EXP { get { return _exp; } set { _exp = value; } }
    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = Mathf.Max(value, 0);
        }
    }

    /*
    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;

            _hp = Mathf.Clamp(_hp, 0, _maxHp);
        }
    }

    public int MaxHP { get { return _maxHp; } set { _maxHp = value; } }

    public int ATK { get { return _atk; } set { _atk = value; } }
    public int DEF 
    { 
        get 
        { 
            return _def; 
        } 
        set 
        { 
            _def = value;

            _def = Mathf.Min(_def, 0);
        } 
    }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public virtual int EXP { get { return _exp; } set { _exp = value; } }
    public int Gold 
    { 
        get 
        { 
            return _gold; 
        } 
        set 
        { 
            _gold = value;

            _gold = Mathf.Min(_gold, 0);
        } 
    }
    */
}
