using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : Monster
{
    private enum State
    {
        Idle,
        Move,
        Attack,
        Damage,
        Return,
        Die,
    }
    private State _curState;
    //NavMeshAgent nav; //여기서 안쓸듯?
    private MonsterFSM _monFSM;
    //MonsterStat _mStat;
    //GameObject _player;
    Vector3 _originPos;

    Dictionary<State, MonsterBaseState> States = new Dictionary<State, MonsterBaseState>();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curState = State.Idle;
        _monFSM = new MonsterFSM(new SlimeIdleState(this));
        _originPos = transform.position;
        #region 상태딕셔너리 초기화
        States.Add(State.Idle, new SlimeIdleState(this));
        States.Add(State.Move, new MoveState(this));
        States.Add(State.Attack, new AttackState(this));
        States.Add(State.Damage, new DamagedState(this));
        States.Add(State.Return, new ReturnState(this));
        States.Add(State.Die, new DieState(this));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SlimeState()
    {
        switch (_curState)
        {
            case State.Idle:
                if (DamageToPlayer())
                {
                    ChangeState(State.Damage);  
                }
                break;
            case State.Damage:
                if (CanAttackPlayer())
                    ChangeState(State.Attack);
                else if (_mStat.Hp <= 0)
                    ChangeState(State.Die);
                else
                    ChangeState(State.Move);
                break;
            case State.Move:
                if (CanAttackPlayer())
                    ChangeState(State.Attack);
                break;
            case State.Attack:
                if (!CanAttackPlayer())
                {
                    if (!ReturnOrigin())
                    {
                        ChangeState(State.Move);
                    }
                    else
                    {
                        ChangeState(State.Return);
                    }
                }
                break;
            case State.Return:
                if ((_originPos - transform.position).magnitude <= 0.1f)
                    ChangeState(State.Idle);
                break;
            case State.Die:
                DropItem();
                Destroy(gameObject, 1.5f);
                break;


        }
    }

    private void ChangeState(State nextState)
    {
        _curState = nextState;
        _monFSM.ChangeState(States[_curState]);
        States[_curState].OnStateEnter();
    }

    private bool DamageToPlayer()
    {
        return _mStat.ReturnRange < (_player.transform.position - transform.position).magnitude;
    }

    private bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _mStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    private bool ReturnOrigin()
    {
        return _mStat.ReturnRange < (_originPos - transform.position).magnitude;
    }
    private void DropItem()
    {

    }
}
