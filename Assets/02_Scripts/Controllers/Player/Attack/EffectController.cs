using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : BaseUI
{
    public enum MeleeEffects
    {
        MeleeCombo1,
        MeleeCombo2,
        MeleeCombo3,
        PowerAttack1,
        PowerAttack2,
        MeleeSkill1,
        MeleeSkill2,
        MeleeSkill3,
        Hit,
        PowerAttackHit,
        Count,
    }

    public enum MageEffects
    {
        NormalAttack,
        Skill1,
        Skill2,
        Skill3,
        Count,
    }

    public enum CommonEffects
    {
        Hit,
    }

    private void Awake()
    {
        Bind<ParticleSystem>(typeof(MeleeEffects));
        //Bind<ParticleSystem>(typeof(MageEffects));
        //Bind<ParticleSystem>(typeof(CommonEffects));

        //Get<ParticleSystem>((int)Effects.Combo1).Play();

        for (int i = 0; i < (int)MeleeEffects.Count; i++)
        {
            Get<ParticleSystem>(i).gameObject.SetActive(false);
        }
    }

    private void Start()
    {

    }

    public void MeleeEffectOn(MeleeEffects name)
    {
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        Get<ParticleSystem>((int)name).Play();
    }
}
