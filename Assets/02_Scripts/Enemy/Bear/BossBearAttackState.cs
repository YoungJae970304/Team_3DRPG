using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearAttackState : MonsterBaseState
{
    public BossBearAttackState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }
    float _timer;
    public override void OnStateEnter()
    {
        _timer = 0;
    }

    public override void OnStateExit()
    {
        _timer = _bossBear._attackDelay;
    }

    public override void OnStateUpdate()
    {
        AttackTimer();
        //������ �� �÷��̾� ����
        if (_timer > _bossBear._attackDelay)
        {
            _timer = 0f;
            //���⿡ ���ʹ� ���� �ֱ�
        }
    }
    public void AttackTimer()
    {
        _timer += Time.deltaTime;
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
