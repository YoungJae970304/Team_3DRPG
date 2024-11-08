using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : BaseScene
{
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] Transform _largeMapCamPos;

    Camera _largeMapCam;

    protected override void Init()
    {
        base.Init();

        Managers.Game.PlayerPosSet(_playerSpawnPos.position);
        Managers.UI.OpenUI<MainUI>(new BaseUIData(), false);
    }

    void Start()
    {
        // LargeMap world size, LargeMap카메라 정의
        LargeMapUI largeMapUI = Managers.UI.IsClosedUI<LargeMapUI>() as LargeMapUI;
        if (largeMapUI == null) return;
        largeMapUI.InitSceneMapInfo(208f, _largeMapCamPos);
    }

    public void OnStartBGMToDungeonType()
    {
        if (Managers.Game._selecDungeonLevel == DeongeonType.Boss)
        {
            Managers.Sound.Play("BGM/boss_bgm", Define.Sound.Bgm);
        }
        else
        {
            Managers.Sound.Play("BGM/dungeon_bgm", Define.Sound.Bgm);
        }
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
