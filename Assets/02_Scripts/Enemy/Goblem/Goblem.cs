using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblem : Monster, IDamageAlbe
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
    //NavMeshAgent nav; //���⼭ �Ⱦ���?
    private MonsterFSM _monFSM;
    //MonsterStat _mStat;
    //GameObject _player;
    public Vector3 _originPos;
    public float _attackDelay = 3f;
    Dictionary<State, MonsterBaseState> States = new Dictionary<State, MonsterBaseState>();
    public GoblemStat _gStat;
    private void Awake()
    {
        _gStat = GetComponent<GoblemStat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curState = State.Idle;
        _monFSM = new MonsterFSM(new GoblemIdleState(this));
        _originPos = transform.position;
        _nav = GetComponent<NavMeshAgent>();

        #region ���µ�ųʸ� �ʱ�ȭ
        States.Add(State.Idle, new GoblemIdleState(this));
        States.Add(State.Move, new GoblemMoveState(this));
        States.Add(State.Attack, new GoblemAttackState(this));
        States.Add(State.Damage, new GoblemDamagedState(this));
        States.Add(State.Return, new GoblemReturnState(this));
        States.Add(State.Die, new GoblemDieState(this));
        #endregion
        States[State.Idle].OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        GoblemState();
        States[_curState].OnStateUpdate();
        Logger.Log(CanSeePlayer().ToString());
    }
    public void GoblemState()
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
                if ((_originPos - transform.position).magnitude <= 1f)
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
        return _gStat.ReturnRange > _player.transform.position.magnitude;
    }
    public bool CanAttackPlayer()
    {
        //�����Ÿ� üũ ����
        return _gStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool CanSeePlayer()
    {
        return _gStat.ChaseRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _gStat.ReturnRange < (_originPos - transform.position).magnitude;
    }
    public void DropItem()
    {

    }

    public void Damaged(int amount)
    {
        if (_curState != State.Return)
        {
            if (DamageToPlayer())
            {
                _gStat.Hp -= amount;
                ChangeState(State.Damage);
            }
        }
    }
    #region return���� �ű� �Լ�
    public void ReturnHeal()
    {
        _gStat.Hp = _gStat.MaxHp;
    }
    #endregion
    public void GoblemDie()
    {
        Destroy(gameObject, 2f);
    }
}
