using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    protected float _chaseRange;
    [SerializeField]
    protected float _returnRange;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected float _awayRange;
    [SerializeField]
    protected float _atkDelay;

    public float ChaseRange { get { return _chaseRange; } set { _chaseRange = value; } }
    public float ReturnRange { get { return _returnRange; } set { _returnRange = value; } }
    public float AttackRange {  get { return _attackRange; } set { _attackRange = value; } }
    public float AwayRange { get { return _awayRange; } set { _awayRange = value; } }

    public float AtkDelay { get { return _atkDelay; } set { _atkDelay = value; } }
}
