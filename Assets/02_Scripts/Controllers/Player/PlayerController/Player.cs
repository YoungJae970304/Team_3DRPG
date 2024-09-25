using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ����
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

public class Player : MonoBehaviour, IDamageAlbe
{
    // ��Ÿ ����
    [HideInInspector]
    public Camera _camera;
    [Header("������Ʈ ����")]
    public Transform _cameraArm;
    public Transform _playerModel;

    // �̵� ���� ����
    [HideInInspector]
    public Vector3 _moveDir;
    [HideInInspector]
    public Vector3 _rotDir;
    [HideInInspector]
    public bool _isMoving = false;
    [Header("ȸ�� �ӵ�")]
    public float _rotSpeed = 0.2f;

    // ȸ�� ���� ����
    [HideInInspector]
    public bool _dodgeing = false;
    [Header("ȸ�� �ð�")]
    public float _dodgeTime = 0.5f;

    // ���� ���� ����
    [HideInInspector]
    public bool _attacking = false;
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

    // ������ȯ ���� ����
    PlayerState _curState;  // ���� ����
    PlayerFSM _pFsm;
    Dictionary<PlayerState, PlayerBaseState> States = new Dictionary<PlayerState, PlayerBaseState>();

    // ������Ʈ
    [HideInInspector]
    public CharacterController _cc;
    [HideInInspector]
    public PlayerStat _playerStat;
    public PlayerInput _playerInput;

    protected void Start()
    {
        #region ������Ʈ �ʱ�ȭ
        _cc = gameObject.GetOrAddComponent<CharacterController>();
        _playerStat = gameObject.GetOrAddComponent<PlayerStat>();
        _playerInput = gameObject.GetOrAddComponent<PlayerInput>();
        #endregion

        #region ��ųʸ� �ʱ�ȭ
        States.Add(PlayerState.Idle, new PlayerIdleState(this));
        States.Add(PlayerState.Move, new PlayerMoveState(this));
        States.Add(PlayerState.Dodge, new PlayerDodgeState(this));
        States.Add(PlayerState.Attack, new PlayerAttackState(this));
        States.Add(PlayerState.Skill, new PlayerSkillState(this));
        States.Add(PlayerState.Damaged, new PlayerDamagedState(this));
        States.Add(PlayerState.Dead, new PlayerDeadState(this));
        #endregion

        #region ���� �ʱ�ȭ
        // �ʱ� ����
        _curState = PlayerState.Idle;
        _pFsm = new PlayerFSM(States[PlayerState.Idle]);
        _camera = Camera.main;
        #endregion
    }

    protected void Update()
    {
        // ���� ��ȯ
        ChangeStateCondition();

        // ���� ������ ������Ʈ ����
        _pFsm.UpdateState();
    }

    protected void FixedUpdate()
    {
        _pFsm.FixedUpdateState();
    }

    // �÷��̾� ���� ��ȯ ������ ����ϴ� �޼���
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
                if (!_dodgeing)
                {
                    if (!_isMoving)
                    {
                        ChangeState(PlayerState.Idle);
                    }
                    else
                    {
                        ChangeState(PlayerState.Move);
                    }
                }
                break;
            case PlayerState.Attack:
                // Attack���� �ٸ� ���·� �̵��ϱ� ���� ����
                if (!_attacking)
                {
                    if (!_isMoving)
                    {
                        ChangeState(PlayerState.Idle);
                    }
                    else
                    {
                        ChangeState(PlayerState.Move);
                    }
                }
                break;
            case PlayerState.Skill:
                // Skill���� �ٸ� ���·� �̵��ϱ� ���� ����
                // if KeyUp -> ������ -> ���� ��ȯ
                break;
            case PlayerState.Damaged:
                // Damaged���� �ٸ� ���·� �̵��ϱ� ���� ����
                break;
            case PlayerState.Dead:
                // Dead���� �ٸ� ���·� �̵��ϱ� ���� ����
                break;
        }
    }

    // ���� ���� ��ȯ�� ���ִ� �޼���
    public void ChangeState(PlayerState nextState)
    {
        _curState = nextState;

        _pFsm.ChangeState(States[_curState]);
    }

    // �ڽ�(Melee, Ranged Player)�� ���� �κ� ���� ( AttackState���� ��� )
    public virtual void Attack()
    {

    }

    public void Damaged(int amount)
    {
        ChangeState(PlayerState.Damaged);
    }
}
