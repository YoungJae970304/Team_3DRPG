using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���� ������
    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Skill,
        Damaged,
        Dead
    }

    // ������ȯ ���� ����
    PlayerState _curState;  // ���� ����
    PlayerFSM _pFsm;

    protected void Start()
    {
        // �ʱ� ����
        _curState = PlayerState.Idle;
        _pFsm = new PlayerFSM(new PlayerIdleState(this));
    }

    protected void Update()
    {
        // ���� ��ȯ
        ChangeStateCondition();

        // ���� ������ ������Ʈ ����
        _pFsm.UpdateState();
    }

    // �÷��̾� ���� ��ȯ ������ ����ϴ� �޼���
    protected void ChangeStateCondition()
    {
        switch (_curState)
        {
            case PlayerState.Idle:
                // Idle���� �ٸ� ���·� �̵��ϱ� ���� ����
                break;
            case PlayerState.Move:
                // Move���� �ٸ� ���·� �̵��ϱ� ���� ����
                break;
            case PlayerState.Attack:
                // Attack���� �ٸ� ���·� �̵��ϱ� ���� ����
                break;
            case PlayerState.Skill:
                // Skill���� �ٸ� ���·� �̵��ϱ� ���� ����
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

    // �ڽ�(Melee, Ranged Player)�� ���� �κ� ���� ( AttackState���� ��� )
    public virtual void Attack()
    {
        
    }
}
