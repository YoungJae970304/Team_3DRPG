using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : MonsterBaseState
{
    public SlimeIdleState(Monster monster) : base(monster) { }
    Vector3 _originPos;
    public override void OnStateEnter()
    {
        //현재 위치 저장
        _originPos = _monster.transform.position;
        OnStateUpdate();
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
    }

    public override void OnStateUpdate()
    {
        //일정 거리 배회
        //슬라임은 계속 배회
        //선공몹들은 플레이어가 일정 거리 안에 들어온다면 Exit로 상태 변환
        float awayRange = Random.Range(0, _monster._mStat.AwayRange + 1);     
        _monster._nav.Move(_originPos + new Vector3(awayRange, awayRange, awayRange));
    }
}
public class MoveState : MonsterBaseState
{
    public MoveState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //플레이어 찾기
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
    }

    public override void OnStateUpdate()
    {
        //플레이어 추격
    }
}

public class AttackState : MonsterBaseState
{
    public AttackState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
       //플레이어 공격
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
    }

    public override void OnStateUpdate()
    {
        //딜레이 후 플레이어 공격
    }
}

public class SkillState : MonsterBaseState
{
    public SkillState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //플레이어에게 스킬 사용
    }

    public override void OnStateExit()
    {
       //다른 상태로 변환
    }

    public override void OnStateUpdate()
    {
       //딜레이 후 플레이어에게 스킬 사용
    }
}

public class DamagedState : MonsterBaseState
{
    public DamagedState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //넉백, 데미지 받기
    }

    public override void OnStateExit()
    {
        //다른 상태로 변환
    }

    public override void OnStateUpdate()
    {
       //업데이트에 들어갈게 있나?
    }
}

public class DieState : MonsterBaseState
{
    public DieState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //죽는 모션
    }

    public override void OnStateExit()
    {
        //exit가 필요할까
    }

    public override void OnStateUpdate()
    {
        //아이템 떨구기
        //자기 파괴하기
    }
}

public class ReturnState : MonsterBaseState
{
    public ReturnState(Monster monster) : base(monster) { }

    public override void OnStateEnter()
    {
        //origin포스 찾아서 이동하기
    }

    public override void OnStateExit()
    {
        //originPos라면 Idle로 상태 변환
        //아니라면 플레이어 추격(Move로 상태 변환)
    }

    public override void OnStateUpdate()
    {
        //계속해서 이동하다가 hp회복
        //다시 맞았을 때 위치 판단해서 return거리보다 작아졌다면 플레이어 추격
    }
}