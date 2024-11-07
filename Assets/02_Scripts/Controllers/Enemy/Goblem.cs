using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Goblem : Monster
{
    public int _goblemID;
 
    public override void Start()
    {
        base.Start();
        
    }
    public override void OnEnable()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        GoblemIDCheck(_deongeonLevel);
        ItemDrop(_deongeonLevel, _goblemID);
        StatCheck(_deongeonLevel, _goblemID);
        _monsterID = _goblemID;
    }
    public override void AttackStateSwitch()
    {
        
        if (_randomAttack <= 50)
        {
          
            _anim.SetTrigger("attack");
            

        }
        else
        {
    
            _anim.SetTrigger("attack1");
            
        }
    }

    public void GoblemIDCheck(DeongeonType curLevel)
    {
        foreach (var gID in _dataTableManager._MonsterDropData)
        {
            string iDCheck = gID.ID.ToString();
            //Logger.LogError(iDCheck);
            char lastDigit = iDCheck[iDCheck.Length - 1];
            char GID = iDCheck[iDCheck.Length - 4];
            //Logger.LogError(lastDigit.ToString());
            if (lastDigit == '2')
            {
                if (lastDigit == '2')
                {
                    _monsterProduct = gID.Value6;
                }
                //Logger.LogError(gID.Value6.ToString("D1"));
                switch (curLevel)
                {
                    case DeongeonType.Easy:
                        if (GID == '1')
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                    case DeongeonType.Normal:
                        if (GID == '2')
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                    case DeongeonType.Hard:
                        if (GID == '3')
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                }
                
            }
            
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
                        _enemyAnimEvent.GoblinHitAtk();
                    }
                    //_player.Damaged(_mStat.ATK);
                    damageable.Damaged(damage);
                }
            }
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
}
