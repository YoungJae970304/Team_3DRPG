using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player _player;

    void Start()
    {
        _player = GetComponent<Player>();

        Managers.Input.KeyAction -= MoveInput;
        Managers.Input.KeyAction += MoveInput;

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
            _player._rotDir += Vector3.forward;
            _player._isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _player._rotDir += Vector3.left;
            _player._isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _player._rotDir += Vector3.back;
            _player._isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _player._rotDir += Vector3.right;
            _player._isMoving = true;
        }
    }

    void AttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }

    void SkillInput()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
        {

        }
    }
}
