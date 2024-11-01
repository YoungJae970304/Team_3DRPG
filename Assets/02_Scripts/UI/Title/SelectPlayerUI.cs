using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngineInternal;

public enum CameraType
{
    Center,
    Melee,
    Mage
}

public class SelectPlayerUI : BaseUI
{   [Multiline(5)]
    [SerializeField] string descTxt;

    ConfirmUIData confirmUIData = new ConfirmUIData();

    GameObject _melee;
    GameObject _mage;
    Animator _meleeAnim;
    Animator _mageAnim;
    BoxCollider _meleeCol;
    BoxCollider _mageCol;
    [SerializeField] LayerMask _player;

    CinemachineVirtualCamera _centerVCam;
    CinemachineVirtualCamera _meleeVCam;
    CinemachineVirtualCamera _mageVCam;

    CinemachineBrain _brain;

    Dictionary<CameraType, CinemachineVirtualCamera> _cameras;

    private CameraType _currentCameraType; // 현재 활성화된 카메라 타입
    private bool _isBlending = false;      // 블렌딩 상태 체크용

    enum SelectButtons
    {
        TitleBtn
    }

    private void Awake()
    {
        Bind<Button>(typeof(SelectButtons));
    }

    private void Start()
    {
        InitCams();
        ChangeVCam(CameraType.Center);

        _melee = GameObject.Find("MeleePlayer");
        _mage = GameObject.Find("MagePlayer");
        _meleeAnim = GameObject.Find("Millial").GetComponent<Animator>();
        _mageAnim = GameObject.Find("RadDoll").GetComponent<Animator>();
        _meleeCol = _melee.GetComponent<BoxCollider>();
        _mageCol = _mage.GetComponent<BoxCollider>();

        Managers.Input.MouseAction -= PlayerSelectRay;
        Managers.Input.MouseAction += PlayerSelectRay;

        // Camera Activated Event에 리스너 추가
        _brain.m_CameraActivatedEvent.AddListener(OnCameraActivated);
    }

    void InitCams()
    {
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        _centerVCam = GameObject.Find("CenterVCam").GetComponent<CinemachineVirtualCamera>();
        _meleeVCam = GameObject.Find("MeleeVCam").GetComponent<CinemachineVirtualCamera>();
        _mageVCam = GameObject.Find("MageVCam").GetComponent<CinemachineVirtualCamera>();

        _cameras = new Dictionary<CameraType, CinemachineVirtualCamera>
        {
            { CameraType.Center, _centerVCam },
            { CameraType.Melee, _meleeVCam },
            { CameraType.Mage, _mageVCam }
        };
    }

    public void ChangeVCam(CameraType newCameraType)
    {
        foreach (var camera in _cameras)
        {
            camera.Value.Priority = (camera.Key == newCameraType) ? 1 : 0;
        }

        // 현재 활성화된 카메라 타입 저장
        _currentCameraType = newCameraType;
    }

    void OnCameraActivated(ICinemachineCamera fromCam, ICinemachineCamera toCam)
    {
        // 카메라 전환이 완료되었을 때 처리
        OnBlendFinish();
    }

    public void OnBlendFinish()
    {
        // 카메라 타입에 따라 애니메이션 실행
        switch (_currentCameraType)
        {
            case CameraType.Melee:
                if (_meleeAnim != null)
                {
                    _meleeAnim.SetTrigger("doSkill");  // 원하는 애니메이션 이름으로 변경
                    _mageCol.enabled = false;
                }
                break;

            case CameraType.Mage:
                if (_mageAnim != null)
                {
                    _mageAnim.SetTrigger("doSkill"); ;  // 원하는 애니메이션 이름으로 변경
                    _meleeCol.enabled = false;
                }
                break;

            case CameraType.Center:
                _meleeCol.enabled = true;
                _mageCol.enabled = true;
                break;
        }
    }

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

                Logger.LogWarning($"캐릭터 선택 확인 {Managers.Game._playerType}");
            }
        }
    }

    public void OpenConfirm()
    {
        ConfirmUI confirmUI = Managers.UI.GetActiveUI<ConfirmUI>() as ConfirmUI;

        if (confirmUI == null)
        {
            descTxt = "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
            confirmUIData.DescTxt = descTxt;  // "게임 진입 후 캐릭터의 변경이 불가능 합니다!\r\n선택한 캐릭터로 진행 하시겠습니까?";
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

    private void OnDisable()
    {
        Managers.Input.MouseAction -= PlayerSelectRay;
    }
}

