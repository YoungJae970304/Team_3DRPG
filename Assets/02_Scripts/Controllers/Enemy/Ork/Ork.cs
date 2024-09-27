using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.AI;

public class Ork : Monster, IDamageAlbe
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        Damage,
        Return,
        Skill,
        Die,
    }
    public State _curState;
    //NavMeshAgent nav; //여기서 안쓸듯?
    private MonsterFSM _monFSM;
    //MonsterStat _mStat;
    //GameObject _player;
    public Vector3 _originPos;
    public float _attackDelay = 3f;
    Dictionary<State, MonsterBaseState> States = new Dictionary<State, MonsterBaseState>();
    public OrkStat _oStat;
    private void Awake()
    {
        _oStat = GetComponent<OrkStat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curState = State.Idle;
        _monFSM = new MonsterFSM(new OrkIdleState(this));
        _originPos = transform.position;
        _nav = GetComponent<NavMeshAgent>();

        #region 상태딕셔너리 초기화
        States.Add(State.Idle, new OrkIdleState(this));
        States.Add(State.Move, new OrkMoveState(this));
        States.Add(State.Attack, new OrkAttackState(this));
        States.Add(State.Damage, new OrkDamagedState(this));
        States.Add(State.Return, new OrkReturnState(this));
        States.Add(State.Die, new OrkDieState(this));
        #endregion
        States[State.Idle].OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        OrkState();
        States[_curState].OnStateUpdate();
        Logger.Log(CanSeePlayer().ToString());
        Logger.Log(ReturnOrigin().ToString());
    }
    public void OrkState()
    {
        switch (_curState)
        {
            case State.Idle:
                if (CanSeePlayer())
                    ChangeState(State.Move);
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
                else if (ReturnOrigin())
                    ChangeState(State.Return);
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
                if ((_originPos - transform.position).magnitude <= 3f)
                    ChangeState(State.Idle);
                break;
            case State.Die:
                break;


        }
    }

    public void ChangeState(State nextState)
    {
        _curState = nextState;
        _monFSM.ChangeState(States[_curState]);
        States[_curState].OnStateEnter();
    }

    public bool DamageToPlayer()
    {
        return _oStat.ReturnRange > _player.transform.position.magnitude;
    }
    public bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _oStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool CanSeePlayer()
    {
        return _oStat.ChaseRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _oStat.ReturnRange < (_originPos - transform.position).magnitude;
        
        //return _nav.remainingDistance < 2f;
    }


    public override void Damaged(int amount)
    {
        if (_curState != State.Return)
        {
            if (DamageToPlayer())
            {
                _oStat.Hp -= amount;
                ChangeState(State.Damage);
            }
        }
    }
    public override void Die(GameObject mob)
    {
        Destroy(mob, 2f);
    }
}
