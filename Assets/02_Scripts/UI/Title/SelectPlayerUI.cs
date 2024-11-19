using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CameraType
{
    Center,
    Melee,
    Mage
}

public class SelectPlayerUI : BaseUI
{   
    // 확인창에 띄울 텍스트
    [Multiline(5)]
    [SerializeField] string descTxt;

    // 확인창 데이터
    ConfirmUIData confirmUIData = new ConfirmUIData();

    // 각 캐릭터 관련 변수
    GameObject _melee;
    GameObject _mage;
    Animator _meleeAnim;
    Animator _mageAnim;
    BoxCollider _meleeCol;
    BoxCollider _mageCol;

    // 플레이어 레이어 체크용
    [SerializeField] LayerMask _player;

    // 시네머신 카메라 변수
    CinemachineVirtualCamera _centerVCam;
    CinemachineVirtualCamera _meleeVCam;
    CinemachineVirtualCamera _mageVCam;

    CinemachineBrain _brain;

    // 카메라 관리용 딕셔너리
    Dictionary<CameraType, CinemachineVirtualCamera> _cameras;

    CameraType _currentCameraType; // 현재 활성화된 카메라 타입
    bool _isBlending = false;      // 블렌딩 상태 체크용

    // 바인드용 enum
    enum SelectButtons
    {
        TitleBtn
    }

    private void Awake()
    {
        // 버튼 바인드
        Bind<Button>(typeof(SelectButtons));
    }

    private void OnEnable()
    {
        // 마우스 이벤트에 등록
        Managers.Input.MouseAction -= PlayerSelectRay;
        Managers.Input.MouseAction += PlayerSelectRay;
    }

    private void Start()
    {
        // 초기화
        InitCams();
        ChangeVCam(CameraType.Center);

        _melee = GameObject.Find("MeleePlayer");
        _mage = GameObject.Find("MagePlayer");
        _meleeAnim = GameObject.Find("Millial").GetComponent<Animator>();
        _mageAnim = GameObject.Find("RadDoll").GetComponent<Animator>();
        _meleeCol = _melee.GetComponent<BoxCollider>();
        _mageCol = _mage.GetComponent<BoxCollider>();

        // Camera Activated Event에 리스너 추가
        _brain.m_CameraActivatedEvent.AddListener(OnCameraActivated);

        // 바인드한 버튼에 리스너 추가
        GetButton((int)SelectButtons.TitleBtn).onClick.AddListener(CloseSelectUI);
    }

    // 카메라 초기화 및 딕셔너리 저장
    void InitCams()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        _centerVCam = GameObject.Find("CenterVCam").GetComponent<CinemachineVirtualCamera>();
        _meleeVCam = GameObject.Find("MeleeVCam").GetComponent<CinemachineVirtualCamera>();
        _mageVCam = GameObject.Find("MageVCam").GetComponent<CinemachineVirtualCamera>();

        // 카메라 타입을 키값으로 실제 virtualCamera를 담아두기
        _cameras = new Dictionary<CameraType, CinemachineVirtualCamera>
        {
            { CameraType.Center, _centerVCam },
            { CameraType.Melee, _meleeVCam },
            { CameraType.Mage, _mageVCam }
        };
    }

    void OnCameraActivated(ICinemachineCamera fromCam, ICinemachineCamera toCam)
    {
        // 카메라 전환이 완료되었을 때 처리
        OnBlendFinish();
    }

    // 카메라 전환 블렌드가 끝났을 때 호출하는 메서드
    public void OnBlendFinish()
    {
        // 카메라 타입에 따라 애니메이션 실행
        switch (_currentCameraType)
        {
            case CameraType.Melee:
                if (_meleeAnim != null)
                {
                    _meleeAnim.SetTrigger("doSkill");
                    _mageCol.enabled = false;
                }
                break;

            case CameraType.Mage:
                if (_mageAnim != null)
                {
                    _mageAnim.SetTrigger("doSkill"); ;
                    _meleeCol.enabled = false;
                }
                break;

            case CameraType.Center:
                _meleeCol.enabled = true;
                _mageCol.enabled = true;
                GetButton((int)SelectButtons.TitleBtn).interactable = true;
                break;
        }
    }

    // 카메라 전환 메서드
    public void ChangeVCam(CameraType newCameraType)
    {
        // 딕셔너리 내부에 있는 카메라의 우선순위를
        // 매개변수로 받은 카메라의 타입에 따라 변경
        foreach (var camera in _cameras)
        {
            camera.Value.Priority = (camera.Key == newCameraType) ? 1 : 0;
        }

        // 현재 활성화된 카메라 타입 저장
        _currentCameraType = newCameraType;
    }

    // 레이캐스트를 통해 플레이어를 선택하는 메서드
    public void PlayerSelectRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _player))
            {
                if (hit.collider.gameObject == _melee)
                {
                    Managers.Game._playerType = Define.PlayerType.Melee;
                    ChangeVCam(CameraType.Melee);
                }
                else if (hit.collider.gameObject == _mage)
                {
                    Managers.Game._playerType = Define.PlayerType.Mage;
                    ChangeVCam(CameraType.Mage);
                }

                GetButton((int)SelectButtons.TitleBtn).interactable = false;
                Logger.Log($"캐릭터 선택 확인 {Managers.Game._playerType}");
            }
        }
    }

    // 확인창을 열어주는 메서드
    public void OpenConfirm()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;

        if (confirmUI == null)
        {
            descTxt = "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";

            // 데이터 전달
            confirmUIData.DescTxt = descTxt;
            ConfirmUIData.confirmAction += () => {
                Animator fadeAnim = GameObject.FindWithTag("SceneManager").GetComponent<Animator>();
                fadeAnim.SetTrigger("doFade");
            };
            ConfirmUIData.cancelAction += () =>
            {
                ChangeVCam(CameraType.Center);
            };

            Managers.UI.OpenUI<ConfirmUI>(confirmUIData);
        }
    }

    // 타이틀로 버튼을 눌렀을 때 타이틀이 나오게 하는 메서드
    public void CloseSelectUI()
    {
        TitleCanvasUI titleUI = Managers.UI.GetActiveUI<TitleCanvasUI>() as TitleCanvasUI;

        if (titleUI == null)
        {
            Managers.UI.OpenUI<TitleCanvasUI>(new BaseUIData());
        }
    }

    private void OnDisable()
    {
        // 마우스 이벤트 구독 해제
        Managers.Input.MouseAction -= PlayerSelectRay;
    }
}

