using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 _moveDir;

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

    // �̵��� ���õ� �Է��� ������ �̵� ���·� ��ȯ
    void MoveInput()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            _moveDir = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.W))
        {
            // ȸ�� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // ȸ�� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // ȸ�� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // ȸ�� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        }

        _moveDir = transform.forward * Time.deltaTime * _player._playerStat._moveSpeed;
        //_player._cc.Move(_moveDir);
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
