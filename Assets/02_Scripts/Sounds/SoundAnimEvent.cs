using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimEvent : MonoBehaviour
{

    #region 사운드 이벤트

    public void PlayEffectSound(string soundPath)
    {
        //Managers.Sound.Play(soundPath);
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

    public void PlayBGMSound(string soundPath)
    {
        Managers.Sound.Play(soundPath, Define.Sound.Bgm);
    }

    #endregion
}
