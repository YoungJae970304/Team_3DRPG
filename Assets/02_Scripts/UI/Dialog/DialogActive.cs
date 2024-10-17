using UnityEngine;

public class DialogActive : MonoBehaviour
{
    bool _isDialogOpen = false;
    PlayerInput _playerInput;

    private void Start()
    {
        _playerInput = FindObjectOfType<PlayerInput>();
    }

    public void DialogActiveState(bool isOpen)
    {
        _isDialogOpen = isOpen;
        if (_isDialogOpen)
        {
            //_playerInput.enabled = false;
            //_playerInput.SetDialog(true);
        }
        else
        {
            //_playerInput.enabled = true;
            //_playerInput.SetDialog(false);
        }
    }
}
