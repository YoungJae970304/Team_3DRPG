using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : BaseUI
{
    public enum GoblemOrkEffects
    {
        LeftAttack,
        RightAttack,
        Count,
    }
    public enum HitEffect
    {
       MonsterHit,
    }
    private void Awake()
    {
        Bind<ParticleSystem>(typeof(GoblemOrkEffects));
        for(int i = 0; i < (int)GoblemOrkEffects.Count; i++)
        {
            Get<ParticleSystem>(i).gameObject.SetActive(false);
        }
    }
    public void MonsterAttack(GoblemOrkEffects name)
    {
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        //Get<ParticleSystem>((int)name).gameObject.transform.position = GetComponentInParent<Transform>().position;
        Get<ParticleSystem>((int)name).Play();
    }
}
