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
            //_player._rotDir += Vector3.forward;
            _dir = _player._camera.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //_player._rotDir += Vector3.left;
            _dir = -_player._camera.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //_player._rotDir += Vector3.back;
            _dir = -_player._camera.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //_player._rotDir += Vector3.right;
            _dir = _player._camera.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;
            _player._isMoving = true;
        }
    }

    void DodgeInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeState(PlayerState.Dodge);
        }
    }

    void AttackInput()
    {
        if (Input.GetMouseButtonDown(0) && !_player._attacking)
        {
            _player.AtkCount++;
            // 플레이어의 현재 정면을 queue에 저장
            InputBufferInsert(_player.AtkCount);
            _player.ChangeState(PlayerState.Attack);
        }
        else if (Input.GetMouseButtonDown(1) && !_player._attacking)
        {
            _player.AtkCount = 0;
            // 플레이어의 현재 정면을 queue에 저장
            InputBufferInsert(_player.AtkCount);
            _player.ChangeState(PlayerState.Attack);
        }
    }

    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        {

        }
    }

    public void InputBufferInsert(int action)
    {
        if(_atkInput.Count > 1) { return; }

        _atkInput.Enqueue(action);
    }
}
