using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;



public class Monster : MonoBehaviour, IDamageAlbe, IStatusEffectAble
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
    public float _timer = 0;
    public int _randomAttack;
    public Dictionary<MonsterState, BaseState> States = new Dictionary<MonsterState, BaseState>();
    [Header("Drop관련 변수")]
    public List<string> sample = new List<string>();
    public DataTableManager _dataTableManager;
    public SphereCollider _collider;
    public Rigidbody _rig;
    public Drop _monsterDrop;
    public DeongeonType _deongeonLevel;
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
    [Header("몬스터 공격")]
    public bool _attackCompleted = false;
    public EnemyEffect _enemyEffect;
    [Header("몬스터 사망")]
    public Action _makeMonster;
    public Action _dieMonster;
    //[HideInInspector]
    //public List<GameObject> _hitPlayer;
    public Animator _anim;
    public int _monsterID;
    public EnemyAnimEvent _enemyAnimEvent;
    public StatusEffectManager StatusEffect { get => null; }
    public MonsterHpBar _monsterHpBar;
    public GameObject _hpBar;
    public ITotalStat Targetstat => _mStat;

    public Transform TargetTr => transform;

    public virtual void Awake()
    {
        _deongeonLevel = Managers.Game._selecDungeonLevel; // 추후 던젼에서 받아오도록 설정
        //_anim = GetComponent<Animator>();
        _anim = GetComponentInChildren<Animator>();
        _enemyAnimEvent = GetComponentInChildren<EnemyAnimEvent>();
        //_characterController.enabled = false;
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
        if (GetComponentInChildren<EnemyEffect>() != null)
        {
            _enemyEffect = GetComponentInChildren<EnemyEffect>();
        }
        _mStat = gameObject.GetOrAddComponent<MonsterStatManager>();
        
        _rig = GetComponent<Rigidbody>();
        _anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<SphereCollider>();
        if (GetComponentInChildren<EnemyEffect>() != null)
        {
            _enemyEffect = GetComponentInChildren<EnemyEffect>();
        }
        _monsterDrop = FindObjectOfType<Drop>();
        _nav = GetComponent<NavMeshAgent>();
        if (_hpBar != null)
        {
            _monsterHpBar = _hpBar.GetComponent<MonsterHpBar>();
            _hpBar.SetActive(false);

        }
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        //Init();
    }
    public virtual void OnEnable()
    {
        Init();
    }
    public virtual void Init()
    {
        if (_hpBar != null)
        {
            _hpBar.SetActive(false);
        }
        _mStat._mStat = new MonsterStat();
        _mStat._buffStat = new MonsterStat();

        _deongeonLevel = Managers.Game._selecDungeonLevel; // 추후 던젼에서 받아오도록 설정
        //_anim = GetComponent<Animator>();
         
        //_characterController.enabled = false;
        _mFSM = new FSM(States[MonsterState.Idle]); // 옮겨본거
        
        _deongeonLevel = Managers.Game._selecDungeonLevel;
        
        _player = Managers.Game._player;
        _dataTableManager = Managers.DataTable;
        

        _originPos = transform.position;
        _curState = MonsterState.Idle;
        Debug.Log($"초기 상태: {_curState}");


        /*_mStat._mStat.MaxHP = 100;
        _mStat._mStat.HP = _mStat._mStat.MaxHP;
        _mStat._mStat.ATK = 30;
        _mStat._mStat.DEF = 10;
        _mStat._mStat.MoveSpeed = 1f;*/
        /*_mStat._mStat.RecoveryHP = 0;
        _mStat._mStat.MP = 0;
        _mStat._mStat.MaxMP = 0;
        _mStat._mStat.RecoveryMP = 0;
        _mStat._mStat.ChaseRange = 20;
        _mStat._mStat.ReturnRange = 20;
        _mStat._mStat.AttackRange = 2;
        _mStat._mStat.AwayRange = 20;*/
        //_mStat._mStat.AtkDelay = 3;
    }
    // Update is called once per frame
    public virtual void Update()
    {

        _mFSM.UpdateState();

        // Logger.LogError($"슬라임위치확인{transform.position}");
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
                if (_attackCompleted == true)
                {

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
    public bool ChangeStateToString(string state)
    {
        MonsterState changeState;
        if (Enum.TryParse<MonsterState>(state, out changeState))
        {
            MChangeState(changeState);
            return true;
        }
        return false;
    }
    #endregion
    #region 상태 변환 FSM 사용
    public void MChangeState(MonsterState nextState)
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
        if (_curState == MonsterState.Die)
            return;
        if (!_hpBar.activeSelf)
        {
            _hpBar.SetActive(true);
        }
        if (_monsterID != 99999)
        {
            _enemyEffect.EffectOff();
        }
        if (_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }

        _mStat.HP -= (int)(amount * (100f / (_mStat.DEF + 100f)));
       
        _monsterHpBar.HpChanged();
        
        
        
        if (_mStat.HP > 0)
        {

            MChangeState(MonsterState.Damage);
            StartDamege(_player.transform.position, 1f, 30f);
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
        _player.PlayerEXPGain(_mStat._mStat.EXP);
        _player.PlayerGOLDGain(_mStat._mStat.Gold);
        Managers.Resource.Destroy(mob);//mob은 풀링오브젝트에 들어가는거
        _monsterDrop.DropItemSelect(_deongeonLevel, sample);//임시 설정 추후 던전에서 받아오도록 변경
        MakeItem();
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
        bool rangeCheck = _mStat.AttackRange > (_player.transform.position - transform.position).magnitude;
        bool angleCheck = Vector3.Angle(transform.forward, _player.transform.position - transform.position) < 45;
        return rangeCheck && angleCheck;
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
    public virtual void SetDestinationTimer(float targetTIme)
    {
        _timer += Time.deltaTime;
        if (_timer >= targetTIme / 2)
        {
            LookPlayer();
        }
        if (_timer >= targetTIme)
        {

            _nav.destination = _player.transform.position;
            _nav.stoppingDistance = _mStat.AttackRange / 2;
            _nav.SetDestination(_nav.destination);
            _timer = 0;

        }
    }
    public void SetDestinationOnTimeToIdle(float targetTime)
    {
        _timer += Time.deltaTime;
       
        if (_timer >= targetTime)
        {
            //일정 거리 배회
            //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
            float awayRangeX = UnityEngine.Random.Range(-_mStat.AwayRange, _mStat.AwayRange);
            //float awayRangeY = Random.Range(0, _sStat.AwayRange);
            float awayRangeZ = UnityEngine.Random.Range(-_mStat.AwayRange, _mStat.AwayRange);
            if (IsOnNavMesh(_originPos + new Vector3(awayRangeX, 0, awayRangeZ)))
            {
                if ((_nav.destination - transform.position).magnitude > 1f)
                {
                    _nav.SetDestination(_nav.destination);
                    _timer = 0;
                }

                else
                {
                    _nav.destination = _originPos + new Vector3(awayRangeX, 0, awayRangeZ);
                    _timer = 0;
                }
            }
            else
            {
                _nav.destination = _originPos;
            }
           
            

        }
    }
    bool IsOnNavMesh(Vector3 position)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(position, out hit, _mStat.AwayRange, NavMesh.AllAreas);
    }
    #endregion
    #region 플레이어 공격관련 함수
    public virtual void AttackPlayer() // 공격 모션 중간에 호출 // 수정 예정
    {
        
        int damage = _mStat.ATK;
        //Collider[] checkColliders = Physics.OverlapSphere(transform.position, _mStat.AttackRange);
        // 몬스터의 위치와 방향을 기반으로 박스의 중심을 계산
        Vector3 boxCenter = transform.position + transform.forward * (_mStat.AttackRange / 1.8f);

        // 박스의 크기 설정 (폭, 높이, 깊이)
        Vector3 boxSize = new Vector3(1.2f, 2f, _mStat.AttackRange); // 너비 1, 높이 1, 깊이 AttackRange

        // 박스에 충돌하는 객체를 체크
        Collider[] checkColliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);
        foreach (Collider collider in checkColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.TryGetComponent<IDamageAlbe>(out var damageable))
                {

                    if (!collider.GetComponent<Player>()._hitting)
                    {
                        //맞는 이펙트 실행(플레이어 위치에)
                        _enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.MonsterHit, collider.transform);
                    }
                    //_player.Damaged(_mStat.ATK);
                    damageable.Damaged(damage);
                }
            }
        
        }


    }


    public void LookBeforeAttack()
    {
        transform.LookAt(_player.transform);
    }
    public void LookPlayer()
    {
        // 플레이어와의 방향 계산
        Vector3 direction = (_player.transform.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        direction.y = 0; // Y축 회전 방지 (수평 평면에서만 회전)

        // 새로운 회전값 설정
        if (direction != Vector3.zero) // 방향 벡터가 0이 아닐 때만 회전
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        }
       
    }


    #endregion
    #region 넉백
    public virtual async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        if (_mStat.HP <= 0)
        {
            return;
        }
        //_nav.enabled = false;
        _nav.ResetPath();
        //_characterController.enabled = true; // CharacterController 비활성화

        // 넉백 방향 계산
        Vector3 diff = transform.position - playerPosition; // 플레이어 반대 방향
        Vector3 force = diff * pushBack; // 넉백 힘

        // 넉백 처리
        Vector3 moveDirection = force; // CharacterController는 이동 방향을 사용
        //_characterController.Move(moveDirection * Time.deltaTime); // 이동

        // 넉백 후 처리
        await Task.Delay((int)(delay * 1000)); // 넉백 지속 시간 (ms 단위)

        // 넉백이 끝나면 CharacterController를 다시 활성화

        // _nav.enabled = true;
        // _characterController.enabled = false;

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
    public virtual void ItemDrop(DeongeonType curGrade, int monsterid)
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
            _mStat._mStat.EXP = dropData.Value5;
            _monsterProduct = dropData.Value6;
            _mStat._mStat.Gold = UnityEngine.Random.Range(dropData.StartValue4, dropData.EndValue4);
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
                    case DeongeonType.Easy:

                        for (int i = startValue1; i <= endValue1; i++)
                        {
                            AddSample(i);

                        }
                        break;
                    case DeongeonType.Normal:
                        for (int i = startValue1; i <= endValue1; i++)
                        {
                            AddSample(i);
                        }
                        for (int i = startValue2; i <= endValue2; i++)
                        {
                            AddSample(i);
                        }
                        break;
                    case DeongeonType.Hard:
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
    public void StatCheck(DeongeonType curLevel, int ID)
    {
        foreach (var stat in _dataTableManager._MonsterStatData)
        {
            switch (curLevel)
            {
                case DeongeonType.Easy:
                    if (ID == stat.ID)
                    {
                        _mStat._mStat.MaxHP = stat.MaxHp;
                        _mStat._mStat.HP = _mStat._mStat.MaxHP;
                        _mStat._mStat.ATK = stat.ATK;
                        _mStat._mStat.DEF = stat.DEF;
                        _mStat._mStat.MoveSpeed = stat.MoveSpeed;
                        _mStat._mStat.AtkDelay = stat.AtkSpeed;
                        _mStat._mStat.RecoveryHP = stat.RecoveryHP;
                        _mStat._mStat.MP = stat.MP;
                        _mStat._mStat.MaxMP = stat.MaxMP;
                        _mStat._mStat.RecoveryMP = stat.RecoveryMP;
                        _mStat._mStat.ChaseRange = stat.ChaseRange;
                        _mStat._mStat.ReturnRange = stat.ReturnRange;
                        _mStat._mStat.AttackRange = stat.AttackRange;
                        _mStat._mStat.AwayRange = stat.AwayRange;
                        _nav.speed = _mStat._mStat.MoveSpeed;
                    }
                    break;
                case DeongeonType.Normal:
                    if (ID == stat.ID)
                    {
                        _mStat._mStat.MaxHP = stat.MaxHp;
                        _mStat._mStat.HP = _mStat._mStat.MaxHP;
                        _mStat._mStat.ATK = stat.ATK;
                        _mStat._mStat.DEF = stat.DEF;
                        _mStat._mStat.MoveSpeed = stat.MoveSpeed;
                        _mStat._mStat.AtkDelay = stat.AtkSpeed;
                        _mStat._mStat.RecoveryHP = stat.RecoveryHP;
                        _mStat._mStat.MP = stat.MP;
                        _mStat._mStat.MaxMP = stat.MaxMP;
                        _mStat._mStat.RecoveryMP = stat.RecoveryMP;
                        _mStat._mStat.ChaseRange = stat.ChaseRange;
                        _mStat._mStat.ReturnRange = stat.ReturnRange;
                        _mStat._mStat.AttackRange = stat.AttackRange;
                        _mStat._mStat.AwayRange = stat.AwayRange;
                        _nav.speed = _mStat._mStat.MoveSpeed;
                    }
                    break;
                case DeongeonType.Hard:
                    if (ID == stat.ID)
                    {
                        _mStat._mStat.MaxHP = stat.MaxHp;
                        _mStat._mStat.HP = _mStat._mStat.MaxHP;
                        _mStat._mStat.ATK = stat.ATK;
                        _mStat._mStat.DEF = stat.DEF;
                        _mStat._mStat.MoveSpeed = stat.MoveSpeed;
                        _mStat._mStat.AtkDelay = stat.AtkSpeed;
                        _mStat._mStat.RecoveryHP = stat.RecoveryHP;
                        _mStat._mStat.MP = stat.MP;
                        _mStat._mStat.MaxMP = stat.MaxMP;
                        _mStat._mStat.RecoveryMP = stat.RecoveryMP;
                        _mStat._mStat.ChaseRange = stat.ChaseRange;
                        _mStat._mStat.ReturnRange = stat.ReturnRange;
                        _mStat._mStat.AttackRange = stat.AttackRange;
                        _mStat._mStat.AwayRange = stat.AwayRange;
                        _nav.speed = _mStat._mStat.MoveSpeed;
                    }
                    break;
                case DeongeonType.Boss:
                    if (ID == stat.ID)
                    {
                        _mStat._mStat.MaxHP = stat.MaxHp;
                        _mStat._mStat.HP = _mStat._mStat.MaxHP;
                        _mStat._mStat.ATK = stat.ATK;
                        _mStat._mStat.DEF = stat.DEF;
                        _mStat._mStat.MoveSpeed = stat.MoveSpeed;
                        _mStat._mStat.AtkDelay = stat.AtkSpeed;
                        _mStat._mStat.RecoveryHP = stat.RecoveryHP;
                        _mStat._mStat.MP = stat.MP;
                        _mStat._mStat.MaxMP = stat.MaxMP;
                        _mStat._mStat.RecoveryMP = stat.RecoveryMP;
                        _mStat._mStat.ChaseRange = stat.ChaseRange;
                        _mStat._mStat.ReturnRange = stat.ReturnRange;
                        _mStat._mStat.AttackRange = stat.AttackRange;
                        _mStat._mStat.AwayRange = stat.AwayRange;
                        _nav.speed = _mStat._mStat.MoveSpeed;
                    }
                    break;
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
        int dropvalue = 30;
        int randomDice = UnityEngine.Random.Range(1, 100);
        if (randomDice <= dropvalue)
        {
            GameObject item = Managers.Resource.Instantiate("DropItem/DropItem");
            item.GetComponent<ItemPickup>()._itemId = _monsterDrop.DropItemSelect(_deongeonLevel, sample);
        }
    }
    #endregion
}
