using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] Transform _largeMapCamPos;

    Camera _largeMapCam;

    protected override void Init()
    {
        base.Init();

        Managers.Game.PlayerPosSet(_playerSpawnPos);
        Managers.UI.OpenUI<MainUI>(new BaseUIData(), false);
        Managers.Game._player._hitMobs.Clear();
    }

    private void Start()
    {
        // LargeMap world size, LargeMap카메라 정의
        LargeMapUI largeMapUI = Managers.UI.IsClosedUI<LargeMapUI>() as LargeMapUI;
        if (largeMapUI == null) return;
        largeMapUI.InitSceneMapInfo(90f, _largeMapCamPos);
    }

    public override void Clear()
    {
        
    }
}
