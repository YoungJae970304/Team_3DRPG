using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBall : MonoBehaviour
{
    public float _ballRange = 10;
    public float _ballSpeed = 5;
    Transform _originPlayerPos;
    Vector3 _ballDir;

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;

        _ballDir = _originPlayerPos.forward;
    }

    void Update()
    {
        transform.position += _ballDir * _ballSpeed * Time.deltaTime;

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
                damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);

                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}
