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
        OrkIDCheck(_deongeonLevel);
        itemtest(_deongeonLevel, _OrkID);
    }
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 100)
        {
            _atkColliders[0].gameObject.SetActive(true);
            _anim.SetTrigger("Attack");
        }
        else
        {
            _atkColliders[1].gameObject.SetActive(true);
            _anim.SetTrigger("Attack1");
        }
    }
   

    public override async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        LookPlayer();
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
