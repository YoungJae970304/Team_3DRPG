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

    private void Start()
    {
        // LargeMap world size, LargeMap카메라 정의
        LargeMapUI largeMapUI = Managers.UI.IsClosedUI<LargeMapUI>() as LargeMapUI;
        if (largeMapUI == null) return;
        largeMapUI.InitSceneMapInfo(90f, _largeMapCamPos);

        Vector3 loadPlayerPos = PlayerPosSet.PlayerPosSetLoad();
        if(!TitleCanvasUI._isNewGame)
        {
            var player = Managers.Game._player.GetComponent<Player>();
            player.transform.position = loadPlayerPos;
            Logger.Log($"저장된 위치로 이동{loadPlayerPos}");
        }
    }

    public override void Clear()
    {

    }
}
