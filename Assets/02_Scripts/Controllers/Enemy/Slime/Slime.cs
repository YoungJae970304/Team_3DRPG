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
    public float _attackDelay = 3f;
    Dictionary<State, MonsterBaseState> States = new Dictionary<State, MonsterBaseState>();
    public SlimeStat _sStat;

    private void Awake()
    {
        _sStat = GetComponent<SlimeStat>();
    }
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

        States[State.Idle].OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        SlimeState();
        States[_curState].OnStateUpdate();
    }
    public void SlimeState()
    {
        switch (_curState)
        {
            case State.Idle:
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
        return _sStat.ReturnRange > _player.transform.position.magnitude;
    }
    public bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _sStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _sStat.ReturnRange < (_originPos - transform.position).magnitude;
    }

    public override void DropItem(string level, Transform mTransform, GameObject[] itemMenu)
    {
        //게임매니저에서 생성된 아이템을 pooling해야하는데 여기서는 아이템 키면서 가져와서 값만 넣어주면될듯
        DropProbability(); //가중치 랜덤
        if(level == "Hard")
        {
            for (int i = 0; i < 4; i++)
            {
                itemMenu = itemtype(_probability[i]);
                if(_probability[i] == _pR)
                {
                    if (_pR <= 100) // 일단 100프로로 설정해야되니까
                    {
                        Instantiate(itemMenu[0], mTransform.position, itemMenu[0].transform.rotation);
                    }
                    return;
                }
                    if (_probability[i] <= 70)
                    {
                        Instantiate(itemMenu[0], mTransform.position, itemMenu[0].transform.rotation);
                    }
                    else if (_probability[i] <= 90)
                    {
                        Instantiate(itemMenu[1], mTransform.position, itemMenu[1].transform.rotation);
                    }
                    else
                    {
                        Instantiate(itemMenu[2], mTransform.position, itemMenu[2].transform.rotation);
                    }
            }
        }
    }
    public GameObject[] itemtype(int type)
    {
        if(type == _wR)
        {
            return _weapon;
        }
        else if(type == _aR)
        {
            return _armor;
        } 
        else if(type == _acR)
        {
            return _accesary;
        }
        else
        {
            return _product;
        }
        return null;
    }
    public void DropProbability()
    {
        for (int i = 0; i <= 3; i++)
        {
            _probability[i] = UnityEngine.Random.Range(0, 100);
        }
    }
    public override void Damaged(int amount)
    {
        if(_curState != State.Return)
        {
            if (DamageToPlayer())
            {
                _sStat.Hp -= amount;
                ChangeState(State.Damage);
            }
        }
    }
 
    public void SlimeDie()
    {
        Destroy(gameObject, 2f);
    }
}
