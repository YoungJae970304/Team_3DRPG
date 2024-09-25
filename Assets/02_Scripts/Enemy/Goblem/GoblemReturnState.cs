using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemReturnState : MonsterBaseState
{
    public GoblemReturnState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }
    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
        _goblem._nav.destination = _goblem._originPos;
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
        _goblem._nav.SetDestination(_goblem._originPos);
        //_ork.ReturnHeal(); //������ �Ǿ������� ������ ��� �۵��� �ȵ�
    }
}
