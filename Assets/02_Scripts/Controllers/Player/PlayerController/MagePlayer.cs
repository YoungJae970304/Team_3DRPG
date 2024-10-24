using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagePlayer : Player
{
    [Header("평타 투사체 생성 위치")]
    public Transform _mageBallPos;

    protected override void Awake()
    {
        base.Awake();

        Managers.Game._playerType = Define.PlayerType.Mage;
    }

    public override void Attack()
    {
        GameObject go = Managers.Resource.Instantiate("Player/MageBall");
        go.transform.forward = _playerModel.forward;
        go.transform.position = _mageBallPos.position;
    }

    public override void Special()
    {
        _playerCam.CamModeChange();
    }

    public override void SkillSetE()
    {
        _skillBase = new TestSkill();
    }

    public override void SkillSetR()
    {
        //_skillBase = new ChainLightning();

        // 임시로 모든 스킬 테스트 해보기 위해 구현 나중에는 E처럼 바꿀것
        int rand = Random.Range(1, 4);

        switch (3)
        {
            case 1:
                _skillBase = new TestSkill();
                break;
            case 2:
                _skillBase = new TestSkill2();
                break;
            case 3:
                _skillBase = new ChainLightning();
                break;
            default:
                break;
        }
    }
}
