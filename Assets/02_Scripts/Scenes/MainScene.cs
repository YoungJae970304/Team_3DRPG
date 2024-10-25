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
    }

    private void OnEnable()
    {

    }

    private void Start()
    {
        // LargeMap world size, LargeMap카메라 정의
        LargeMapUI largeMapUI = Managers.UI.GetActiveUI<LargeMapUI>() as LargeMapUI;
        if (largeMapUI == null) return;
        largeMapUI._worldSize = 90f;

        Logger.LogError($"라지맵 이닛 확인");

        if (GameObject.FindWithTag("LargeMapCam").TryGetComponent<Camera>(out var cam))
        {
            _largeMapCam = cam;
            _largeMapCam.orthographicSize = largeMapUI._worldSize * 0.5f;
            Logger.LogError($"라지맵 카메라 초기화 성공");
        }
        else
        {
            Logger.LogError($"라지맵 카메라 초기화 실패");
        }
    }

    public override void Clear()
    {
        
    }
}
