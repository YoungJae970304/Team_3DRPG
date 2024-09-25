using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override void Attack()
    {
        switch (_curAtkCount)
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

        // 추후 애니메이션 이벤트로 변경 예정

        // 애니메이션 시작 시 _canAtkInput = false, _attacking = true;
        // CanAtkInputOffTimer는 애니메이션의 중반쯤 _canAtkInput = true;

        // AtkOffTimer는 애니메이션 종료 직전에 if-else문(_playerInput._atkInput.Count < 1)으로 
        // _attacking = false;하거나 _curAtkCount = _playerInput._atkInput.Dequeue();
        CanAtkInputOffTimer(0.5f);
        AtkOffTimer(1.0f);
    }


    // 추후 애니메이션 이벤트로 변경 예정
    float _curCAITime = 0;
    void CanAtkInputOffTimer(float targetTime)
    {
        _curCAITime += Time.deltaTime;

        if (_curCAITime >= targetTime)
        {
            _curCAITime = 0;
            _canAtkInput = true;
        }
    }

    // 추후 애니메이션 이벤트로 변경 예정
    float _curATime = 0;
    void AtkOffTimer(float targetTime)
    {
        _curATime += Time.deltaTime;

        if (_curATime >= targetTime)
        {
            _curATime = 0;

            // 선입력이 없다면 공격 중지
            if (_playerInput._atkInput.Count < 1)
            {
                _attacking = false;
            }
            else    // 선입력이 남아있다면 재공격 
            {
                _curAtkCount = _playerInput._atkInput.Dequeue();
            }
        }
    }
}
