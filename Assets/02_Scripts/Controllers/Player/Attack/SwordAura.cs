using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordAura : MonoBehaviour
{
    HashSet<IDamageAlbe> _damageAlbes = new HashSet<IDamageAlbe>();

    public float _swordAuraDis = 10;
    public float _swordAuraSpd = 7.5f;
    Transform _originPlayerPos;
    Vector3 _swordAuraDir;

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;

        _swordAuraDir = _originPlayerPos.forward;

        _damageAlbes.Clear();
    }

    void Update()
    {
        transform.position += _swordAuraDir * _swordAuraSpd * Time.deltaTime;

        float distance = Vector3.Distance(_originPlayerPos.position, transform.position);

        if (distance > _swordAuraDis)
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
                if (!_damageAlbes.Contains(damageAlbe))
                {
                    damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
                // 콜라이더로 담을 때
                _damageAlbes.Add(damageAlbe);
            }
        }
    }
}
