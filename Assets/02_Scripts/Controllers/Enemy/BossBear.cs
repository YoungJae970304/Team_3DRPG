using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBear : Monster, IDamageAlbe
{
    protected override void BaseState()
    {
        switch (_curState)
        {
            case MonsterState.Idle:
                if (CanSeePlayer())
                    MChangeState(MonsterState.Move);
                break;
            case MonsterState.Damage:
                if (CanAttackPlayer())
                    _mFSM.ChangeState(States[MonsterState.Attack]);
                else if (_mStat.HP <= 0)
                    _mFSM.ChangeState(States[MonsterState.Die]);
                else
                    _mFSM.ChangeState(States[MonsterState.Move]);
                break;
            case MonsterState.Move:
                if (CanAttackPlayer())
                    _mFSM.ChangeState(States[MonsterState.Attack]);
                else if (ReturnOrigin())
                    _mFSM.ChangeState(States[MonsterState.Return]);
                break;
            case MonsterState.Attack:
                if (!CanAttackPlayer())
                {
                    if (!ReturnOrigin())
                    {
                        _mFSM.ChangeState(States[MonsterState.Move]);
                    }
                    else
                    {
                        _mFSM.ChangeState(States[MonsterState.Return]);
                    }
                }
                break;
            case MonsterState.Return:
                if ((_originPos - transform.position).magnitude <= 3f)
                    _mFSM.ChangeState(States[MonsterState.Idle]);
                break;
            case MonsterState.Skill:
                if (CanAttackPlayer())
                    _mFSM.ChangeState(States[MonsterState.Attack]);
                else
                {
                    _mFSM.ChangeState(States[MonsterState.Move]);
                }
                break;
            case MonsterState.Die:
                break;


        }
    }

    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 15)
        {
            LeftBiteAttack();
        }
        else if (_randomAttack <= 30)
        {
            RightBiteAttack();
        }
        else if (_randomAttack <= 60)
        {
            LeftHandAttack();
        }
        else if (_randomAttack <= 90)
        {
            RightHandAttack();
        }
        else
        {
            EarthquakeAttack();
        }
    }
    public void EarthquakeAttack()
    {
        Logger.Log("EarthquakeAttack");
        AttackPlayer();
    }
    public void LeftBiteAttack()
    {
        Logger.Log("LeftBiteAttack");
        AttackPlayer();
    }
    public void RightBiteAttack()
    {
        Logger.Log("RightBiteAttack");
        AttackPlayer();
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
        AttackPlayer();
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
        AttackPlayer();
    }

    public override IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)
    {
        return null;
    }
}
