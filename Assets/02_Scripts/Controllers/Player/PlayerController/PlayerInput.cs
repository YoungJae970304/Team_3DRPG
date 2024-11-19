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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        // 이벤트에 등록
        Managers.Input.KeyAction -= MoveInput;
        Managers.Input.KeyAction += MoveInput;

        Managers.Input.KeyAction -= DodgeInput;
        Managers.Input.KeyAction += DodgeInput;

        Managers.Input.KeyAction -= SkillInput;
        Managers.Input.KeyAction += SkillInput;

        Managers.Input.KeyAction -= UIInput;
        Managers.Input.KeyAction += UIInput;

        Managers.Input.MouseAction -= AttackInput;
        Managers.Input.MouseAction += AttackInput;
    }

    // 이동 관련 입력 받고 상태전환을 위한 bool변수인 _isMoving에 접근 
    void MoveInput()
    {
        _player._isMoving = false;

        // 예외처리
        if (_player._hitting || _player._dodgeing || Managers.Game._cantInputKey || !_player._canAtkInput) return;

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

    // 회피 입력
    void DodgeInput()
    {
        // 예외처리
        if (_player._hitting || _player._skillUsing || _player._dodgeing || Managers.Game._cantInputKey || _player._curState == PlayerState.Dead ) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeState(PlayerState.Dodge);
        }
    }

    // 공격 입력
    void AttackInput()
    {
        // 예외처리
        if (_player._hitting || _player._skillUsing || !_player._canAtkInput) return;
           

        if (Input.GetMouseButtonDown(0))
        {
            _player.AtkCount++;

            // Queue에 Enqueue함으로써 선입력 처리
            InputBufferInsert(_player.AtkCount);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _player.Special();
        }
    }
    // 선입력
    public void InputBufferInsert(int action)
    {
        if (_atkInput.Count > 1) { return; }

        _atkInput.Enqueue(action);
    }

    // 스킬입력
    void SkillInput()
    {
        if (_player._hitting || _player._dodgeing || _player._skillUsing || Managers.Game._cantInputKey) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _player.SkillSetE();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            _player.SkillSetR();
        }
    }

    // UI 관련 키입력
    void UIInput()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenPlayerUI<InventoryUI>();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Managers.UI.ExistsOpenUI())
            {
                CloseFrontUI();
            }
            else
            {
                OpenPlayerUI<OptionUI>();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            if (Managers.Game._cantInputKey) { return; }

            _player._interectController.Interection();
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            OpenPlayerUI<LargeMapUI>();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            SkillTreeData skillTreeData = new SkillTreeData(Managers.Game._playerType);
            OpenPlayerUI<SkillTree>(skillTreeData);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            OpenPlayerUI<EquipMentUI>();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.QuestManager._questInput = Define.QuestInput.Q;
            OpenPlayerUI<QuestUI>();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))//퀵슬롯 사용
        {
            MainUI mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
            mainUI.ItemSlot_1.Use();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MainUI mainUI = Managers.UI.GetActiveUI<MainUI>() as MainUI;
            mainUI.ItemSlot_2.Use();
        }
    }

    // UI를 열고 닫는 메서드. 데이터 입력 없는 디폴트
    public void OpenPlayerUI<T>() where T : BaseUI
    {
        T playerUI = Managers.UI.GetActiveUI<T>() as T;
        if (playerUI != null)
        {
            Managers.UI.CloseUI(playerUI);

            Managers.Sound.Play("ETC/ui_close");
        }
        else
        {
            if (Managers.Scene.LoadingSceneCheck()) return;

            Managers.UI.OpenUI<T>(new BaseUIData());
        }
    }
    // 데이터 입력을 받는 함수 오버로딩
    public void OpenPlayerUI<T>(BaseUIData baseUIData) where T : BaseUI
    {
        T playerUI = Managers.UI.GetActiveUI<T>() as T;
        if (playerUI != null)
        {
            Managers.UI.CloseUI(playerUI);

            Managers.Sound.Play("ETC/ui_close");
        }
        else
        {
            if (Managers.Scene.LoadingSceneCheck()) return;

            Managers.UI.OpenUI<T>(baseUIData);
        }
    }

    // 라지맵 초기화용
    public void LargeMapInit()
    {
        LargeMapUI largeMapUI = Managers.UI.GetActiveUI<LargeMapUI>() as LargeMapUI;
        if (largeMapUI != null)
        {
            Managers.UI.CloseUI(largeMapUI);
        }
        else
        {
            if (Managers.Scene.LoadingSceneCheck()) return;

            Managers.UI.OpenUI<LargeMapUI>(new BaseUIData());
        }
    }

    public void CloseFrontUI()
    {
        //ESC
        Managers.UI.CloseCurrFrontUI();
    }
}
