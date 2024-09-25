using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkReturnState : MonsterBaseState
{
    public OrkReturnState(Ork ork) : base(ork) 
    {
        _ork = ork;
    }


    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
        _ork._nav.destination = _ork._originPos;
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
        _ork._nav.SetDestination(_ork._originPos);
        //_ork.ReturnHeal(); //������ �Ǿ������� ������ ��� �۵��� �ȵ�
    }
}
