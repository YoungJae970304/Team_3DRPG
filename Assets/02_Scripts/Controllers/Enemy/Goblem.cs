using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Goblem : Monster, IDamageAlbe
{
    public int _goblemID;
 
    public override void Start()
    {
        base.Start();
        GoblemIDCheck(_deongeonLevel);
        itemtest(_deongeonLevel, _goblemID);
    }
    public override void AttackStateSwitch()
    {
        if (_randomAttack <= 50)
        {
            _atkColliders[0].gameObject.SetActive(true);
            NomalAttack();
            _anim.SetTrigger("attack");
            
           
        }
        else
        {
            _atkColliders[1].gameObject.SetActive(true);
            SkillAttack();
            _anim.SetTrigger("attack1");
        }
    }
    public void NomalAttack()
    {
        Logger.Log("NomalAttack");
       
        _player._playerHitState = PlayerHitState.NomalAttack;
        AttackPlayer();
       
      
        
        
    }
    public void SkillAttack()
    {
        Logger.Log("SkillAttack");
        _player._playerHitState = PlayerHitState.SkillAttack;
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
        rb.freezeRotation = true;
        //여기에 애니메이션 멈추기 추가

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
    public void GoblemIDCheck(DeongeonLevel curLevel)
    {
        foreach (var gID in _dataTableManager._MonsterDropData)
        {
            string iDCheck = gID.ID.ToString("D0");
            if (iDCheck == "2")
            {
                switch (curLevel)
                {
                    case DeongeonLevel.Easy:
                        if (gID.ID.ToString("F1") == "1")
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                    case DeongeonLevel.Normal:
                        if (gID.ID.ToString("F1") == "2")
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                    case DeongeonLevel.Hard:
                        if (gID.ID.ToString("F1") == "3")
                        {
                            _goblemID = gID.ID;
                        }
                        break;
                }
            }
        }
    }
}
