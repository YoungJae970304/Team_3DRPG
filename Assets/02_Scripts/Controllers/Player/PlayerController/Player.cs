using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

// 상태 
public enum PlayerState
{
    Idle,
    Move,
    Dodge,
    Attack,
    AttackWait,
    Skill,
    Damaged,
    Dead
}

public enum PlayerHitState
{
    NomalAttack,
    SkillAttack,
    StunAttack,
}

public abstract class Player : MonoBehaviour, IDamageAlbe
{
    // 참조용 변수
    Monster _monster;

    // 기타 변수
    [HideInInspector]
    public PlayerHitState _playerHitState;
    [HideInInspector]
    public bool _canAnyInput;

    // 애니메이션 관련 변수
    [HideInInspector]
    public Animator _playerAnim;

    [Header("오브젝트 참조")]
    public Transform _playerModel;
    public UI_Cursor _cursorUI;
    [HideInInspector]
    public Define.PlayerType _playerType;

    // 이동 관련 변수
    [HideInInspector]
    public Vector3 _moveDir;
    [HideInInspector]
    public Vector3 _rotDir;
    [Header("회전 속도")]
    public float _rotSpeed = 0.2f;
    [HideInInspector]
    public float gravity = -9.81f;
    private Vector3 verticalVelocity;

    // 회피 관련 변수
    // 무적 변수
    [Header("회피 무적 체크")]
    public bool _invincible = false;

    // 피격 관련 변수
    [Header("일반피격 무적 시간")]
    public float _hitDelay = 0.5f;

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
    [Header("공격 콜라이더 리스트")]
    public List<Collider> _atkColliders;
    //[HideInInspector]
    public List<Collider> _hitMobs;

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
    public PlayerInput _playerInput;
    [HideInInspector]
    public PlayerCam _playerCam;
    [HideInInspector]
    public PlayerStatManager _playerStatManager;
    [HideInInspector]
    public InterectController _interectController;
    protected virtual void Awake()
    {
        #region 컴포넌트 초기화
        _cc = gameObject.GetOrAddComponent<CharacterController>();
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        _playerCam = gameObject.GetOrAddComponent<PlayerCam>();
        _interectController = gameObject.GetOrAddComponent<InterectController>();
        _playerAnim = GetComponentInChildren<Animator>();
        _playerStatManager = new PlayerStatManager();
        #endregion

        _playerStatManager._originStat = new PlayerStat();
        _playerStatManager._equipStat = new PlayerStat();
        _playerStatManager._buffStat = new PlayerStat();
    }

    protected virtual void Start()
    {
        #region 딕셔너리 초기화
        States.Add(PlayerState.Idle, new PlayerIdleState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Move, new PlayerMoveState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Dodge, new PlayerDodgeState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Attack, new PlayerAttackState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Skill, new PlayerSkillState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Damaged, new PlayerDamagedState(this, _monster, _playerStatManager));
        States.Add(PlayerState.Dead, new PlayerDeadState(this, _monster, _playerStatManager));
        States.Add(PlayerState.AttackWait, new PlayerAttackWaitState(this, _monster, _playerStatManager));
        #endregion

        #region 변수 초기화
        // 초기 상태
        _curState = PlayerState.Idle;
        _pFsm = new FSM(States[PlayerState.Idle]);
        _canAtkInput = true;

        _playerStatManager._originStat.MaxHP = 10000;
        _playerStatManager.HP = _playerStatManager._originStat.MaxHP;
        _playerStatManager._originStat.MaxMP = 100;
        _playerStatManager.MP = _playerStatManager._originStat.MaxMP;
        _playerStatManager._originStat.MoveSpeed = 5f;
        _playerStatManager._originStat.DodgeSpeed = 10f;
        _playerStatManager._originStat.ATK = 50;
        _playerStatManager._originStat.DEF = 50;

        // 공격 콜라이더 off
        //SetColActive("Combo1");
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
        // 중력 적용
        ApplyGravity();

        _pFsm.FixedUpdateState();
    }

