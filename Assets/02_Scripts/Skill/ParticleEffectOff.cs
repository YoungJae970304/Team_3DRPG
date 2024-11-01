using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectOff : MonoBehaviour
{
    ParticleSystem _particle;
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        if (!_particle.isPlaying)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }
}
