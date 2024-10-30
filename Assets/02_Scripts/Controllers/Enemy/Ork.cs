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
    public override void Init()
    {
        base.Init();
        OrkIDCheck(_deongeonLevel);
        itemtest(_deongeonLevel, _OrkID);
        StatCheck(_deongeonLevel, _OrkID);
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
