using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public abstract class Monster : MonoBehaviour, IDamageAlbe
{


    protected enum MonsterState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damage,
        Return,
        Die,
    }
    protected MonsterState _curState;
    public FSM _mFSM;
    public Vector3 _originPos;
    public Player _player;
    public NavMeshAgent _nav;
    public MonsterStat _mStat;
    protected Dictionary<MonsterState, BaseState> States = new Dictionary<MonsterState, BaseState>();
    public float _timer = 0;
    public int _randomAttack;


    private void Awake()
    {
        _mStat = GetComponent<MonsterStat>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _curState = MonsterState.Idle;
        _mFSM = new FSM(new IdleState(_player, this, _mStat));
        _originPos = transform.position;
        _nav = GetComponent<NavMeshAgent>();
        #region 상태딕셔너리 초기화
        States.Add(MonsterState.Idle, new IdleState(_player,this,_mStat));
        States.Add(MonsterState.Move, new MoveState(_player, this, _mStat));
        States.Add(MonsterState.Attack, new AttackState(_player, this, _mStat));
        States.Add(MonsterState.Damage, new DamagedState(_player, this, _mStat));
        States.Add(MonsterState.Return, new ReturnState(_player, this, _mStat));
        States.Add(MonsterState.Die, new DieState(_player, this, _mStat));
        States.Add(MonsterState.Skill, new SkillState(_player, this, _mStat));
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        BaseState();
        _mFSM.UpdateState();
    }

    protected virtual void BaseState()
    {
        switch (_curState)
        {
            case MonsterState.Idle:
                if (CanSeePlayer())
                    MChangeState(MonsterState.Move);
                break;
            case MonsterState.Damage:
                if (CanAttackPlayer())
                    MChangeState(MonsterState.Attack);
                else if (_mStat.HP <= 0)
                    MChangeState(MonsterState.Die);
                else
                    MChangeState(MonsterState.Move);
                break;
            case MonsterState.Move:
                if (CanAttackPlayer())
                    MChangeState(MonsterState.Attack);
                else if (ReturnOrigin())
                    MChangeState(MonsterState.Return);
                break;
            case MonsterState.Attack:
                if (!CanAttackPlayer())
                {
                    if (!ReturnOrigin())
                    {
                        MChangeState(MonsterState.Move);
                    }
                    else
                    {
                        MChangeState(MonsterState.Return);
                    }
                }
                break;
            case MonsterState.Return:
                if ((_originPos - transform.position).magnitude <= 3f)
                    MChangeState(MonsterState.Idle);
                break;
            case MonsterState.Die:
                break;


        }
    }
    protected void MChangeState(MonsterState nextState)
    {
        _curState = nextState;
        _mFSM.ChangeState(States[_curState]);
    }
    public virtual void Damaged(int amount)
    {
        if (_curState != MonsterState.Return)
        {
            if (DamageToPlayer())
            {
                _mStat.HP -= amount;
                _mFSM.ChangeState(States[MonsterState.Damage]);
            }
        }
    }

    public virtual void Die(GameObject mob)
    {
        Destroy(mob, 2f);
    }
    public bool DamageToPlayer()
    {
        return _mStat.ReturnRange > _player.transform.position.magnitude;
    }
    public bool CanAttackPlayer()
    {
        //사정거리 체크 구현
        return _mStat.AttackRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool CanSeePlayer()
    {
        return _mStat.ChaseRange > (_player.transform.position - transform.position).magnitude;
    }
    public bool ReturnOrigin()
    {
        return _mStat.ReturnRange < (_originPos - transform.position).magnitude;
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer() // 일단 여기에 넣어놨는데 애니메이션에서 호출하는 이벤트방식으로 쓸듯
    {
        _player.Damaged(_mStat.ATK);

    }
    public virtual IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
    {
        yield return new WaitForSeconds(delay);

        try//이걸 실행해보고 문제가 없다면 실행
        {

            Vector3 diff = playerPosition - transform.position;
            diff = diff / diff.sqrMagnitude;
            _nav.isStopped = true;
            GetComponent<Rigidbody>().
            AddForce((transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// 문제가 있다면 에러메세지 출력
        {
            Debug.Log(e.ToString());
        }
        //예외처리문
    }
    public virtual void AttackStateSwitch()
    {

    }
}
