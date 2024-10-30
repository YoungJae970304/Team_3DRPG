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
        itemtest(_deongeonLevel, _goblemID);
        StatCheck(_deongeonLevel, _goblemID);
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
    public override void MakeItem()
    {
        base.MakeItem();
        int randomDice = UnityEngine.Random.Range(1, 101);
        if (randomDice <= 100)
        {
            GameObject productItem = Managers.Resource.Instantiate("ItemTest/TestItem");
            productItem.GetComponent<ItemPickup>()._itemId = _monsterProduct.ToString();
            Logger.LogError($"{productItem.GetComponent<ItemPickup>()._itemId}");
            productItem.transform.position = new Vector3(productItem.transform.position.x + 1, productItem.transform.position.y, productItem.transform.position.z + 1);
           
        }
    }
}
