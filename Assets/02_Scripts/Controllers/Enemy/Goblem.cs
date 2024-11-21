using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Goblem : Monster
{
    public int _goblemID; // 고블린의 아이디를 담기위한 변수입니다. 난이도 별로 다르게 값이 들어갑니다.
 
    public override void OnEnable()
    {
        base.OnEnable();
        Init();
    }
    public override void Init() // 던전 진입 후 고블린이 풀링되어 생성될 때 실행됩니다.
    {
        base.Init();
        GoblemIDCheck(_deongeonLevel); // 던전 레벨로 고블린의 아이디를 불러옵니다.
        ItemDrop(_deongeonLevel, _goblemID); // 던전 레벨과 고블린의 아이디로 드랍템 목록을 불러옵니다.
        StatCheck(_deongeonLevel, _goblemID); // 던전 레벨과 고블린의 아이디로 스텟을 불러옵니다.
        _monsterID = _goblemID; // 몬스터(베이스)의 아이디를 고블린의 아이디값으로 넣어줍니다.
    }
    public override void AttackStateSwitch() // 좌측공격과 우측 공격을 랜덤으로 발생시키기 위하여 작성하였습니다.
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

    public void GoblemIDCheck(DeongeonType curLevel) // 고블린의 아이디를 체크하기위한 함수입니다. 현재 던전레벨에 따라 다른 아이디가 대입됩니다.
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
    public override void AttackPlayer() //고블린의 공격에 사용되는 함수입니다.
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
    public override void MakeItem() // 아이템을 드랍하기위한 함수입니다.
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
