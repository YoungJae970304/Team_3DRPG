using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedPlayer : Player
{
    protected override void Start()
    {
        base.Start();
        _playerType = Define.PlayerType.Ranged;
    }

    public override void Skill()
    {
        SkillOffTimer(1f);
    }

    public override void Special()
    {
        switch (_cameraMode)
        {
            case Define.CameraMode.QuarterView:

                _cameraMode = Define.CameraMode.ZoomView;

                _playerCam._delta = new Vector3(0f, 1.5f, -2f);
                _playerCam._zoomOffset += new Vector3(1f, 0);
                break;

            case Define.CameraMode.ZoomView:

                _cameraMode = Define.CameraMode.QuarterView;

                _playerCam._delta = new Vector3(0f, 3f, -4f);
                _playerCam._zoomOffset = Vector3.zero;
                break;
        }
    }
}
