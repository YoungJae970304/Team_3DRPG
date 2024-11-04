using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBear : Monster
{
    public int _bossBearID = 99999;
    int skillCount = 0;
    public GameObject _roarRange; // 장판 오브젝트
    private Vector2 _startScale; // 초기 크기
    float _stageRoarPlus = 10f;
    public float _roarTimer;
    public GameObject _maxRoarRange;
    public override void Start()
    {
        base.Start();

    }
    public override void OnEnable()
    {
        Init();
        _monsterID = _bossBearID;

    }
    public override void Init()
    {
        base.Init();
        itemtest(_deongeonLevel, _bossBearID);
        StatCheck(_deongeonLevel, _bossBearID);
        _monsterProduct = 61004;
        _startScale = _roarRange.transform.localScale;
        skillCount = 0;
        _maxRoarRange.SetActive(false);
        _roarRange.SetActive(false);
        _mStat._mStat.AttackRange = 4;
        _roarList = new List<float> { 0.7f, 0.4f, 0.1f };
        
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
    IEnumerator PlusRoarRange()
    {
        Logger.LogError("여긴 들어옴?");
        _roarTimer = 0;
        _roarRange.transform.localScale = _startScale;
        _maxRoarRange.SetActive(true);
        Logger.LogError("여긴 들어옴?2");
        while (_roarRange.transform.localScale.x < _mStat.AtkDelay)
        {
            Logger.LogError("여긴 들어옴?3");
            _roarRange.SetActive(true);
            //Logger.LogError(_roarRange.activeSelf.ToString());
            _roarRange.transform.localScale = _startScale * (0.1f + _roarTimer * _stageRoarPlus);
            //Logger.LogError(_roarRange.transform.localScale.x.ToString());
            _roarTimer += Time.deltaTime;
            if (_roarRange.transform.localScale.x >= _mStat.AtkDelay)
            {
                Logger.LogError("여긴 들어옴?4");
                _roarTimer = 0;
                _anim.SetBool("AfterStay", true);
                _roarRange.transform.localScale = _startScale;
                _roarRange.SetActive(false);//애니메이션이 끝나는 시점에 꺼지도록 따로 함수작성
                BearRoar();
                _maxRoarRange.SetActive(false);//애니메이션이 끝나는 시점에 꺼지도록 따로 함수작성
                break;
            }
            _anim.SetBool("AfterStay", false);
            Logger.LogError("여긴 들어옴?5");
            yield return null;
        }


    }

    protected override void BaseState()
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
    public override void AttackStateSwitch()
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
    //bool RoarOn = false;
    public void BearRoar()
    {

        _player._playerHitState = PlayerHitState.StunAttack;
        //로어 애니메이션
        Roar();




    }
    public void Roar()
    {
        //int damage = 0;
        Collider[] checkColliders = Physics.OverlapSphere(transform.position, _maxRoarRange.transform.localScale.x * 2);
        Logger.LogError($"{_maxRoarRange.transform.localScale.x}");
        foreach (Collider collider in checkColliders)
        {
            if (collider.CompareTag("Player"))
            {
                if (collider.TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.StatusEffect.SpawnEffect<StunEffect>(1);
                    //_player.Damaged(_mStat.ATK);
                    Logger.LogError($"{_player._playerStatManager.HP}");
                }
            }
        }
    }

    public List<float> _roarList = new List<float> { 0.7f, 0.4f, 0.1f };
    public override void Damaged(int amount)
    {

        if (_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }

        //Logger.LogError(_mStat.HP.ToString());
        _mStat.HP -= (amount - _mStat.DEF);
        //Logger.LogError(_mStat.HP.ToString());

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
            StartCoroutine(PlusRoarRange());
            skillCount++;


        }
        else
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

    public override void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        LookPlayer();

    }
    public override void MakeItem()
    {
        base.MakeItem();
        int randomDice = UnityEngine.Random.Range(1, 101);
        if (randomDice <= 100)
        {
            GameObject productItem = Managers.Resource.Instantiate("ItemTest/TestItem");
            productItem.GetComponent<ItemPickup>()._itemId = _monsterProduct.ToString();
            productItem.transform.position = new Vector3(productItem.transform.position.x + 1, productItem.transform.position.y, productItem.transform.position.z + 1);

        }
    }
}
