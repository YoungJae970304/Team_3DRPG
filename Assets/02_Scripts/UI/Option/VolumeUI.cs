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
    public override void Init(Transform anchor)
    {
        base.Init(anchor);
        Bind<Slider>(typeof(Sliders));
    }
}
