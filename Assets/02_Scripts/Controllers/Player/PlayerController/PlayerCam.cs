using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerCam : MonoBehaviour
{
    // 타겟 (플레이어)으로 부터의 거리 
    [Header("카메라의 초기 위치값")]
    //public Vector3 _delta = new Vector3(0f, 3f, -4f);

    [SerializeField]
    CinemachineVirtualCamera _cmQuarterCam;
    [SerializeField]
    CinemachineVirtualCamera _cmZoomCam;
    CinemachineVirtualCamera _curCam;
    Cinemachine3rdPersonFollow _personFollow;

    // 장애물 감지를 위한 레이어
    [SerializeField]
    [Header("장애물 레이어")]
    LayerMask _obstacle;

    // 중요 변수
    Player _player;

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
        _player = gameObject.GetOrAddComponent<Player>();
        _personFollow = _cmQuarterCam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _curCam = _cmQuarterCam;
        _originFov = _curCam.m_Lens.FieldOfView;
    }

    void Update()
    {
        ChangeCMCam();
        LookAround();

        switch (_player._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                FOVControl();
                break;
            case Define.CameraMode.ZoomView:
                break;
        }
    }

    // 각 카메라의 우선도 설정, 카메라 모드에 따라 전환되는 카메라 전환
    void ChangeCMCam()
    {
        _cmQuarterCam.Priority = _player._cameraMode == Define.CameraMode.QuarterView ? 10 : 0;
        _cmZoomCam.Priority = _player._cameraMode == Define.CameraMode.ZoomView ? 10 : 0;
    }

    // 마우스 회전값에 따라 최상위 플레이어를 돌리면 카메라와 모델 둘다 돌아감

    // 마우스 이동에 따른 회전 메서드
    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = _player._cameraArm.rotation.eulerAngles;

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

        _player._cameraArm.rotation = Quaternion.Euler(x, y, camAngle.z);

        // ZoomView모드면 캐릭터가 카메라 정면을 보도록
        if (_player._cameraMode == Define.CameraMode.ZoomView)
        {
            Vector3 playerDir = _player._cameraArm.forward;
            playerDir.y = 0;
            _player._playerModel.forward = playerDir;
        }
    }

    // FOV조절 메서드
    void FOVControl()
    {
        float fovDelta = Input.GetAxis("Mouse ScrollWheel") * _fovChangeSpeed;
        //float newFOV = Mathf.Clamp(_player._camera.fieldOfView - fovDelta, _minFOV, _maxFOV);
        //_player._camera.fieldOfView = newFOV;
        float nowFOv = Mathf.Clamp(_curCam.m_Lens.FieldOfView - fovDelta, _minFOV, _maxFOV);
        _curCam.m_Lens.FieldOfView = nowFOv;
    }

    public void CamModeChange()
    {
        switch (_player._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                // 카메라 모드 변경
                _player._cameraMode = Define.CameraMode.ZoomView;
                // 현재 카메라 초기화
                _curCam = _cmZoomCam;
                _curCam.m_Lens.FieldOfView = _originFov;
                _minVRot = 20f;
                _maxVRot = 345f;
                break;

            case Define.CameraMode.ZoomView:
                // 카메라 모드 변경
                _player._cameraMode = Define.CameraMode.QuarterView;
                // 현재 카메라 초기화
                _curCam = _cmQuarterCam;
                _curCam.m_Lens.FieldOfView = _originFov;
                _minVRot = 40f;
                _maxVRot = 335f;
                break;
        }
    }
}
