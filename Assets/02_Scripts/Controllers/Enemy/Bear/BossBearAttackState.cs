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
        int RandomAttack = Random.Range(0, 100);
        //������ �� �÷��̾� ����
        if (_timer > _bossBear._attackDelay)
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
    public void AttackPlayer() // �ϴ� ���⿡ �־���µ� �ִϸ��̼ǿ��� ȣ���ϴ� �̺�Ʈ������� ����
    {
        _pStat.PlayerHP -= _ork._oStat.Attack;
    }
    public void LeftHandAttack()
    {
        //�̰� Ŭ���� ���·� �ѹ� �� �����ұ�?
        //�ƴϸ� �Լ��� ����� �ұ�
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
