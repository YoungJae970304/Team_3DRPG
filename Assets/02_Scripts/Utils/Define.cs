using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount    // �ƹ��͵� �ƴ����� Sound�� ������ ���� ���� �߰�
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
}