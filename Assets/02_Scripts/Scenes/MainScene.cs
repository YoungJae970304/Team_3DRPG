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
        Managers.UI.OpenUI<DragAndDrop>(new BaseUIData(), false);
        Managers.Game._player._hitMobs.Clear();

        if (!TitleCanvasUI._isNewGame)
        {
            Vector3 loadPlayerPos = PlayerPosSetData.PlayerPosSetLoad();
            var playerTransfrom = Managers.Game._player?.transform;
            if (playerTransfrom != null)
            {
                playerTransfrom.position = loadPlayerPos;
                //Logger.Log($"저장된 위치로 이동{loadPlayerPos}");
            }
            else
            {
                Logger.LogError("플레이어를 못찾았습니다.");
            }
        }
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
