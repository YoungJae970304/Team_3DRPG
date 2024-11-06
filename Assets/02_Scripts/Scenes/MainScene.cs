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

        //Vector3 loadPlayerPos = PlayerPosSet.PlayerPosSetLoad();
        //if(!TitleCanvasUI._isNewGame)
        //{
        //    var player = Managers.Game._player?.GetComponent<Player>().gameObject.transform;
        //    if(player != null)
        //    {
        //        player.transform.position = loadPlayerPos;
        //        Logger.Log($"저장된 위치로 이동{loadPlayerPos}");
        //    }
        //    else
        //    {
        //        Logger.LogError("플레이어를 못찾았습니다.");
        //    }
        //}
    }

    public override void Clear()
    {

    }
}
