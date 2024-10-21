using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action UIMouseAction = null;
    public Action MouseAction = null;
    public Action AXis = null;
    bool _isPress = false;

    public void OnUpdate()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            UIMouseAction?.Invoke();
        }
        else
        {
            MouseAction?.Invoke();
        }

        // 다이얼로그에 ESC로 창닫기 적용 시킬거면 주석처리, ESC안먹게 하려면 주석 해제
        //if (Managers.Game._isActiveDialog) { return; }

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
