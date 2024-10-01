using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BossBear : Monster, IDamageAlbe
{
   

    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 15)
        {
            _mAttackState = MAttackState.SkillAttack;
            LeftBiteAttack();
            
        }
        else if (_randomAttack <= 30)
        {
            _mAttackState = MAttackState.SkillAttack;
            RightBiteAttack();
            
        }
        else if (_randomAttack <= 60)
        {
            _mAttackState = MAttackState.SkillAttack;
            LeftHandAttack();
            
        }
        else if (_randomAttack <= 90)
        {
            _mAttackState = MAttackState.SkillAttack;
            RightHandAttack();
            
        }
        else
        {
            _mAttackState = MAttackState.SkillAttack;
            EarthquakeAttack();
            
        }
    }
    //bool RoarOn = false;
    public void BearRoar()
    {
 
            AttackPlayer();
            _mAttackState = MAttackState.StunAttack;
 
       
    }


    private List<float> _roarList = new List<float> {0.7f, 0.4f, 0.1f};
    public async override void Damaged(int amount)
    {
        //MChangeState(MonsterState.Damage);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        if (_mStat == null)
        {
            Logger.LogError("MonsterStat이 null입니다");
            return;
        }

   
        _mStat.HP -= (amount - _mStat.DEF);

       
        float hpPercentage = (float)_mStat.HP / _mStat.MaxHP;
        int skillCount = 0;
      
        if (skillCount < _roarList.Count &&hpPercentage <= _roarList[skillCount])
        {
            BearRoar();
            skillCount++;
        }
        
        await Task.Delay(2);

        // HP 상태에 따른 상태 전환
        if (_mStat.HP < 0)
        {
            MChangeState(MonsterState.Die);
        }
        else
        {
            MChangeState(MonsterState.Move);
        }
    }

    public void EarthquakeAttack()
    {
        Logger.Log("EarthquakeAttack");
        AttackPlayer();
        _mAttackState = MAttackState.SkillAttack;
    }
    public void LeftBiteAttack()
    {
        Logger.Log("LeftBiteAttack");
        AttackPlayer();
        _mAttackState = MAttackState.SkillAttack;
    }
    public void RightBiteAttack()
    {
        Logger.Log("RightBiteAttack");
        AttackPlayer();
        _mAttackState = MAttackState.SkillAttack;
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
        AttackPlayer();
        _mAttackState = MAttackState.SkillAttack;
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
        AttackPlayer();
        _mAttackState = MAttackState.SkillAttack;
    }

    public override void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        throw null;
    }
}
