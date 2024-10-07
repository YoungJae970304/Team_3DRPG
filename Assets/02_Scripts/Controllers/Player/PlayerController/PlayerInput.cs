using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Player _player;
    [HideInInspector]
    public Vector3 _dir;

    public Queue<int> _atkInput = new Queue<int>();

    private void Awake()
    {
        _player = gameObject.GetOrAddComponent<Player>();
        Managers.Game._player = _player;
    }

    void Start()
    {
        Managers.Input.KeyAction -= MoveInput;
        Managers.Input.KeyAction += MoveInput;

        Managers.Input.KeyAction -= DodgeInput;
        Managers.Input.KeyAction += DodgeInput;

        Managers.Input.KeyAction -= AttackInput;
        Managers.Input.KeyAction += AttackInput;

        Managers.Input.KeyAction -= SkillInput;
        Managers.Input.KeyAction += SkillInput;

        Managers.Input.KeyAction -= UIInput;
        Managers.Input.KeyAction += UIInput;
    }

    // 이동 관련 입력 받고 상태전환을 위한 bool변수인 _isMoving에 접근 
    void MoveInput()
    {
        if (_player._dodgeing) return;

        _player._rotDir = Vector3.zero;
        _player._isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            _dir = _player._playerCam._cameraArm.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _dir = -_player._playerCam._cameraArm.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _dir = -_player._playerCam._cameraArm.transform.forward;
            _dir.y = 0;
            _player._rotDir += _dir;

            _player._isMoving = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _dir = _player._playerCam._cameraArm.transform.right;
            _dir.y = 0;
            _player._rotDir += _dir;
            _player._isMoving = true;
        }

        _player._rotDir.Normalize();
    }

    void DodgeInput()
    {
        if (_player._skillUsing || _player._dodgeing) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeState(PlayerState.Dodge);
        }
    }

    void AttackInput()
    {
        if (_player._dodgeing || _player._skillUsing || !_player._canAtkInput) return;

        if (Input.GetMouseButtonDown(0))
        {
            _player.AtkCount++;

            // Queue에 Enqueue함으로써 선입력 처리
            InputBufferInsert(_player.AtkCount);
            //_player.ChangeState(PlayerState.AttackWait);
        }
        else if (Input.GetMouseButtonDown(1) && !_player._attacking)
        {
            _player.Special();
        }
    }

    void SkillInput()
    {
        if (_player._dodgeing) return;

        //_player._skillBase = 스킬 무언가가 들어가나?

        // 추후 스킬 들어오는거에 맞춰 수정 필요
        // _player.ChangeState(PlayerState.Skill); 하나만 쓰고 
        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.ChangeState(PlayerState.Skill);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _player.ChangeState(PlayerState.Skill);
        }
    }

    public void InputBufferInsert(int action)
    {
        if(_atkInput.Count > 1) { return; }

        _atkInput.Enqueue(action);
    }

    void UIInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseFrontUI();
        }
    }

    public void OpenInventory()
    {
        InventoryUI inventoryUI = Managers.UI.GetActiveUI<InventoryUI>() as InventoryUI;
        if (inventoryUI != null)
        {
            Managers.UI.CloseUI(inventoryUI);
        }
        else {
            // 인벤토리 여는 것 I? ( 풀링 )
            Managers.UI.OpenUI<InventoryUI>(new BaseUIData());
        }
        
    }
    public void CloseFrontUI()
    {
        //ESC
        Managers.UI.CloseCurrFrontUI();
    }

    // 사용할일이 있을까?
    public void OpenNewtest()
    {
        // 완전 새로운 오브젝트를 생성 후 여는 것 ( 풀링은 되는데 무조건 생성 )
        Managers.UI.OpenUI<InventoryUI>(new BaseUIData(), true, true);
    }
    public void Remove()
    {
        // 오브젝트 자체를 삭제 ( 1번만 쓰는 UI 같은거 )
        Managers.UI.CloseCurrFrontUI(true);
    }
}
