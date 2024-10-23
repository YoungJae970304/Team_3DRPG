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
        Unknown = -1,
        Loading,
        Title,
        Main,
        Dungeon,
        Boss
    }

    public enum UIEvent
    {
        Click,
        BeginDrag,
        Drag,
        EndDrag,
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
        QuarterView,
        ZoomView
    }

    public enum PlayerType
    {
        Melee,  // 근접
        Mage  // 원거리
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

    public enum StatusEffectType { 
        Buff,
        DeBuff,
    }
    public enum StatusEffects
    {
        Slow,
    }
}
