using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Jobs;

public class Slime : Monster
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
    public void SlimeIDCheck(DeongeonType curLevel)
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
                    case DeongeonType.Easy:
                        if(SID == '1')
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
