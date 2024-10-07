using Data;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;
using System.Threading.Tasks;
using System.Linq;



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
    [Header("enum 변수")]
    public MonsterState _curState;
    
    [Header("FSM관련 변수")]
    public FSM _mFSM;
    public Vector3 _originPos;
    public Player _player;
    public NavMeshAgent _nav;
    public MonsterStat _mStat;
    public float _timer = 0;
    public int _randomAttack;
    public Dictionary<MonsterState, BaseState> States = new Dictionary<MonsterState, BaseState>();
    GameManager _gameManager;
    [Header("Drop관련 변수")]
    public List<string> sample = new List<string>();
    public DataTableManager _dataTableManager;
    public Drop _monsterDrop;
    public DeongeonLevel _deongeonLevel;
    public DropData _dropData;
    public Dictionary<string, int> randomValue = new Dictionary<string, int>();
    
    [Header("Drop리스트 추가 관련 변수")]
    int startValue1;
    int endValue1;
    int startValue2;
    int endValue2;
    int startValue3;
    int endValue3;

    public virtual void Awake()
    {
        _deongeonLevel = DeongeonLevel.Hard; // 추후 던젼에서 받아오도록 설정
        _gameManager = Managers.Game;
        _dataTableManager = Managers.DataTable;
        _monsterDrop = FindObjectOfType<Drop>();
        itemtest(_deongeonLevel);
        _mStat = GetComponent<MonsterStat>();
         _nav = GetComponent<NavMeshAgent>();
        _player = _gameManager._player;
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
       
        if (sample.Count == 0)
        {
            Debug.LogError("sample 리스트가 비어 있습니다. 아이템을 추가하세요.");
            return;
        }
        _monsterDrop.DropItemSelect(_deongeonLevel, sample);
    }
    #region 상태 변환
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
    #endregion
    #region 상태 변환 FSM 사용
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
    #endregion
    #region 받는 데미지 함수
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
    #endregion
    #region 죽었을 때
    public virtual void Die(GameObject mob)
    {
        Destroy(mob, 2f);
    }
    #endregion
    #region 상태 변환 조건
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
    #endregion
    #region 타이머
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    #endregion
    #region 플레이어 공격함수
    public void AttackPlayer() // 일단 여기에 넣어놨는데 애니메이션에서 호출하는 이벤트방식으로 쓸듯
    {
        _player.Damaged(_mStat.ATK);

    }
    #endregion
    #region 넉백 코루틴
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
    #endregion
    #region 공격 상태 변환
    public virtual void AttackStateSwitch()
    {

    }
    #endregion
    #region 몬스터 난이도별 드랍목록 정리 //추후 던전 난이도로 변경 예정

    #endregion

    #region 아이템 드랍
    public virtual void itemtest(DeongeonLevel curGrade)
    {
        
        foreach (var item in _dataTableManager._MonsterDropData)
        {
            startValue1 = item.StartValue1;
            endValue1 = item.EndValue1;
            startValue2 = item.StartValue2;
            endValue2 = item.EndValue2;
            startValue3 = item.StartValue3;
            endValue3 = item.EndValue3;
            if (startValue1 != 0 && endValue1 != 0)
            {
                switch (curGrade)
                {
                    case DeongeonLevel.Easy:
                        
                        for (int i = startValue1; i <= endValue1; i++)
                        {
                            AddSample(i);
                            
                        }
                        break;
                    case DeongeonLevel.Normal:
                        for (int i = startValue1; i <= endValue1; i++)
                        {
                            AddSample(i);
                        }
                        for (int i = startValue2; i <= endValue2; i++)
                        {
                            AddSample(i);
                        }
                        break;
                    case DeongeonLevel.Hard:
                        for (int i = startValue1; i <= endValue1; i++)
                        {
                            AddSample(i);
                        }
                        for (int i = startValue2; i <= endValue2; i++)
                        {
                            AddSample(i);
                            
                        }
                        for (int i = startValue3; i <= endValue3; i++)
                        {
                            AddSample(i);
                        }
                        break;
                }
            }
        }
        
        
    }
    public void AddSample(int i)
    {
        if (!sample.Contains(i.ToString()) && i !=0)
        {
            sample.Add(i.ToString());
        }
    }
    public void MakeItem()
    {
        GameObject item = Instantiate(Managers.Resource.Instantiate("ItemTest/TestItem"));
        item.GetComponent<ItemPickup>()._itemId = _monsterDrop.DropItemSelect(_deongeonLevel, sample);
    }
    #endregion
}
