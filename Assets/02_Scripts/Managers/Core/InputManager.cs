using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

// 매니저스가 있어서 굳이 컴포넌트로 만들 필요가 없으니 POCO로 만듬 ( MonoBehaviour가 없음 )
public class InputManager
{
    public Action KeyAction = null;
    public Action AXis = null;
    bool _isPress = false;


    // 대표로 입력을 체크한 다음 실제로 입력이 있으면 그것을 이벤트로 전파하는 형식으로 구현 ( 리스너 패턴 )
    // 이렇게 관리하면 어디서 키보드 입력을 받았는지 찾기 어려운 문제가 해소됨
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
