using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBall : MonoBehaviour
{
    public float _ballRange = 10;
    public float _ballSpeed = 5;
    public float _rayDistance;

    Transform _originPlayerPos;
    Vector3 _cameraForward;
    Vector3 _ballDir;
    Vector3 _mageBallPos;

    Camera _cam;

    public LayerMask _notPlayerLayer;

    private void Awake()
    {
        _rayDistance = 50f;
    }

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;
        _cam = Camera.main;
        _cameraForward = _cam.transform.forward;
        MagePlayer mage = (MagePlayer)Managers.Game._player;
        _mageBallPos = mage._mageBallPos.position;

        Logger.LogError($"인에이블 확인");

        if (Managers.Game._player._playerCam._cameraMode == Define.CameraMode.QuarterView)
        {
            _ballDir = _originPlayerPos.forward;
        }
        else
        {
            // 줌모드일 경우
            RaycastHit hit;
            Debug.DrawRay(_cam.transform.position, _cameraForward * _rayDistance, Color.red, 1f);

            if (Physics.Raycast(_cam.transform.position, _cameraForward, out hit, _rayDistance, _notPlayerLayer))
            {
                _ballDir = (hit.point - _mageBallPos).normalized;
            }
            else
            {
                Vector3 rayEndPos = _cam.transform.position + _cameraForward * _rayDistance;
                _ballDir = (rayEndPos - _mageBallPos).normalized;
            }
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
