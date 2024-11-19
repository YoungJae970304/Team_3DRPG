using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitlePlayerAnimEvent : MonoBehaviour
{
    [SerializeField] string descTxt;

    ConfirmUIData confirmUIData = new ConfirmUIData();

    [SerializeField] GameObject meleePlayer;
    [SerializeField] GameObject magePlayer;

    public void MeleeEffect()
    {
        GameObject go = Managers.Resource.Instantiate("Player/MeleeSkill2T");
        go.transform.position = meleePlayer.transform.position;
    }

    public void MageEffect()
    {
        GameObject go = Managers.Resource.Instantiate("Player/ShatterEarthEffectT");
        go.transform.forward = magePlayer.transform.forward;
        go.transform.position = new Vector3(magePlayer.transform.position.x, 0 , magePlayer.transform.position.z);
    }

    public void PlayEffectSound(string soundPath)
    {
        string[] parts = soundPath.Split(',');
        if (parts.Length >= 2 && float.TryParse(parts[1], out float volume))
        {
            Managers.Sound.Play(parts[0], Define.Sound.Effect, volume);
        }
        else
        {
            Managers.Sound.Play(parts[0], Define.Sound.Effect);
        }
    }

    public void OpenConfirm()
    {
        SelectPlayerUI selectUI = Managers.UI.GetActiveUI<SelectPlayerUI>() as SelectPlayerUI;

        selectUI?.OpenConfirm();
    }
}
