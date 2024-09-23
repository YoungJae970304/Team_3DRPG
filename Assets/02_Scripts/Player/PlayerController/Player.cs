using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 상태
    public enum PlayerState
    {
        Idle,
        Move,
        Dodge,
        Attack,
        Skill,
        Damaged,
        Dead
    }

    // 이동 관련 변수
    public Vector3 _moveDir;
    public Vector3 _rotDir;
    public bool _isMoving = false;
    public float _rotSpeed = 0.2f;

    // 상태전환 관련 변수
    PlayerState _curState;  // 현재 상태
    PlayerFSM _pFsm;
    Dictionary<PlayerState, PlayerBaseState> States = new Dictionary<PlayerState, PlayerBaseState>();

    // 컴포넌트
    [HideInInspector]
    public CharacterController _cc;
    [HideInInspector]
    public PlayerStat _playerStat;

    protected void Start()
    {
        #region 컴포넌트 초기화
        _cc = GetComponent<CharacterController>();
        _playerStat = GetComponent<PlayerStat>();
        #endregion

        #region 딕셔너리 초기화
        States.Add(PlayerState.Idle, new PlayerIdleState(this));
        States.Add(PlayerState.Move, new PlayerMoveState(this));
        States.Add(PlayerState.Dodge, new PlayerDodgeState(this));
        States.Add(PlayerState.Attack, new PlayerAttackState(this));
        States.Add(PlayerState.Skill, new PlayerSkillState(this));
        States.Add(PlayerState.Damaged, new PlayerDamagedState(this));
        States.Add(PlayerState.Dead, new PlayerDeadState(this));
        #endregion

        #region 변수 초기화
        // 초기 상태
        _curState = PlayerState.Idle;
        _pFsm = new PlayerFSM(States[PlayerState.Idle]);
        #endregion
    }

    protected void Update()
    {
        // 상태 전환
        ChangeStateCondition();

        // 상태 내부의 업데이트 실행
        _pFsm.UpdateState();
    }

    protected void FixedUpdate()
    {
        _pFsm.FixedUpdateState();
    }

    // 플레이어 상태 전환 조건을 담당하는 메서드
    protected void ChangeStateCondition()
    {
        switch (_curState)
        {
            case PlayerState.Idle:
                // Idle -> Move
                if (_isMoving)
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Move:
                // Move -> Idle
                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                break;
            case PlayerState.Dodge:
                break;
            case PlayerState.Attack:
                // Attack에서 다른 상태로 이동하기 위한 조건
                break;
            case PlayerState.Skill:
                // Skill에서 다른 상태로 이동하기 위한 조건
                // if KeyUp -> 딜레이 -> 상태 전환
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
        _curState = nextState;

        _pFsm.ChangeState(States[_curState]);
    }

    // 자식(Melee, Ranged Player)의 공격 부분 구현 ( AttackState에서 사용 )
    public virtual void Attack()
    {
        
    }
}
