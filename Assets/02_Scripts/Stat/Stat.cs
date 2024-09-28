using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Stat : MonoBehaviour
{
    protected int _hp;
    protected int _maxHp;

    protected int _atk;
    protected int _def;

    protected float _moveSpeed;

    protected int _exp;
    protected int _gold;

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
}
