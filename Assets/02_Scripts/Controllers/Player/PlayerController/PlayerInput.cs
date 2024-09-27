using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player _player;
    Vector3 _dir;

    public Queue<int> _atkInput = new Queue<int>();

    void Start()
    {
        _player = gameObject.GetOrAddComponent<Player>();

        Managers.Input.KeyAction -= MoveInput;
        Managers.Input.KeyAction += MoveInput;

        Managers.Input.KeyAction -= DodgeInput;
        Managers.Input.KeyAction += DodgeInput;

        Managers.Input.KeyAction -= AttackInput;
        Managers.Input.KeyAction += AttackInput;

        Managers.Input.KeyAction -= SkillInput;
        Managers.Input.KeyAction += SkillInput;

    }

    // 이동 관련 입력 받고 상태전환을 위한 bool변수인 _isMoving에 접근 
    void MoveInput()
    {
        _player._rotDir = Vector3.zero;
        _player._isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            _dir = _player._playerCam._cameraArm.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _dir = -_player._playerCam._cameraArm.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _dir = -_player._playerCam._cameraArm.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _dir = _player._playerCam._cameraArm.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;
            _player._isMoving = true;
        }
    }

    void DodgeInput()
    {
        if (_player._attacking || _player._skillUsing) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeState(PlayerState.Dodge);
        }
    }

    void AttackInput()
    {
        if (_player._dodgeing || _player._skillUsing) return;

        if (Input.GetMouseButtonDown(0) && _player._canAtkInput)
        {
            _player.AtkCount++;

            // Queue에 Enqueue함으로써 선입력 처리
            InputBufferInsert(_player.AtkCount);
            _player.ChangeState(PlayerState.Attack);
        }
        else if (Input.GetMouseButtonDown(1) && _player._canAtkInput)
        {
            _player.Special();
        }
    }

    void SkillInput()
    {
        if (_player._dodgeing) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player._skillIndex = 1;
            _player.ChangeState(PlayerState.Skill);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _player._skillIndex = 2;
            _player.ChangeState(PlayerState.Skill);
        }
    }

    public void InputBufferInsert(int action)
    {
        if(_atkInput.Count > 1) { return; }
        _atkInput.Enqueue(action);

        Logger.Log(action.ToString());
    }
}
