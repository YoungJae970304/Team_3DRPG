using UnityEngine;

public class Slime : Monster
{

    public int _slimeID;
 
    public override void OnEnable()
    {
       
        Init();
    }
    public override void Init()
    {
        base.Init();
        SlimeIDCheck(_deongeonLevel);
        ItemDrop(_deongeonLevel, _slimeID);
        StatCheck(_deongeonLevel, _slimeID);
        _monsterID = _slimeID;
    }
    public override void AttackStateSwitch()
    {
        _anim.SetTrigger("Attack");
    }


    protected override void BaseState()
    {
        switch (_curState)
        {
            case MonsterState.Idle:
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
    public override void MakeItem()
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
    public override void AttackPlayer()
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
                        Logger.LogError(_player.transform.position.ToString());
                        _enemyAnimEvent.SlimeHitAtk();//이거는 내일수정
                    }
                    //_player.Damaged(_mStat.ATK);
                    Logger.LogError($"{_player._playerStatManager.HP}");
                    damageable.Damaged(damage);
                }
            }
        }

    }
    public void SlimeIDCheck(DeongeonType curLevel)
    {
        foreach (var sID in _dataTableManager._MonsterDropData)
        {
            string iDCheck = sID.ID.ToString();
            char lastDigit = iDCheck[iDCheck.Length - 1];
            char SID = iDCheck[iDCheck.Length - 4];
            if (lastDigit == '1')
            {
                if (lastDigit == '1')
                {
                    _monsterProduct = sID.Value6;
                }
                switch (curLevel)
                {
                    case DeongeonType.Easy:
                        if (SID == '1')
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonType.Normal:
                        if (SID == '2')
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonType.Hard:
                        if (SID == '3')
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                }


            }

        }

    }

}
