using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using System.Threading.Tasks;



public class Monster : MonoBehaviour, IDamageAlbe
{


    public enum MonsterState
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
 
    public Dictionary<MonsterState, BaseState> States = new Dictionary<MonsterState, BaseState>();
    public float _timer = 0;
    public int _randomAttack;
    //

    private void Awake()
    {
        _mStat = GetComponent<MonsterStat>();
         _nav = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
       
        _originPos = transform.position;
        #region 상태딕셔너리 초기화
        States.Add(MonsterState.Idle, new MonsterIdleState(_player, this, _mStat));
        States.Add(MonsterState.Move, new MonsterMoveState(_player, this, _mStat));
        States.Add(MonsterState.Attack, new MonsterAttackState(_player, this, _mStat));
        States.Add(MonsterState.Damage, new MonsterDamagedState(_player, this, _mStat));
        States.Add(MonsterState.Return, new MonsterReturnState(_player, this, _mStat));
        States.Add(MonsterState.Die, new MonsterDieState(_player, this, _mStat));
        States.Add(MonsterState.Skill, new MonsterSkillState(_player, this, _mStat));
        #endregion
        _mFSM = new FSM(States[MonsterState.Idle]); // 옮겨본거
    }
    // Start is called before the first frame update
    void Start()
    {
        _curState = MonsterState.Idle;
        Debug.Log($"초기 상태: {_curState}");
       

        _mStat.MaxHP = 100;
        _mStat.HP = _mStat.MaxHP;
        _mStat.ATK = 30;
        _mStat.DEF = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        _mFSM.UpdateState();

        
        if (_curState == MonsterState.Damage)
        {
            
            return;
        }
        else
        {
            BaseState();
        }
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
        if(_mFSM == null)
        {
            Logger.LogError("FSM이 null");
            return;
        }
        if (States.ContainsKey(nextState))
        {
            _curState = nextState;
            _mFSM.ChangeState(States[_curState]);
        }
        else
        {
            Logger.LogError($"상태가 유효하지 않음: {nextState}");
        }
       
    }
    public virtual void Damaged(int amount)
    {
        if(_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }
        _mStat.HP -= ( amount - _mStat.DEF );
        StartDamege(_player.transform.position, 0.1f, 20f);
        if (_mStat.HP > 0)
        {
            MChangeState(MonsterState.Damage);
        }
        else
        {
            MChangeState(MonsterState.Die);
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
    /*public virtual IEnumerator StartDamege(Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
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
    }*/
    public virtual async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        _nav.enabled = false;
        // 넉백 방향 계산
        Vector3 diff = (transform.position - playerPosition).normalized; // 플레이어 반대 방향
        Vector3 force = diff * pushBack; // 넉백 힘

        // Rigidbody 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // 물리 효과 활성화
       
        
        // 넉백 힘 적용
        rb.AddForce(force, ForceMode.Impulse);

        // 넉백 후 처리
        await Task.Delay((int)(delay * 1000)); // 넉백 지속 시간 (필요에 따라 조정)

        // 넉백이 끝나면 NavMeshAgent를 다시 활성화


        _nav.enabled = true;
        rb.isKinematic = true; // 다시 비활성화 (필요시)
        if (CanAttackPlayer())
            MChangeState(MonsterState.Attack);
        else
            MChangeState(MonsterState.Move);
    }
    public virtual void AttackStateSwitch()
    {

    }
}
