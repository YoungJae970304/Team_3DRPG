using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerCam : MonoBehaviour
{
    // Ÿ�� (�÷��̾�)���� ������ �Ÿ�
    [SerializeField]
    [Header("ī�޶��� �ʱ� ��ġ��")]
    Vector3 _delta = new Vector3(0f, 3f, -4f);

    // ��ֹ� ������ ���� ���̾�
    [SerializeField]
    [Header("��ֹ� ���̾�")]
    LayerMask _obstacle;

    // �߿� ����
    Player _player;

    // FOV���� ����
    [SerializeField]
    [Header("FOV �ּҰ�")]
    float _minFOV = 30f;
    [SerializeField]
    [Header("FOV �ִ밪")]
    float _maxFOV = 90f;
    [SerializeField]
    [Header("FOV ���� �ӵ�")]
    float _fovChangeSpeed = 5f;

    private void Start()
    {
        _player = gameObject.GetOrAddComponent<Player>();
    }

    void Update()
    {
        LookAround();
        FOVControl();
    }

    private void LateUpdate()
    {
        CamControl();
    }

    // ���콺 �̵��� ���� ȸ�� �޼���
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
    }

    // ī�޶� ��� ���õ� �޼��� ( ��ġ, ���� ����, ��ֹ� üũ �� )
    void CamControl()
    {
        RaycastHit hit;
        Vector3 camPos = _player._cameraArm.position + _player._cameraArm.rotation * _delta;
        Vector3 camDir = camPos - _player._cameraArm.position;

        // �÷��̾�� ī�޶� �������� ���̸� �߻� (�÷��̾� ��ġ, ī�޶� ��ġ, out hit, ����, �浹 ������ ���̾� )
        if (Physics.Raycast(_player.transform.position, camDir, out hit, camDir.magnitude, _obstacle))
        {
            float dist = (hit.point - _player.transform.position).magnitude * 0.8f; // �Ÿ��� 0.8 �����༭ �ٿ���
            _player._camera.transform.position = _player._cameraArm.position + _player._cameraArm.rotation * _delta.normalized * dist; // ī�޶��� ��ġ ����
        }
        else
        {
            _player._camera.transform.position = camPos;
        }

        _player._camera.transform.LookAt(_player._cameraArm.position);
    }

    // FOV���� �޼���
    void FOVControl()
    {
        float fovDelta = Input.GetAxis("Mouse ScrollWheel") * _fovChangeSpeed;
        float newFOV = Mathf.Clamp(_player._camera.fieldOfView - fovDelta, _minFOV, _maxFOV);
        _player._camera.fieldOfView = newFOV;
    }
}
