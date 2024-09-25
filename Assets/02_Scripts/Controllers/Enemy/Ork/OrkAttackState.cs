using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkAttackState : MonsterBaseState
{
    public OrkAttackState(Ork ork) : base(ork) 
    {
        _ork = ork;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    PlayerStat _pStat;
    float _timer = 0f;
    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //������ �� �÷��̾� ����
        if (_timer > _ork._attackDelay)
        {
            _timer = 0f;
            //���⿡ ���ʹ� ���� �ֱ�
            AttackPlayer();
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer()
    {
        _pStat.PlayerHP -= _ork._oStat.Attack;
    }
}
