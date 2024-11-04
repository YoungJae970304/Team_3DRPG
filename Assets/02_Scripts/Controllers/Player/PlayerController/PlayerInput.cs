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

        Managers.Input.KeyAction -= SkillInput;
        Managers.Input.KeyAction += SkillInput;

        Managers.Input.KeyAction -= UIInput;
        Managers.Input.KeyAction += UIInput;

        Managers.Input.MouseAction -= AttackInput;
        Managers.Input.MouseAction += AttackInput;

        //// 한번 실행시켰다 꺼줌으로써 맵 업데이트가 가능하도록
        //OpenLargeMap();
        //OpenLargeMap();
    }

    // 이동 관련 입력 받고 상태전환을 위한 bool변수인 _isMoving에 접근 
    void MoveInput()
    {
        _player._isMoving = false;

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
        if (_player._hitting || _player._skillUsing || _player._dodgeing || Managers.Game._cantInputKey) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ChangeState(PlayerState.Dodge);
        }
    }

    // 공격 입력
    void AttackInput()
    {
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

        else if (Input.GetKeyDown(KeyCode.B))
        {
            //_player._playerStatManager.EXP += 1000;
            //Logger.LogError($"초기화 확인 {Managers.DataTable._PlayerStat[0]}");
            Logger.LogError($"초기화 확인 {Managers.Game._player._playerStatManager.HP}");
            Logger.LogError($"초기화 확인 {Managers.Game._player._playerStatManager.Level}");
            Logger.LogError($"초기화 확인 {Managers.Game._player._playerStatManager.SpAddAmount}");
            Logger.LogError($"초기화 확인 {Managers.Game._player._playerStatManager.MaxEXP}");
            Logger.LogError($"돈 확인 {Managers.Game._player._playerStatManager.Gold}");
            
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            _player._playerStatManager.EXP += 100;
            _player._playerStatManager.Gold += 100000;
            _player._playerStatManager.HP = _player._playerStatManager.MaxEXP;
            _player._playerStatManager.MP = _player._playerStatManager.MaxMP;
            //try
            //{
            //    Managers.Data.SaveData<PlayerSaveData>();
            //    Logger.Log($"플레이어 데이터 저장{_player._playerStatManager.EXP}\n{_player._playerStatManager.Gold}");
            //}catch(System.Exception e)
            //{
            //    Logger.LogError($"플레이어 데이터 저장 실패{e.Message}");
            //}
             
        }
    }
         
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

            //skillTreeData.path = "test";
            //Managers.UI.OpenUI<SkillTree>(skillTreeData);
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
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Logger.LogError("test1");
            PubAndSub.Publish<int>("80001",80001);
            Logger.LogError("test2");
        }    
    }

    // 데이터 입력 없는 디폴트
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
