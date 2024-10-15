using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    public float _swordAuraDis = 10;
    public float _swordAuraSpd = 7.5f;
    Transform _originPlayerPos;
    Vector3 _swordAuraDir;

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;

        _swordAuraDir = _originPlayerPos.forward;
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
                damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
            }
        }
    }
}
