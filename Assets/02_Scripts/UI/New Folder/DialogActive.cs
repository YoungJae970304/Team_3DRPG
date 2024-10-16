using UnityEngine;
using System;
using Unity.VisualScripting;

public class DialogActive : MonoBehaviour
{
    bool _isDialogOpen = false;
    Action _originKeyAction;
    PlayerInput _playerInput;
    public void DialogActiveState(bool isOpen)
    {
        _isDialogOpen = isOpen;

        if (_isDialogOpen)
        {
            _originKeyAction = Managers.Input.KeyAction;
            Logger.Log("키액션 저장 : " +_originKeyAction?.Method.Name);
            Managers.Input.KeyAction = null;
            Logger.Log("키액션 멈춤");
        }
        else
        {
            if (_originKeyAction != null)
            {
                Managers.Input.KeyAction = _originKeyAction;
            }
            else
            {
                Logger.Log("복구할 키 없음");
            }
        }
    }
}
