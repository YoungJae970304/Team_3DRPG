using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerCam : MonoBehaviour
{
    [Header("오브젝트 참조")]
    public Transform _cameraArm;
    [HideInInspector]
    public Define.CameraMode _cameraMode;
    [SerializeField]
    CinemachineVirtualCamera _cmQuarterCam;
    [SerializeField]
    CinemachineVirtualCamera _cmZoomCam;
    public CinemachineBrain _cmBrain;

    CinemachineVirtualCamera _curCam;

    // FOV관련 변수
    [SerializeField]
    [Header("FOV 최소값")]
    float _minFOV = 30f;
    [SerializeField]
    [Header("FOV 최대값")]
    float _maxFOV = 90f;
    [SerializeField]
    [Header("FOV 조절 속도")]
    float _fovChangeSpeed = 5f;
    float _originFov;

    // 수직 회전각 제한
    public float _minVRot = 40f;
    public float _maxVRot = 335f;

    private void Start()
    {
        VirtualCamInit();

        _cameraMode = Define.CameraMode.QuarterView;
        _curCam = _cmQuarterCam;
        _originFov = _curCam.m_Lens.FieldOfView;
    }

    void VirtualCamInit()
    {
        // 어떤 플레이어가 들어오냐에 따라 추적해야 할 대상이 다르기 때문에 시작시 Follow와 LookAt 초기화
        _cmQuarterCam.Follow = Managers.Game._player.transform.Find("CameraArm");
        _cmQuarterCam.LookAt = Managers.Game._player.transform.Find("PlayerModel");
        _cmZoomCam.Follow = Managers.Game._player.transform.Find("CameraArm");
        _cmZoomCam.LookAt = Managers.Game._player.transform.Find("PlayerModel");
    }

    void Update()
    {
        ChangeCMCam();  // 줌

        // 마우스 휠
        switch (_cameraMode)
        {
            case Define.CameraMode.QuarterView:
                FOVControl();
                break;
            case Define.CameraMode.ZoomView:
                break;
        }

        // 열려있는 UI가 있다면 화면 회전 금지
        if (Managers.UI.ExistsOpenUI()) return;

        LookAround();
    }

    // 각 카메라의 우선도 설정, 카메라 모드에 따라 전환되는 카메라 전환
    void ChangeCMCam()
    {
        _cmQuarterCam.Priority = _cameraMode == Define.CameraMode.QuarterView ? 10 : 0;
        _cmZoomCam.Priority = _cameraMode == Define.CameraMode.ZoomView ? 10 : 0;
    }

    // 마우스 회전값에 따라 최상위 플레이어를 돌리면 카메라와 모델 둘다 돌아감

    // 마우스 이동에 따른 회전 메서드
    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = _cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        float y = camAngle.y + mouseDelta.x;

        // 수직 회전 제한
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, _minVRot);
        }
        else
        {
            x = Mathf.Clamp(x, _maxVRot, 361f);
        }

        _cameraArm.rotation = Quaternion.Euler(x, y, camAngle.z);

        // ZoomView모드면 캐릭터가 카메라 정면을 보도록
        if (_cameraMode == Define.CameraMode.ZoomView)
        {
            Vector3 playerDir = _cameraArm.forward;
            playerDir.y = 0;
            Managers.Game._player._playerModel.forward = playerDir;
        }
    }

    // FOV조절 메서드
    void FOVControl()
    {
        float fovDelta = Input.GetAxis("Mouse ScrollWheel") * _fovChangeSpeed;
        float nowFOv = Mathf.Clamp(_curCam.m_Lens.FieldOfView - fovDelta, _minFOV, _maxFOV);
        _curCam.m_Lens.FieldOfView = nowFOv;
    }

    public void CamModeChange()
    {
        switch (_cameraMode)
        {
            case Define.CameraMode.QuarterView:
                EnableFL();
                // 카메라 모드 변경
                _cameraMode = Define.CameraMode.ZoomView;
                // 현재 카메라 초기화
                _curCam = _cmZoomCam;
                _curCam.m_Lens.FieldOfView = _originFov;
                _minVRot = 20f;
                _maxVRot = 345f;
                break;

            case Define.CameraMode.ZoomView:
                // 카메라 모드 변경
                _cameraMode = Define.CameraMode.QuarterView;
                // 현재 카메라 초기화
                _curCam = _cmQuarterCam;
                _curCam.m_Lens.FieldOfView = _originFov;
                _minVRot = 40f;
                _maxVRot = 335f;
                break;
        }
    }

    // zoom 카메라의 Follow와 LookAt을 끄는 함수
    public void DisableFL()
    {
        _cmZoomCam.m_Follow = null;
        _cmZoomCam.m_LookAt = null;
    }

    // zoom 카메라의 Follow와 LookAt을 키는 함수
    public void EnableFL()
    {
        _cmZoomCam.m_Follow = _cameraArm;
        _cmZoomCam.m_LookAt = Managers.Game._player._playerModel;
    }
}
