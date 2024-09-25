using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeAttackState : MonsterBaseState
{
    public SlimeAttackState(Slime slime) : base(slime) 
    {
        _slime = slime;
        _sStat = _slime._sStat;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    float _timer = 0f;
    PlayerStat _pStat;
    SlimeStat _sStat;
    public override void OnStateEnter()
    {
        //�÷��̾� ����
        
        //_slime._player.Damaged(_slime._mStat.Attack);
        //�ִϸ��̼� ����
       
    }

    public override void OnStateExit()
    {
        //���� ���� ���� �ʱ�ȭ �̰� ����� �����ؾ��ҵ�
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //������ �� �÷��̾� ����
        if(_timer > _slime._attackDelay)
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
        _player.Damaged(_sStat.Attack);
    }
}
