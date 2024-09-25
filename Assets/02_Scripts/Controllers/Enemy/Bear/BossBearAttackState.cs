using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearAttackState : MonsterBaseState
{
    public BossBearAttackState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
        _bStat = _bossBear._bStat;
        _player = _ork._player.GetComponent<Player>();
        _pStat = _player._playerStat;
    }
    float _timer;
    BearStat _bStat;
    PlayerStat _pStat;
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
        //딜레이 후 플레이어 공격
        if (_timer > _bossBear._attackDelay)
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
    public void AttackPlayer() // 일단 여기에 넣어놨는데 애니메이션에서 호출하는 이벤트방식으로 쓸듯
    {
        _pStat.PlayerHP -= _ork._oStat.Attack;
    }
    public void LeftHandAttack()
    {
        //이걸 클래스 상태로 한번 더 정리할까?
        //아니면 함수로 사용을 할까
    }
    public void RightHandAttack()
    {

    }
    public void LeftBiteAttack()
    {

    }
    public void RightBiteAttack()
    {

    }
    public void EarthquakeAttack()
    {

    }
}
