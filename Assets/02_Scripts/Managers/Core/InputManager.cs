using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

// �Ŵ������� �־ ���� ������Ʈ�� ���� �ʿ䰡 ������ POCO�� ���� ( MonoBehaviour�� ���� )
public class InputManager
{
    public Action KeyAction = null;
    public Action AXis = null;
    bool _isPress = false;


    // ��ǥ�� �Է��� üũ�� ���� ������ �Է��� ������ �װ��� �̺�Ʈ�� �����ϴ� �������� ���� ( ������ ���� )
    // �̷��� �����ϸ� ��� Ű���� �Է��� �޾Ҵ��� ã�� ����� ������ �ؼҵ�
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (KeyAction != null)
        {
            if (_isPress && !Input.anyKey)
            {
                KeyAction.Invoke();
            }
            if (Input.anyKey)
            {
                KeyAction.Invoke();
            }
            _isPress = Input.anyKey;
        }
        /*
        if (KeyAction != null)
        {
            KeyAction.Invoke();
        }*/
    }

    public void Clear()
    {
        KeyAction = null;
    }
}
