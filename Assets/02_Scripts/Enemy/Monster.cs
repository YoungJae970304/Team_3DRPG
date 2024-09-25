using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IDamageAlbe
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damage,
        Return,
        Die,
    }
    private State _state;
    public GameObject _player;
    public NavMeshAgent _nav;
    public MonsterStat _mStat;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BaseState()
    {
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Move:
                break;
            case State.Attack:
                break;
            case State.Skill:
                break;
            case State.Damage:
                break;
            case State.Return:
                break;
            case State.Die:
                break;
        }
    }

    public virtual void Damaged(int amount)
    {

    }


}
