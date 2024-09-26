using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerCam : MonoBehaviour
{
    // 타겟 (플레이어)으로 부터의 거리 
    [Header("카메라의 초기 위치값")]
    public Vector3 _delta = new Vector3(0f, 3f, -4f);

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

    // 줌모드 오프셋
    [HideInInspector]
    public Vector3 _zoomOffset = Vector3.zero;

    private void Start()
    {
        _player = gameObject.GetOrAddComponent<Player>();
    }

    void Update()
    {
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

    private void LateUpdate()
    {
        CamControl();
    }

    // 마우스 회전값에 따라 최상위 플레이어를 돌리면 카메라와 모델 둘다 돌아감

    // 마우스 이동에 따른 회전 메서드
    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = _player._cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        float y = camAngle.y + mouseDelta.x;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 40f);
        }
        else
        {
            x = Mathf.Clamp(x, 320f, 361f);
        }

        _player._cameraArm.rotation = Quaternion.Euler(x, y, camAngle.z);

        switch (_player._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                
                break;
            case Define.CameraMode.ZoomView:
                Vector3 playerDir = _player._cameraArm.forward;
                playerDir.y = 0;
                _player._playerModel.forward = playerDir;
                break;
        }
    }

    // 카메라 제어에 관련된 메서드 ( 위치, 보는 방향, 장애물 체크 등 )
    void CamControl()
    {
        RaycastHit hit;
        Vector3 camPos = _player._cameraArm.position + _player._cameraArm.rotation * _delta;
        Vector3 camDir = camPos - _player._cameraArm.position;

        // 플레이어에서 카메라 방향으로 레이를 발사 (플레이어 위치, 카메라 위치, out hit, 방향, 충돌 가능한 레이어 )
        if (Physics.Raycast(_player.transform.position, camDir, out hit, camDir.magnitude, _obstacle))
        {
            float dist = (hit.point - _player.transform.position).magnitude * 0.8f; // 거리를 0.8 곱해줘서 줄여줌
            _player._camera.transform.position = _player._cameraArm.position + _player._cameraArm.rotation * _delta.normalized * dist; // 카메라의 위치 변경
        }
        else
        {
            _player._camera.transform.position = camPos;
        }

        switch (_player._cameraMode)
        {
            case Define.CameraMode.QuarterView:
                _player._camera.transform.LookAt(_player._cameraArm.position);
                break;
            case Define.CameraMode.ZoomView:
                _player._camera.transform.position = camPos + _zoomOffset;
                break;
        }
    }

    // FOV조절 메서드
    void FOVControl()
    {
        float fovDelta = Input.GetAxis("Mouse ScrollWheel") * _fovChangeSpeed;
        float newFOV = Mathf.Clamp(_player._camera.fieldOfView - fovDelta, _minFOV, _maxFOV);
        _player._camera.fieldOfView = newFOV;
    }
}
