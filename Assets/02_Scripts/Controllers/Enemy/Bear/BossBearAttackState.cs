using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossBearAttackState : MonsterBaseState
{
    public enum BearAttackSTATE
    {
        Idle,
        Earthqauke,
        LBite,
        RBite,
        LHand,
        RHand,
    }
    
    public BossBearAttackState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
        _bStat = _bossBear._bStat;
        _player = _bossBear._player.GetComponent<Player>();
        _pStat = _player._playerStat;
        _curState = BearAttackSTATE.Idle;
        #region �� ���� ���� �ʱ�ȭ
        #endregion
    }
    float _timer;
    BearStat _bStat;
    PlayerStat _pStat;
    int _randomAttack;
    BearAttackSTATE _curState;
    MonsterFSM _monFSM;
    public override void OnStateEnter()
    {
        _timer = 0;
        _bossBear._nav.stoppingDistance = _bossBear._bStat.AttackRange;
    }

    public override void OnStateExit()
    {
        _timer = _bossBear._attackDelay;
        _bossBear._nav.stoppingDistance = 0;
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        
        _randomAttack = UnityEngine.Random.Range(1, 101);
        //������ �� �÷��̾� ����
        if (_timer > _bossBear._attackDelay)
        {
            _timer = 0f;
            //���⿡ ���ʹ� ���� �ֱ�
            AttackPlayer();
            AttackStateSwitch();
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer() // �ϴ� ���⿡ �־���µ� �ִϸ��̼ǿ��� ȣ���ϴ� �̺�Ʈ������� ����
    {
        _player.Damaged(_bStat.Attack);
        
    }
    public void AttackStateSwitch()
    {
        if(_randomAttack <= 15)
        {
            //ChangeState(BearAttackSTATE.LBite);
        }
        else if(_randomAttack <= 30)
        {
            //ChangeState(BearAttackSTATE.RBite);
        }
        else if(_randomAttack <= 60)
        {
           // ChangeState(BearAttackSTATE.LHand);
        }
        else if(_randomAttack <= 90)
        {
           // ChangeState(BearAttackSTATE.RHand);
        }
        else
        {
           // ChangeState(BearAttackSTATE.Earthqauke);
        }
    }
    public void EarthquakeAttack()
    {
        Logger.Log("EarthquakeAttack");
    }
    public void LeftBiteAttack()
    {
        Logger.Log("LeftBiteAttack");
    }
    public void RightBiteAttack()
    {
        Logger.Log("RightBiteAttack");
    }
    public void LeftHandAttack()
    {
        Logger.Log("LeftHandAttack");
    }
    public void RightHandAttack()
    {
        Logger.Log("RightHandAttack");
    }
}
