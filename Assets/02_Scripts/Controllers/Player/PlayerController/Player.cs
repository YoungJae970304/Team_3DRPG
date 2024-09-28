using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

public abstract class Player : MonoBehaviour, IDamageAlbe
{
    // 참조용 변수
    Monster _monster;

    // 기타 변수
    [Header("오브젝트 참조")]
    public Transform _playerModel;
    [HideInInspector]
    public Define.PlayerType _playerType;

    // 이동 관련 변수
    [HideInInspector]
    public Vector3 _moveDir;
    [HideInInspector]
    public Vector3 _rotDir;
    [Header("회전 속도")]
    public float _rotSpeed = 0.2f;

    // 회피 관련 변수
    [Header("회피 시간")]
    public float _dodgeTime = 0.5f;

    // 공격 관련 변수
    [HideInInspector]
    public bool _canAtkInput = true;
    int _atkCount = 0;
    [HideInInspector]
    public int AtkCount
    {
        get
        {
            return _atkCount;
        }
        set
        {
            _atkCount = value;

            _atkCount = Mathf.Clamp(_atkCount, 0, 4);

            if (_atkCount > 3)
            {
                _atkCount = 1;
            }
        }
    }
    [HideInInspector]
    public int _curAtkCount;

    // 스킬 관련 변수
    [HideInInspector]
    public int _skillIndex = 0;
    [HideInInspector]
    public SkillBase _skillBase;

    // 상태전환 관련 변수
    PlayerState _curState;  // 현재 상태
    FSM _pFsm;
    Dictionary<PlayerState, BaseState> States = new Dictionary<PlayerState, BaseState>();
    [HideInInspector]
    public bool _isMoving = false;
    [HideInInspector]
    public bool _dodgeing = false;
    [HideInInspector]
    public bool _attacking = false;
    [HideInInspector]
    public bool _skillUsing = false;
    [HideInInspector]
    public bool _hitting = false;

    // 컴포넌트
    [HideInInspector]
    public CharacterController _cc;
    [HideInInspector]
    public PlayerStat _playerStat;
    [HideInInspector]
    public PlayerInput _playerInput;
    [HideInInspector]
    public PlayerCam _playerCam;

    protected virtual void Start()
    {
        #region 컴포넌트 초기화
        _cc = gameObject.GetOrAddComponent<CharacterController>();
        _playerStat = gameObject.GetOrAddComponent<PlayerStat>();
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        _playerCam = gameObject.GetOrAddComponent<PlayerCam>();
        #endregion

        #region 딕셔너리 초기화
        States.Add(PlayerState.Idle, new PlayerIdleState(this, _monster, _playerStat));
        States.Add(PlayerState.Move, new PlayerMoveState(this, _monster, _playerStat));
        States.Add(PlayerState.Dodge, new PlayerDodgeState(this, _monster, _playerStat));
        States.Add(PlayerState.Attack, new PlayerAttackState(this, _monster, _playerStat));
        States.Add(PlayerState.Skill, new PlayerSkillState(this, _monster, _playerStat));
        States.Add(PlayerState.Damaged, new PlayerDamagedState(this, _monster, _playerStat));
        States.Add(PlayerState.Dead, new PlayerDeadState(this, _monster, _playerStat));
        #endregion

        #region 변수 초기화
        // 초기 상태
        _curState = PlayerState.Idle;
        _pFsm = new FSM(States[PlayerState.Idle]);
        _canAtkInput = true;
        _playerStat.MoveSpeed = 5f;
        #endregion
    }

    protected virtual void Update()
    {
        // 상태 전환
        ChangeStateCondition();

        // 상태 내부의 업데이트 실행
        _pFsm.UpdateState();
    }

    protected virtual void FixedUpdate()
    {
        _pFsm.FixedUpdateState();
    }

