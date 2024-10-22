using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Stat
{
    protected int _hp;
    protected int _maxHp;

    protected int _atk;
    protected int _def;

    protected float _moveSpeed;

    protected int _exp;
    protected int _gold;

    public virtual int HP
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Clamp(value, 0, _maxHp);
        }
    }

    public virtual int MaxHP { get { return _maxHp; } set { _maxHp = value; } }

    public virtual int ATK { get { return _atk; } set { _atk = value; } }
    public virtual int DEF 
    { 
        get 
        { 
            return _def; 
        } 
        set 
        { 
            _def = Mathf.Max(value, 0);
        } 
    }
    public virtual float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

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
}
