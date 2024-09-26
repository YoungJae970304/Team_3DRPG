using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount    // 아무것도 아니지만 Sound의 갯수를 세기 위해 추가
    }

    public enum Scene
    {
        Unknown,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    }

    public enum CameraMode
    {
        QuarterView
    }

    public enum SkillType { 
        KeyDown,
        Normal,
    }

    public enum QuestType
    {
        Main,
        Sub,
    }
}
