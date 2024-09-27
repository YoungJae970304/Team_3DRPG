using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Slime slime) : base(slime) 
    {
        _slime = slime;
    }
    SlimeStat _sStat;
    float awayRangeX;
    //float awayRangeY = Random.Range(0, _sStat.AwayRange);
    float awayRangeZ;
    public override void OnStateEnter()
    {
        //현재 위치 저장
        //근데 이것도 슬라임에서 했잖아
        _sStat = _slime.GetComponent<SlimeStat>();
        if (_sStat == null)
        {
            Debug.LogError("SlimeStat 컴포넌트를 찾을 수 없습니다.");
        }
        awayRangeX = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        _slime._nav.destination = _slime._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
    }

    public override void OnStateExit()
    {
        //얘를 어떻게 해야할까
    }

    public override void OnStateUpdate()
    {
        if (_sStat == null) return;
        //일정 거리 배회
        //슬라임은 계속 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        awayRangeX = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);
        //float awayRangeY = Random.Range(0, _sStat.AwayRange);
        awayRangeZ = Random.Range(-_sStat.AwayRange, _sStat.AwayRange);

            if((_slime._nav.destination - _slime.transform.position).magnitude > 1f)
            {
                _slime._nav.SetDestination(_slime._nav.destination);
            }
            else if((_slime._nav.destination - _slime.transform.position).magnitude <= 1f)
            {
                _slime._nav.destination = _slime._originPos + new Vector3(awayRangeX, 0, awayRangeZ);
            }

    }
}