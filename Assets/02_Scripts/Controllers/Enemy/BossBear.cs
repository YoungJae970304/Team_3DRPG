using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BossBear : Monster, IDamageAlbe
{
    public int _bossBearID = 99999;
    public override void Awake()
    {
        base.Awake();
        itemtest(_deongeonLevel, _bossBearID);
    }
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 15)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            
            LeftBiteAttack();
            
        }
        else if (_randomAttack <= 30)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            
            RightBiteAttack();
            
        }
        else if (_randomAttack <= 60)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
           
            LeftHandAttack();
            
        }
        else if (_randomAttack <= 90)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
           
            RightHandAttack();
            
        }
        else
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
        ;
            EarthquakeAttack();
            
        }
    }
    //bool RoarOn = false;
    public void BearRoar()
    {
        _player._playerHitState = PlayerHitState.StunAttack;
        AttackPlayer();
            
        


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
      
        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void LeftBiteAttack()
    {
        Logger.Log("LeftBiteAttack");
        AttackPlayer();
        
        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void RightBiteAttack()
    {
        Logger.Log("RightBiteAttack");
        AttackPlayer();
       
        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
        AttackPlayer();
      
        _player._playerHitState = PlayerHitState.SkillAttack;
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
        AttackPlayer();
       
        _player._playerHitState = PlayerHitState.SkillAttack;
    }

    public override void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        throw null;
    }
  
}
