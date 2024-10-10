using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class Slime : Monster, IDamageAlbe
{
    
    public int _slimeID;
    public override void Start()
    {
        base.Start();
        SlimeIDCheck(_deongeonLevel);
        itemtest(_deongeonLevel, _slimeID);
    }
    public override void AttackStateSwitch()
    {
        _atkColliders[0].gameObject.SetActive(true);
        _anim.SetTrigger("Attack");
    }
    public override async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        transform.LookAt(_player.transform.position);
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
        base.MakeItem();
        int randomDice = UnityEngine.Random.Range(1, 101);
        if (randomDice <= 100)
        {
            GameObject productItem = Managers.Resource.Instantiate("ItemTest/TestItem");
            productItem.GetComponent<ItemPickup>()._itemId = _monsterProduct.ToString();
            productItem.transform.position = new Vector3(productItem.transform.position.x + 1, productItem.transform.position.y, productItem.transform.position.z + 1);
            
        }
    }
    public void SlimeIDCheck(DeongeonLevel curLevel)
    {
        foreach(var sID in _dataTableManager._MonsterDropData)
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
                    case DeongeonLevel.Easy:
                        if(SID == '1')
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonLevel.Normal:
                        if (SID == '2')
                        {
                            _slimeID = sID.ID;
                        }
                        break;
                    case DeongeonLevel.Hard:
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
