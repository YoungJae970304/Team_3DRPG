using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : Player
{
    public override IEnumerator Attack()
    {
        Debug.Log("근거리 캐릭터 공격");
        switch (AtkCount)
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

        yield return new WaitForSeconds(0.5f);
        _attacking = false;
    }
}