    private void ApplyGravity()
    {
        if (_cc.isGrounded && verticalVelocity.y < 0)
        {
            verticalVelocity.y = -2f; // 약간의 하향 속도 유지
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime;
        }

        _cc.Move(new Vector3(0, verticalVelocity.y, 0) * Time.fixedDeltaTime);
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
                // 선입력 있다면 공격상태로
                else if (_playerInput._atkInput.Count > 0)
                {
                    ChangeState(PlayerState.Attack);
                }
                break;
            case PlayerState.Move:
                // Move -> Idle
                if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                // 선입력 있다면 공격상태로
                else if (_playerInput._atkInput.Count > 0)
                {
                    ChangeState(PlayerState.Attack);
                }
                break;
            case PlayerState.Dodge:
                // 회피 중일때는 상태전환 불가
                if (_dodgeing) return;

                // 공격 전환 추가
                if (_playerInput._atkInput.Count > 0)
                {
                    ChangeState(PlayerState.Attack);
                }
                else if (!_isMoving)
                {
                    ChangeState(PlayerState.Idle);
                }
                else
                {
                    ChangeState(PlayerState.Move);
                }
                break;
            case PlayerState.Attack:
                if (_canAtkInput)
                {
                    ChangeState(PlayerState.AttackWait);
                }
                break;
            case PlayerState.AttackWait:
                // 공격 선입력이 있다면 공격상태로 전환
                if (_playerInput._atkInput.Count > 0)
                {
                    ChangeState(PlayerState.Attack);
                }

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
                if (_hitting) return;

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

    // 공격관련 콜라이더 제어
    public void SetColActive(string colName)
    {
        foreach (var col in _atkColliders)
        {
            col.gameObject.SetActive(col.name == colName);
        }
    }

    // 자식(Melee, Ranged Player)의 공격 부분 구현
    public virtual void Attack()
    {

    }

    public void ApplyDamage()
    {
        if (_hitMobs.Count == 0) return;

        int damage = _playerStatManager._originStat.ATK;

        foreach(var mob in _hitMobs)
        {
            if (mob.TryGetComponent<IDamageAlbe>(out var damageable))
            {
                damageable.Damaged(damage);
            }
        }

        _hitMobs.Clear();
    }

    public abstract void Skill();

    // 우클릭 시 발생하는 행동
    public abstract void Special();

    public virtual void Damaged(int atk)
    {
        // 회피 = 모션이랑 다르게 회피 후 잠깐 무적
        // 피격 = 피격 모션 중 통짜 무적
        if (_invincible) return;

        // 체력- 공격력*(100f/(방어력+100f))
        _playerStatManager.HP -= (int)(atk * (100f/(_playerStatManager.DEF+100f)));
        if (_playerStatManager.HP > 0)
        {
            _hitting = true;
            _invincible = true;

            if (_playerHitState == PlayerHitState.SkillAttack || _playerHitState == PlayerHitState.StunAttack)
            {
                ChangeState(PlayerState.Damaged);
            }
            else
            {
                InvincibleDelay();
            }
        }
        else
        {
            ChangeState(PlayerState.Dead);
        }
    }

    public void InvincibleDelay()
    {
        StartCoroutine(HitDelayCo());
    }

    // 1회성인 데미지에서만 실행되니까 이건 코루틴으로 바꾸던가 해야할듯, 움찔하지 않는 피격의 경우에도 사용해야 하니까 Damage상태에서는 못쓸것같음
    IEnumerator HitDelayCo()
    {
        yield return new WaitForSeconds(_hitDelay);

        _invincible = false;
    }

    // 현재 공격 도중 회피 하면 타이머가 진행중에 끊기기때문에 다음 공격이 엄청 짧아짐
    // 이는 추후 애니메이션 이벤트로 처리하게 될시 자동으로 해결될 것
    #region 타이머들(추후 anim이벤트로 일부 변경)

    #endregion
}
