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
        Count,
    }

    public enum MageEffects
    {
        MageSkill1,
        Count,
    }

    public enum CommonEffects
    {
        SlashHit,
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (Managers.Game._playerType == Define.PlayerType.Melee)
        {
            Bind<ParticleSystem>(typeof(MeleeEffects));

            for (int i = 0; i < (int)MeleeEffects.Count; i++)
            {
                Get<ParticleSystem>(i).gameObject.SetActive(false);
            }
        }
        else if (Managers.Game._playerType == Define.PlayerType.Mage)
        {
            Bind<ParticleSystem>(typeof(MageEffects));

            for (int i = 0; i < (int)MageEffects.Count; i++)
            {
                Get<ParticleSystem>(i).gameObject.SetActive(false);
            }
        }


        //Bind<ParticleSystem>(typeof(CommonEffects));
    }

    public void MeleeEffectOn(MeleeEffects name)
    {
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        Get<ParticleSystem>((int)name).Play();
    }

    public void MageEffectOn(MageEffects name)
    {
        Get<ParticleSystem>((int)name).gameObject.SetActive(true);
        Get<ParticleSystem>((int)name).Play();
    }
}
