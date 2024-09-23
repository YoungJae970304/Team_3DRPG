using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Monster monster) : base(monster) { }
    Vector3 _originPos;
    public override void OnStateEnter()
    {
        //���� ��ġ ����
        _originPos = _monster.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
    }

    public override void OnStateUpdate()
    {
        //���� �Ÿ� ��ȸ
        //�������� ��� ��ȸ
        //���������� �÷��̾ ���� �Ÿ� �ȿ� ���´ٸ� Exit�� ���� ��ȯ
        float awayRange = Random.Range(0, _monster._mStat.AwayRange + 1);     
        _monster._nav.Move(_originPos + new Vector3(awayRange, awayRange, awayRange));
    }
}
public class MoveState : MonsterBaseState
{
    public MoveState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //�÷��̾� ã��
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
    }

    public override void OnStateUpdate()
    {
        //�÷��̾� �߰�
    }
}

public class AttackState : MonsterBaseState
{
    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
       //�÷��̾� ����
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
    }

    public override void OnStateUpdate()
    {
        //������ �� �÷��̾� ����
    }
}

public class SkillState : MonsterBaseState
{
    public SkillState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //�÷��̾�� ��ų ���
    }

    public override void OnStateExit()
    {
       //�ٸ� ���·� ��ȯ
    }

    public override void OnStateUpdate()
    {
       //������ �� �÷��̾�� ��ų ���
    }
}

public class DamagedState : MonsterBaseState
{
    public DamagedState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //�˹�, ������ �ޱ�
    }

    public override void OnStateExit()
    {
        //�ٸ� ���·� ��ȯ
    }

    public override void OnStateUpdate()
    {
       //������Ʈ�� ���� �ֳ�?
    }
}

public class DieState : MonsterBaseState
{
    public DieState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //�״� ���
    }

    public override void OnStateExit()
    {
        //exit�� �ʿ��ұ�
    }

    public override void OnStateUpdate()
    {
        //������ ������
        //�ڱ� �ı��ϱ�
    }
}

public class ReturnState : MonsterBaseState
{
    public ReturnState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
    }

    public override void OnStateExit()
    {
        //originPos��� Idle�� ���� ��ȯ
        //�ƴ϶�� �÷��̾� �߰�(Move�� ���� ��ȯ)
    }

    public override void OnStateUpdate()
    {
        //����ؼ� �̵��ϴٰ� hpȸ��
        //�ٽ� �¾��� �� ��ġ �Ǵ��ؼ� return�Ÿ����� �۾����ٸ� �÷��̾� �߰�
    }
}