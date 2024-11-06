using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUIData : BaseUIData
{
    public string DescTxt;
    public Action volumeAction;
}
public class VolumeUI : BaseUI
{
    enum Sliders
    {
        BackgroundSoundSlider,
        EffectSoundSlider,
    }
    private SoundManager _soundManager;
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        _soundManager = Managers.Sound;
        Bind<Slider>(typeof(Sliders));
        Get<Slider>((int)Sliders.BackgroundSoundSlider).value = _soundManager.GetBgmVolume();
        Get<Slider>((int)Sliders.EffectSoundSlider).value = _soundManager.GetEffectVolume();
        Get<Slider>((int)Sliders.BackgroundSoundSlider).onValueChanged.AddListener(OnBgmVolumeChange);
        Get<Slider>((int)Sliders.EffectSoundSlider).onValueChanged.AddListener(OnEffectVolumeChange);
    }
    // BGM 슬라이더 값이 변경될 때 호출되는 메서드
    private void OnBgmVolumeChange(float value)
    {
        _soundManager.SetBgmVolume(value); // BGM 볼륨 설정
    }

    // 효과음 슬라이더 값이 변경될 때 호출되는 메서드
    private void OnEffectVolumeChange(float value)
    {
        _soundManager.SetEffectVolume(value); // 효과음 볼륨 설정
    }
}
