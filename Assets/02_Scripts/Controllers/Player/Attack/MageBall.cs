using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBall : MonoBehaviour
{
    public float _ballRange = 10;
    public float _ballSpeed = 5;
    Transform _originPlayerPos;
    Vector3 _cameraForward;
    Vector3 _ballDir;

    Camera _cam;

    public LayerMask _notPlayerLayer;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;
        _cameraForward = _cam.transform.forward;

        if (Managers.Game._player._playerCam._cameraMode == Define.CameraMode.QuarterView)
        {
            _ballDir = _originPlayerPos.forward;
        }
        else
        {
            // 줌모드일 경우
            // 지역변수 하나 더 추가 후 hit.point나 50끝점 값 담기
            RaycastHit hit;
            if (Physics.Raycast(_cam.transform.position, _cameraForward, out hit, 50f, _notPlayerLayer))
            {
                // 지역변수에 값 담기로 변경
                _ballDir = (hit.point - transform.position).normalized;
            }
            else
            {
                // 지역변수에 값 담기로 변경
                // ray 50
                _ballDir = _cameraForward;
            }
            //_ballDir = 지역변수;
        }

        Managers.Game._player._damageAlbes.Clear();
    }

    void Update()
    {
        transform.position += _ballDir * _ballSpeed * Time.deltaTime;
        transform.forward = _ballDir;

        float distance = Vector3.Distance(_originPlayerPos.position, transform.position);

        if (distance > _ballRange)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            if (other.TryGetComponent<IDamageAlbe>(out var damageAlbe))
            {
                // 데미지 적용
                if (!Managers.Game._player._damageAlbes.Contains(damageAlbe))
                {
                    damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
                    Managers.Game._player._effectController.HitEffectsOn("MageNormalhit", other.transform);
                    Managers.Sound.Play("Mage/mage_atk_hit");
                    Managers.Resource.Destroy(gameObject);
                }
                // 콜라이더로 담을 때
                Managers.Game._player._damageAlbes.Add(damageAlbe);
            }
        }
    }
}
