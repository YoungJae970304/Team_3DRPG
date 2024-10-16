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
            //Managers.Input.KeyAction = null;
        }
        else
        {
            if (_originKeyAction != null)
            {
                Managers.Input.KeyAction = _originKeyAction;
            }
        }
    }
}
