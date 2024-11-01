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

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;
        _cameraForward = Camera.main.transform.forward;

        if (Managers.Game._player._playerCam._cameraMode == Define.CameraMode.QuarterView)
        {
            _ballDir = _originPlayerPos.forward;
        }
        else
        {
            // 클릭한 곳을 향해 날아가도록 구현
            _ballDir = _cameraForward;
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
                    Managers.Resource.Destroy(gameObject);
                }
                // 콜라이더로 담을 때
                Managers.Game._player._damageAlbes.Add(damageAlbe);
            }
        }
    }
}
