using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.AI;

public class Ork : Monster
{
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 100)
        {
            NomalAttack();
        }
        else
        {
            SkillAttack();
        }
    }
    public void NomalAttack()
    {
        Logger.Log("NomalAttack");
        _mAttackState = MAttackState.NomalAttack;
        AttackPlayer();
    }
    public void SkillAttack()
    {
        Logger.Log("SkillAttack");
        _mAttackState = MAttackState.SkillAttack;
        AttackPlayer();
    }

    public override async void StartDamege(Vector3 playerPosition, float delay, float pushBack)
    {
        _nav.enabled = false;
        // 넉백 방향 계산
        Vector3 diff = (transform.position - playerPosition).normalized; // 플레이어 반대 방향
        Vector3 force = diff * pushBack; // 넉백 힘

        // Rigidbody 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = false; // 물리 효과 활성화


        // 넉백 힘 적용
        rb.AddForce(force, ForceMode.Impulse);

        // 넉백 후 처리
        await Task.Delay((int)(delay * 1000)); // 넉백 지속 시간 (필요에 따라 조정)

        // 넉백이 끝나면 NavMeshAgent를 다시 활성화


        _nav.enabled = true;
        rb.isKinematic = true; // 다시 비활성화 (필요시)
        if (CanAttackPlayer())
            MChangeState(MonsterState.Attack);
        else
            MChangeState(MonsterState.Move);
    }
}
