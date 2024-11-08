using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.AI;

public class Ork : Monster
{
    public int _OrkID;
    public override void Start()
    {
        base.Start();
        
    }
    public override void OnEnable()
    {
        base.OnEnable();
        Init();
    }
    public override void Init()
    {
        base.Init();
        OrkIDCheck(_deongeonLevel);
        ItemDrop(_deongeonLevel, _OrkID);
        StatCheck(_deongeonLevel, _OrkID);
        _monsterID = _OrkID;
    }
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 50)
        {
            //_atkColliders[0].gameObject.SetActive(true);
            _anim.SetTrigger("Attack");
        }
        else
        {
            //_atkColliders[1].gameObject.SetActive(true);
            _anim.SetTrigger("Attack1");
        }
    }


    public void OrkIDCheck(DeongeonType curLevel)
    {
        foreach (var oID in _dataTableManager._MonsterDropData)
        {
            string iDCheck = oID.ID.ToString();
            //Logger.LogError(iDCheck);
            char lastDigit = iDCheck[iDCheck.Length - 1];
            char OID = iDCheck[iDCheck.Length - 4];
            if (lastDigit == '3')
            {
                if (lastDigit == '3')
                {
                    _monsterProduct = oID.Value6;
                }
                switch (curLevel)
                {
                    case DeongeonType.Easy:
                        if (OID == '1')
                        {
                            _OrkID = oID.ID;
                        }
                        break;
                    case DeongeonType.Normal:
                        if (OID == '2')
                        {
                            _OrkID = oID.ID;
                        }
                        break;
                    case DeongeonType.Hard:
                        if (OID== '3')
                        {
                            _OrkID = oID.ID;
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
                        _enemyAnimEvent.OrkHitAttack();
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
