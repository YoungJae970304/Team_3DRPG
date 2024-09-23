using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 상태 열거형
    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damaged,
        Dead
    }

    // 상태전환 관련 변수
    PlayerState _curState;  // 현재 상태
    PlayerFSM _pFsm;

    protected void Start()
    {
        // 초기 상태
        _curState = PlayerState.Idle;
        _pFsm = new PlayerFSM(new PlayerIdleState(this));
    }

    protected void Update()
    {
        // 상태 전환
        ChangeStateCondition();

        // 상태 내부의 업데이트 실행
        _pFsm.UpdateState();
    }

    // 플레이어 상태 전환 조건을 담당하는 메서드
    protected void ChangeStateCondition()
    {
        switch (_curState)
        {
            case PlayerState.Idle:
                // Idle에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Move:
                // Move에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Attack:
                // Attack에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Skill:
                // Skill에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Damaged:
                // Damaged에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Dead:
                // Dead에서 다른 상태로 이동하기 위한 조건
                break;
        }
    }

    // 실제 상태 전환을 해주는 메서드
    protected void ChangeState(PlayerState nextState)
    {
        _curState= nextState;
        switch(_curState)
        {
            case PlayerState.Idle:
                _pFsm.ChangeState(new PlayerIdleState(this));
                break;
            case PlayerState.Move:
                _pFsm.ChangeState(new PlayerMoveState(this));
                break;
            case PlayerState.Attack:
                _pFsm.ChangeState(new PlayerAttackState(this));
                break;
            case PlayerState.Skill:
                _pFsm.ChangeState(new PlayerSkillState(this));
                break;
            case PlayerState.Damaged:
                _pFsm.ChangeState(new PlayerDamagedState(this));
                break;
            case PlayerState.Dead:
                _pFsm.ChangeState(new PlayerDeadState(this));
                break;
        }
    }

    // 자식(Melee, Ranged Player)의 공격 부분 구현 ( AttackState에서 사용 )
    public virtual void Attack()
    {
        
    }
}
