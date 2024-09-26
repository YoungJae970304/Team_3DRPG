using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBear : Monster, IDamageAlbe
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
    public BearStat _bStat;
    private void Awake()
    {
        _bStat = GetComponent<BearStat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curState = State.Idle;
        _monFSM = new MonsterFSM(new BossBearIdleState(this));
        _originPos = transform.position;
        _nav = GetComponent<NavMeshAgent>();

        #region 상태딕셔너리 초기화
        States.Add(State.Idle, new BossBearIdleState(this));
        States.Add(State.Move, new BossBearMoveState(this));
        States.Add(State.Attack, new BossBearAttackState(this));
        States.Add(State.Damage, new BossBearDamagedState(this));
        States.Add(State.Return, new BossBearReturnState(this));
        States.Add(State.Die, new BossBearDieState(this));
        States.Add(State.Skill, new BossBearSkillState(this));
        #endregion
        States[State.Idle].OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        BossBearState();
        States[_curState].OnStateUpdate();
        //Logger.Log(CanSeePlayer().ToString());
    }
    public void BossBearState()
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
            case State.Skill:
                if (CanAttackPlayer())
                    ChangeState(State.Attack);
                else
                {
                    ChangeState(State.Move);
                }
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
        return _bStat.ReturnRange > _player.transform.position.magnitude;
    }
    public bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _bStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool CanSeePlayer()
    {
        return _bStat.ChaseRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _bStat.ReturnRange < (_originPos - transform.position).magnitude;
    }



    public override void Damaged(int amount)
    {
        if (_curState != State.Return)
        {
            if (DamageToPlayer())
            {
                _bStat.Hp -= amount;
                ChangeState(State.Damage);
            }
        }
    }
   
    public void BossBearDie()
    {
        Destroy(gameObject, 2f);
    }
}
