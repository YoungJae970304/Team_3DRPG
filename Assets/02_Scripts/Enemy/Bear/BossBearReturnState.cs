using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBearReturnState : MonsterBaseState
{
    public BossBearReturnState(BossBear bossBear) : base(bossBear)
    {
        _bossBear = bossBear;
    }
    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
        _bossBear._nav.destination = _bossBear._originPos;
    }

    public override void OnStateExit()
    {
        //originPos��� Idle�� ���� ��ȯ
        //�ƴ϶�� �÷��̾� �߰�(Move�� ���� ��ȯ) // �������� �ʿ���µ� Idle�θ� ���ϸ��
        //�굵 ����Ȯ���ϱ�
        //�� ������ ��� �ϴ� ����α�
        //�Ƹ� �ִϸ��̼� ���� ����
    }

    public override void OnStateUpdate()
    {
        //�̰� �� ����ؾ��ҵ�
        //������ ��� �����ϴ°� �´��ϴ� ���������� ü��ȸ�� + ������ұ��� ��� ���� // 
        //�̷��� ������ �Լ��� ȣ���� �� return�����̸� ������Ʈ ��ȯ�� ���ϰ� ���ǰɾ�ߵ� // ���� �ɷ�����
        _bossBear._nav.SetDestination(_bossBear._originPos);
        //_bossBear.ReturnHeal(); //������ �Ǿ������� ������ ��� �۵��� �ȵ�
    }
}