    // 플레이어 상태 전환 조건을 담당하는 메서드
    protected virtual void ChangeStateCondition()
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
                // 회피 중일때는 상태전환 불가
                if (_dodgeing) return;

                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                else
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Attack:
                // 공격 중일때는 상태전환 불가
                if (_attacking) return;

                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                else
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Skill:
                // 스킬사용 중일때는 상태전환 불가
                if (_skillUsing) return;

                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                else
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Damaged:
                // Damaged에서 다른 상태로 이동하기 위한 조건
                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                else
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Dead:
                // Dead에서 다른 상태로 이동하기 위한 조건
                break;
        }
    }

    // 실제 상태 전환을 해주는 메서드
    public void ChangeState(PlayerState nextState)
    {
        _curState = nextState;

        _pFsm.ChangeState(States[_curState]);
    }

    // 자식(Melee, Ranged Player)의 공격 부분 구현 ( AttackState에서 사용 )
    public virtual void Attack()
    {
        switch (_curAtkCount)
        {
            case 0:
                Logger.Log("강공격");
                break;
            case 1:
                Logger.Log("기본공격 1타");
                break;
            case 2:
                Logger.Log("기본공격 2타");
                break;
            case 3:
                Logger.Log("기본공격 3타");
                break;
            default:
                Logger.LogError("지정한 공격이 아님");
                break;
        }

        // 추후 애니메이션 이벤트로 변경 예정

        // 애니메이션 시작 시 _canAtkInput = false, _attacking = true;
        // CanAtkInputOffTimer는 애니메이션의 중반쯤 _canAtkInput = true;

        // AtkOffTimer는 애니메이션 종료 직전에 if-else문(_playerInput._atkInput.Count < 1)으로 
        // _attacking = false;하거나 _curAtkCount = _playerInput._atkInput.Dequeue();
        CanAtkInputOffTimer(0.5f);
        AtkOffTimer(1.0f);
    }

    public abstract void Skill();

    // 우클릭 시 발생하는 행동
    public abstract void Special();

    public virtual void Damaged(int damage)
    {
        _playerStat.HP -= damage;
        ChangeState(PlayerState.Damaged);
        HitOffTimer(0.3f);
    }

    // 현재 공격 도중 회피 하면 타이머가 진행중에 끊기기때문에 다음 공격이 엄청 짧아짐
    // 이는 추후 애니메이션 이벤트로 처리하게 될시 자동으로 해결될 것
    #region 타이머들(추후 anim이벤트로 변경)
    // 추후 애니메이션 이벤트로 변경 예정
    float _curCAITime = 0;
    protected void CanAtkInputOffTimer(float targetTime)
    {
        _curCAITime += Time.deltaTime;

        if (_curCAITime >= targetTime)
        {
            _curCAITime = 0;
            _canAtkInput = true;
        }
    }

    // 추후 애니메이션 이벤트로 변경 예정
    float _curATime = 0;
    protected void AtkOffTimer(float targetTime)
    {
        _curATime += Time.deltaTime;

        if (_curATime >= targetTime)
        {
            _curATime = 0;

            // 선입력이 없다면 공격 중지
            if (_playerInput._atkInput.Count < 1)
            {
                _attacking = false;
            }
            else    // 선입력이 남아있다면 재공격 
            {
                _curAtkCount = _playerInput._atkInput.Dequeue();
            }
        }
    }

    // 추후 애니메이션 이벤트로 변경 예정
    float _curSTime = 0;
    protected void SkillOffTimer(float targetTime)
    {
        _curSTime += Time.deltaTime;

        if (_curSTime >= targetTime)
        {
            _curSTime = 0;

            _skillUsing = false;
        }
    }

    // 추후 애니메이션 이벤트로 변경 예정
    float _curHTime = 0;
    protected void HitOffTimer(float targetTime)
    {
        _curHTime += Time.deltaTime;

        if (_curHTime >= targetTime)
        {
            _curHTime = 0;

            _hitting = false;
        }
    }
    #endregion
}
