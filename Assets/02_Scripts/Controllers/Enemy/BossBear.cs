using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBear : Monster
{
    public int _bossBearID = 99999; // 보스 아이디
    int skillCount = 0; // 로어횟수를 체크하기위한 변수
    public GameObject _roarRange; // 장판 오브젝트
    private Vector2 _startScale; // 초기 크기
    float _stageRoarPlus = 10f; // 로어 크기를 커지게하기 위한 변수
    public float _roarTimer; // 로어가 시간별로 커지게 하기 위한 변수
    public GameObject _maxRoarRange; // 로어의 최대 크기
    public List<float> _roarList = new List<float> { 0.7f, 0.4f, 0.1f };
    public override void OnEnable()
    {
        Init();
        _monsterID = _bossBearID;

    }
    public override void Init()
    {
        base.Init();
        ItemDrop(_deongeonLevel, _bossBearID); // 보스 아이디로 던전 단계로 드랍템 목록 불러오기
        StatCheck(_deongeonLevel, _bossBearID); // 보스 아이디와 던전 단계로 스텟 불러오기
        _monsterProduct = 61004; // 보스의 기타템 부산물
        _startScale = _roarRange.transform.localScale; // 로어의 시작 크기(로어가 끝난 후 다시 로어장판 크기를 작게 하기위하여 저장)
        skillCount = 0; // 로어를 사용한 횟수(보스 처치 OR 던전 실패 시 로어 횟수를 다시 0으로 초기화)
        _maxRoarRange.SetActive(false);// 로어 장판 범위 끄기
        _roarRange.SetActive(false); // 커지는 로어 장판 끄기
        _mStat._mStat.AttackRange = 8; // 보스의 공격 범위
        _roarList = new List<float> { 0.7f, 0.4f, 0.1f }; // 로어의 발동 시점을 정한 리스트(HP 70%, 40%, 10%일때 로어 발동)
    }
    public void OnDisable()
    {
        
    }
    public override void Update()
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
    IEnumerator PlusRoarRange() // 시간별로 로어를 커지게 하기위한 함수, 최종적으로 로어가 MaxRoarRange에 도달했다면 로어 실행
    {
        _roarTimer = 0;
        _roarRange.transform.localScale = _startScale;
        _maxRoarRange.SetActive(true);
        while (_roarRange.transform.localScale.x < _mStat.AtkDelay)
        {
            _roarRange.SetActive(true);
            //Logger.LogError(_roarRange.activeSelf.ToString());
            _roarRange.transform.localScale = _startScale * (0.1f + _roarTimer * _stageRoarPlus);
            //Logger.LogError(_roarRange.transform.localScale.x.ToString());
            _roarTimer += Time.deltaTime;
            if (_roarRange.transform.localScale.x >= _mStat.AtkDelay)
            {
                _roarTimer = 0;
                _anim.SetBool("AfterStay", true);
                _roarRange.transform.localScale = _startScale;
                _roarRange.SetActive(false);//애니메이션이 끝나는 시점에 꺼지도록 따로 함수작성
                //Roar();
                _maxRoarRange.SetActive(false);//애니메이션이 끝나는 시점에 꺼지도록 따로 함수작성
                break;
            }
            _anim.SetBool("AfterStay", false);
            yield return null;
        }


    }

    protected override void BaseState() // 곰의 상태 변화를 위한 함수, 조건에 따라 상태 변환
    {
        switch (_curState)
        {
            case MonsterState.Idle:
                if (CanSeePlayer())
                {
                    _anim.SetTrigger("PlayerChase");
                    MChangeState(MonsterState.Move);
                }
                break;
            case MonsterState.Damage:

                break;
            case MonsterState.Move:
                if (CanAttackPlayer())
                {
                    _anim.SetTrigger("BeforeAttack");
                    MChangeState(MonsterState.Attack);
                }
                else if (ReturnOrigin())
                {
                    _anim.SetTrigger("NonPlayerChase");
                    MChangeState(MonsterState.Return);
                }
                break;
            case MonsterState.Attack:
                if (_attackCompleted == true)
                {
                    if (!CanAttackPlayer())
                    {
                        if (!ReturnOrigin())
                        {
                            _anim.SetTrigger("PlayerChase");
                            MChangeState(MonsterState.Move);
                        }
                        else
                        {
                            _anim.SetTrigger("NonPlayerChase");
                            MChangeState(MonsterState.Return);
                        }
                    }
                }

                break;
            case MonsterState.Return:
                if ((_originPos - transform.position).magnitude <= 3f)
                {
                    _anim.SetTrigger("NonPlayerChase");
                    MChangeState(MonsterState.Idle);
                    skillCount = 0;
                    _roarList = new List<float> { 0.7f, 0.4f, 0.1f };
                }
                break;
            case MonsterState.Die:
                break;
            case MonsterState.Skill:
                break;
        }
    }
    public override void AttackStateSwitch() // 공격이 랜덤하게 나오도록 하는 함수
    {

        if (_randomAttack <= 30)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("Bite");
            // BiteAttack();

        }
        else if (_randomAttack <= 60)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("LeftHandAttack");
            //LeftHandAttack();

        }
        else if (_randomAttack <= 90)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("RightHandAttack");
            // RightHandAttack();

        }
        else
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("Earthquake");
            // EarthquakeAttack();

        }
    }
  
    
    public override void AttackPlayer() // 공격 모션 중간에 호출되는 플레이어 공격용 함수
    {
        int damage = _mStat.ATK;
        //Collider[] checkColliders = Physics.OverlapSphere(transform.position, _mStat.AttackRange);
        // 몬스터의 위치와 방향을 기반으로 박스의 중심을 계산
        Vector3 boxCenter = transform.position + transform.forward * (_mStat.AttackRange / 1.2f);

        // 박스의 크기 설정 (폭, 높이, 깊이)
        Vector3 boxSize = new Vector3(2.3f, 6f, _mStat.AttackRange * 1.6f); // 너비 1, 높이 1, 깊이 AttackRange

        // 박스에 충돌하는 객체를 체크
        Collider[] checkColliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);
        foreach (Collider collider in checkColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.TryGetComponent<IDamageAlbe>(out var damageable))
                {

                    //if (!collider.GetComponent<Player>()._hitting)
                    //{
                        //맞는 이펙트 실행(플레이어 위치에)
                        _enemyEffect.MonsterAttack(EnemyEffect.GoblemOrkEffects.MonsterHit, collider.transform);
                        _enemyAnimEvent.BossHitSound();
                    //}
                    //_player.Damaged(_mStat.ATK);
                    damageable.Damaged(damage);
                }
            }
        }


    }
    
    public override void Damaged(int amount) //추상클래스로 구현된 인터페이스를 상속받아 구현된 함수입니다. 데미지 처리를 위해 사용됩니다.
    {

        if (_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }

        //Logger.LogError(_mStat.HP.ToString());
        _mStat.HP -= (int)(amount * (100f / (_mStat.DEF + 100f)));
        Logger.LogWarning($"{gameObject.name} 남은 HP : {_mStat.HP}");
        PubAndSub.Publish<int>("BossHP", _mStat.HP);
        float hpPercentage = (float)_mStat.HP / _mStat.MaxHP;


        if (skillCount < _roarList.Count && hpPercentage <= _roarList[skillCount] && _mStat.HP > 0)
        {
            _anim.SetTrigger("BossRoar");
            MChangeState(MonsterState.Skill);
            //(이 밑에 if문 들어갈거임)
            //시간 초 후 roar발동
            //바닥에 깔리는 장판 구현해야함
            //시간 ++, 장판 활성화
            //시간이 늘어남에 따라 장판크기가 그에 맞춰서 커지고
            //다 커졌을 때 로어와 함께 장판 삭제
            if(_roarTimer <= 0)
            {
                StartCoroutine(PlusRoarRange());
            }
            
            
            skillCount++;


        }
        else if(_roarTimer == 0)
        {
            // HP 상태에 따른 상태 전환
            if (_mStat.HP <= 0)
            {
                MChangeState(MonsterState.Die);

            }
            else
            {
                MChangeState(MonsterState.Move);
            }
        }
        else if(_mStat.HP <= 0)
        {
           
                MChangeState(MonsterState.Die);
            
        }

    }
    public override void SetDestinationTimer(float targetTIme) // 이동을 위한 타이머입니다. 일정 시간마다 몬스터의 이동 경로를 탐색합니다.
    {
        _timer += Time.deltaTime;
        if (_timer >= targetTIme / 2)
        {
            LookPlayer();
        }
        if (_timer >= targetTIme)
        {
            _nav.destination = _player.transform.position;
            _nav.stoppingDistance = _mStat.AttackRange;
            _nav.SetDestination(_nav.destination);
            _timer = 0;
        }
    }

    public void EarthquakeAttack()
    {
        Logger.Log("EarthquakeAttack");
        AttackPlayer();

        _player._playerHitState = PlayerHitState.SkillAttack;
    }

    public void BiteAttack()
    {
        Logger.Log("BiteAttack");
        AttackPlayer();

        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
        AttackPlayer();

        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
        AttackPlayer();

        _player._playerHitState = PlayerHitState.SkillAttack;
    }

    public override void StartDamege(Vector3 playerPosition, float delay, float pushBack) //데미지 받을 시 처리되는 함수입니다. 애니메이션 이벤트의 보스 데미지를 불러와서 처리합니다.
    {
        LookPlayer();
        _enemyAnimEvent.BossDamage();
    }
    public override void MakeItem() // 아이템을 만드는 함수입니다. 일정 확률로 아이템이 드랍되도록 설정되었습니다.
    {
        int dropvalue = 70;
        base.MakeItem();
        int randomDice = UnityEngine.Random.Range(1, 101);
        if (randomDice <= dropvalue)
        {
            GameObject productItem = Managers.Resource.Instantiate("DropItem/DropItem");
            productItem.GetComponent<ItemPickup>()._itemId = _monsterProduct.ToString();
            productItem.transform.position = new Vector3(productItem.transform.position.x + 1, productItem.transform.position.y, productItem.transform.position.z + 1);

        }
    }
}
