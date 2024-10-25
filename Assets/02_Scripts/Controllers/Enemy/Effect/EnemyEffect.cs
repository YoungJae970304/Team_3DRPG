using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : BaseUI //monobihavior로 변경
{
    public enum GoblemOrkEffects
    {
        LeftAttack,
        RightAttack,
        Roar,
        MonsterHit,
        Count,
    }
    private void OnEnable()
    {
        Bind<ParticleSystem>(typeof(GoblemOrkEffects));
        EffectOff();
    }

    public void EffectOff()
    {
        for (int i = 0; i < (int)GoblemOrkEffects.Count; i++)
        {
            Get<ParticleSystem>(i).gameObject.SetActive(false);
            Logger.LogError("이펙트 꺼짐");
        }
    }
    public void MonsterAttack(GoblemOrkEffects name, Transform playerTransform = null)
    {
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        Logger.LogError($"{Get<ParticleSystem>((int)name).gameObject.name}켜진 이펙트 이름임");
        if (playerTransform != null) 
        {
            Get<ParticleSystem>((int)name).gameObject.transform.position = playerTransform.position;
            Logger.LogError($"{Get<ParticleSystem>((int)name).gameObject.transform.position}바뀐위치임");
        }
        Get<ParticleSystem>((int)name).Play();
        Logger.LogError("이팩트 켜짐");
    }
}
