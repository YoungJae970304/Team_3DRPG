using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturnState : MonsterBaseState
{
    public SlimeReturnState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
        _slime._nav.destination = _slime._originPos;
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
        _slime._nav.SetDestination(_slime._originPos);
        //_slime.ReturnHeal(); //������ �Ǿ������� ������ ��� �۵��� �ȵ�
    }
}
