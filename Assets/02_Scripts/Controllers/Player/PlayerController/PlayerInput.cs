using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player _player;
    Vector3 _dir;

    Queue<Vector3> _atkInput = new Queue<Vector3>();

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

    // �̵� ���� �Է� �ް� ������ȯ�� ���� bool������ _isMoving�� ����
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
            _player.ChangeState(Player.PlayerState.Dodge);
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
