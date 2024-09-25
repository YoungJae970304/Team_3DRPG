using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SlimeAttackState : MonsterBaseState
{
    public SlimeAttackState(Slime slime) : base(slime) 
    {
        _slime = slime;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    float _timer = 0f;
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //플레이어 공격
        
        //_slime._player.Damaged(_slime._mStat.Attack);
        //애니메이션 실행
       
    }

    public override void OnStateExit()
    {
        //공격 관련 변수 초기화 이건 만들고 생각해야할듯
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //딜레이 후 플레이어 공격
        if(_timer > _slime._attackDelay)
        {
            _timer = 0f;
            //여기에 에너미 공격 넣기
            AttackPlayer();
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
    }
    public void AttackPlayer()
    {
        _pStat.PlayerHP -= _slime._sStat.Attack;
    }
}
