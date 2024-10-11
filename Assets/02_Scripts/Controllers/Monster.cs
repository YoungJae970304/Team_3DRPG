using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;



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
    public MonsterStatManager _mStat;
    public MonsterStat _dropStat;
    public float _timer = 0;
    public int _randomAttack;
    public Dictionary<MonsterState, BaseState> States = new Dictionary<MonsterState, BaseState>();

    [Header("Drop관련 변수")]
    public List<string> sample = new List<string>();
    public DataTableManager _dataTableManager;
    public Drop _monsterDrop;
    public DeongeonLevel _deongeonLevel;
    public DropData _dropData;
    public Dictionary<string, int> randomValue = new Dictionary<string, int>();
    public int _monsterProduct;
    [Header("Drop리스트 추가 관련 변수")]
    int startValue1;
    int endValue1;
    int startValue2;
    int endValue2;
    int startValue3;
    int endValue3;
    [Header("몬스터 공격 콜라이더 리스트")]
    public List<Collider> _atkColliders;
    //[HideInInspector]
    //public List<GameObject> _hitPlayer;
    public Animator _anim;
    public virtual void Awake()
    {
        _deongeonLevel = DeongeonLevel.Hard; // 추후 던젼에서 받아오도록 설정
        _anim = GetComponent<Animator>();
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
    public virtual void Start()
    {
        _mStat = new MonsterStatManager();
        _mStat._mStat = new MonsterStat();
        _mStat._buffStat = new MonsterStat();
        _player = Managers.Game._player;
        _dataTableManager = Managers.DataTable;
        _monsterDrop = FindObjectOfType<Drop>();
        _dropStat = GetComponent<MonsterStat>();
       
        _nav = GetComponent<NavMeshAgent>();

        _originPos = transform.position;
        _curState = MonsterState.Idle;
        Debug.Log($"초기 상태: {_curState}");


        _mStat._mStat.MaxHP = 100;
        _mStat._mStat.HP = _mStat._mStat.MaxHP;
        _mStat._mStat.ATK = 30;
        _mStat._mStat.DEF = 10;
        _mStat._mStat.MoveSpeed = 1f;
        _mStat._mStat.RecoveryHP = 0;
        _mStat._mStat.MP = 0;
        _mStat._mStat.MaxMP = 0;
        _mStat._mStat.RecoveryMP = 0;
        _mStat._mStat.ChaseRange = 20;
        _mStat._mStat.ReturnRange = 20;
        _mStat._mStat.AttackRange = 2;
        _mStat._mStat.AwayRange = 20;
        _mStat._mStat.AtkDelay = 3;
    }

    // Update is called once per frame
    public virtual void Update()
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
        //_monsterDrop.DropItemSelect(_deongeonLevel, sample);

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
                if (!CanAttackPlayer())//내가 공격을 끝냈는지에 대한 조건을 상위조건으로 걸기// 불변수 // 급
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
        if (_mFSM == null)
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
        if (_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }
        _mStat.HP -= (amount - _mStat.DEF);
        StartDamege(_player.transform.position, 0.1f, 30f);
        Logger.LogError(_mStat.HP.ToString());
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
        Destroy(mob, 4f);
    }
    public void MonsterAnimFalse() // 애니메이션 이벤트용
    {
        _anim.enabled = false;
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
        //여기에 타이머넣어서 변환까지 시간걸리게
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
    #region 플레이어 공격관련 함수
    public void AttackPlayer() // 공격 모션 중간에 호출
    {
//
        int damage = _mStat.ATK;
        Collider[] checkColliders = Physics.OverlapSphere(transform.position, _mStat.AttackRange);
        foreach (Collider collider in checkColliders)
        {
            if (collider.CompareTag("Player") )
            {
                if (collider.TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.Damaged(damage);
                    //_player.Damaged(_mStat.ATK);
                    Logger.LogError($"{_player._playerStatManager.HP}");
                }
            }
        }

    }
    public void NomalAttack() //이벤트 2번
    {
        Logger.Log("NomalAttack");

        _player._playerHitState = PlayerHitState.NomalAttack;
        AttackPlayer();

    }
    public void SkillAttack() // 이벤트 2번
    {
        Logger.Log("SkillAttack");

        _player._playerHitState = PlayerHitState.SkillAttack;
        AttackPlayer();

    }
    public void AttackColliderActiveFalse()//공격 모션 마지막에 호출 이벤트 3번
    {
      
        for (int i = 0; i < _atkColliders.Count; i++)
        {
            _atkColliders[i].gameObject.SetActive(false);
        }
        //_hitPlayer.Clear();
    }
    public void LookPlayer()
    {
        // 플레이어와의 방향 계산
        Vector3 direction = _player.transform.position - transform.position;
        direction.y = 0; // Y축 회전 방지 (수평 평면에서만 회전)

        // 새로운 회전값 설정
        if (direction != Vector3.zero) // 방향 벡터가 0이 아닐 때만 회전
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
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
    public virtual void itemtest(DeongeonLevel curGrade, int monsterid)
    {
        DropData dropData = null;
        //아이템 데이터 테이블에서 ID에 맞는 아이템 찾기
        foreach (var newItem in _dataTableManager._MonsterDropData)
        {

            if (newItem.ID == monsterid)
            {
                dropData = newItem;
                break;
            }
        }

        if (dropData != null)
        {
            _dropStat.EXP = dropData.Value5;
            _monsterProduct = dropData.Value6;
            _dropStat.Gold = UnityEngine.Random.Range(dropData.StartValue4, dropData.EndValue4);
        }
        else
        {
            Logger.Log("해당 Id의 아이템을 찾을수가 없습니다. : " + monsterid);

        }

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
        if (!sample.Contains(i.ToString()) && i != 0)
        {
            sample.Add(i.ToString());
        }
    }
    public virtual void MakeItem()
    {
        int randomDice = UnityEngine.Random.Range(1, 100);
        if(randomDice <= 100)
        {
            GameObject item = Managers.Resource.Instantiate("ItemTest/TestItem");
            item.GetComponent<ItemPickup>()._itemId = _monsterDrop.DropItemSelect(_deongeonLevel, sample);
        }
        
        
    }
    #endregion
}
