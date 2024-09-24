using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeReturnState : MonsterBaseState
{
    public SlimeReturnState(Slime slime) : base(slime) { }

    public override void OnStateEnter()
    {
        //origin���� ã�Ƽ� �̵��ϱ�
        _slime._nav.Move(_slime._originPos);
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //originPos��� Idle�� ���� ��ȯ
        //�ƴ϶�� �÷��̾� �߰�(Move�� ���� ��ȯ)
        if ((_slime._originPos - _slime.transform.position).magnitude <= 0.1f)
        {
            _slime.ChangeState(Slime.State.Idle);
        }
    }

    public override void OnStateUpdate()
    {
        //�ٽ� �¾��� �� ��ġ �Ǵ��ؼ� return�Ÿ����� �۾����ٸ� �÷��̾� �߰�
        OnStateExit();
    }
}
