using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        StartCoroutine(ParticleDestroy());
    }

    IEnumerator ParticleDestroy()
    {
        if (_particle == null) yield break;

        yield return new WaitForSeconds(_particle.main.duration);

        Managers.Resource.Destroy(_particle.gameObject);
    }
}
