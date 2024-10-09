using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class BossBear : Monster, IDamageAlbe
{
    public int _bossBearID = 99999;
    public override void Start()
    {
        base.Start();
        itemtest(_deongeonLevel, _bossBearID);
        _monsterProduct = 61004;
    }
    protected override void BaseState()
    {
        switch (_curState)
        {
            case MonsterState.Idle:
                if (CanSeePlayer())
                {
                    _anim.SetTrigger("PlayerChase");
                    MChangeState(MonsterState.Move);
                }
                break;
            case MonsterState.Damage:

                break;
            case MonsterState.Move:
                if (CanAttackPlayer())
                    MChangeState(MonsterState.Attack);
                else if (ReturnOrigin())
                {
                    _anim.SetTrigger("NonPlayerChase");
                    MChangeState(MonsterState.Return);
                }
                break;
            case MonsterState.Attack:
                if (!CanAttackPlayer())
                {
                    if (!ReturnOrigin())
                    {
                        _anim.SetTrigger("PlayerChase");
                        MChangeState(MonsterState.Move);
                    }
                    else
                    {
                        _anim.SetTrigger("NonPlayerChase");
                        MChangeState(MonsterState.Return);
                    }
                }
                break;
            case MonsterState.Return:
                if ((_originPos - transform.position).magnitude <= 3f)
                {
                    _anim.SetTrigger("NonPlayerChase");
                    MChangeState(MonsterState.Idle);
                }
                break;
            case MonsterState.Die:
                break;
        }
        }
    public override void AttackStateSwitch()
    {
        
        if (_randomAttack <= 30)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("Bite");
           // BiteAttack();
            
        }
        else if (_randomAttack <= 60)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("LeftHandAttack");
            //LeftHandAttack();

        }
        else if (_randomAttack <= 90)
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("RightHandAttack");
           // RightHandAttack();
            
        }
        else
        {
            _player._playerHitState = PlayerHitState.SkillAttack;
            _anim.SetTrigger("Earthquake");
           // EarthquakeAttack();
            
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
            _anim.SetTrigger("BossRoar");
            //(이 밑에 if문 들어갈거임)
            //시간 초 후 roar발동
            //바닥에 깔리는 장판 구현해야함
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
    
    public void BiteAttack()
    {
        Logger.Log("BiteAttack");
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
