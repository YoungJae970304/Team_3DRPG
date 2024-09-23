using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class Slime : Monster, IDamageAlbe
{
    public enum State
    {
        Idle,
        Move,
        Attack,
        Damage,
        Return,
        Die,
    }
    public State _curState;
    //NavMeshAgent nav; //여기서 안쓸듯?
    private MonsterFSM _monFSM;
    //MonsterStat _mStat;
    //GameObject _player;
    public Vector3 _originPos;
    public float _timer = 0f;
    public float _attackDelay = 3f;
    Dictionary<State, MonsterBaseState> States = new Dictionary<State, MonsterBaseState>();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _curState = State.Idle;
        _monFSM = new MonsterFSM(new SlimeIdleState(this));
        _originPos = transform.position;
        _nav = GetComponent<NavMeshAgent>();
        #region 상태딕셔너리 초기화
        States.Add(State.Idle, new SlimeIdleState(this));
        States.Add(State.Move, new SlimeMoveState(this));
        States.Add(State.Attack, new SlimeAttackState(this));
        States.Add(State.Damage, new SlimeDamagedState(this));
        States.Add(State.Return, new SlimeReturnState(this));
        States.Add(State.Die, new SlimeDieState(this));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerChack())
        {
            InvokeRepeating("AttackTimer", 1f, 1f);
        }
        ReturnHeal();
        SlimeState();
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
                else
                {
                    States[State.Idle].OnStateEnter();
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

    public void ChangeState(State nextState)
    {
        _curState = nextState;
        _monFSM.ChangeState(States[_curState]);
        States[_curState].OnStateEnter();
    }

    public bool DamageToPlayer()
    {
        return _mStat.ReturnRange < (_player.transform.position - transform.position).magnitude;
    }
    public bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _mStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _mStat.ReturnRange < (_originPos - transform.position).magnitude;
    }
    public void DropItem()
    {

    }

    public void Damaged(int amount)
    {
        if(_curState != State.Return)
        {
            if (DamageToPlayer())
            {
                _mStat.Hp -= amount;
                ChangeState(State.Damage);
            }
        }
        else
        {

        }
        
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
    {
        yield return new WaitForSeconds(delay);

        try//이걸 실행해보고 문제가 없다면 실행
        {

            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude;
            GetComponent<Rigidbody>().
            AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);
           
        }
        catch (MissingReferenceException e)// 문제가 있다면 에러메세지 출력
        {
            Debug.Log(e.ToString());
        }
        //예외처리문
    }
    public void AttackTimer()
    {
        _timer++;
        if (_timer > _attackDelay)
        {
            States[_curState].OnStateUpdate();
        }
    }
    public bool TimerChack()
    {
        return _curState == State.Attack;
    }
    public bool ReturnChack()
    {
        return _curState == State.Return;
    }
    public void ReturnHeal()
    {
        if (ReturnChack())
        {
            _mStat.Hp = _mStat.MaxHp;
        }
    }
    public void SlimeDie()
    {
        Destroy(gameObject, 2f);
    }
}
