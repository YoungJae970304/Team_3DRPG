using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override void Attack()
    {
        if (_playerInput._atkInput.Count > 0)
        {
            switch (_playerInput._atkInput.Dequeue())
            {
                case 0:
                    Logger.Log("강공격");
                    break;
                case 1:
                    Logger.Log("기본공격 1타");
                    break;
                case 2:
                    Logger.Log("기본공격 2타");
                    break;
                case 3:
                    Logger.Log("기본공격 3타");
                    break;
                default:
                    Logger.LogError("지정한 공격이 아님");
                    break;
            }

            CanAtkInputOffTimer(0.5f);
            AtkOffTimer(1.5f);
        }
    }

    float _curCAITime = 0;

    // 추후 애니메이션 이벤트로 이동 예정
    void CanAtkInputOffTimer(float targetTime)
    {
        _curCAITime += Time.deltaTime;

        if (_curCAITime >= targetTime)
        {
            _curCAITime = 0;
            _canAtkInput = true;
        }
    }

    float _curATime = 0;
    void AtkOffTimer(float targetTime)
    {
        _curATime += Time.deltaTime;

        if (_curATime >= targetTime)
        {
            _curATime = 0;
            _attacking = false;
        }
    }
}
