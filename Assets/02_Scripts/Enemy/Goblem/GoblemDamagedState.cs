using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblemDamagedState : MonsterBaseState
{
    public GoblemDamagedState(Goblem goblem) : base(goblem)
    {
        _goblem = goblem;
    }
    PlayerStat _pStat;
    public override void OnStateEnter()
    {
        //넉백, 데미지 받기
        //임시로 몬스터의 데미지를 넣어놓음 추후 플레이어 데미지 값 받아오게 설정
        _goblem._nav.enabled = false;
        _goblem.GetComponent<Rigidbody>().isKinematic = false;
    }

    public override void OnStateExit()
    {
        //코루틴 멈추기?
        //상태는 냅둿다가 나중에 슬라임 스위치 데미지파트랑 비교하기
        _goblem._nav.enabled = true;
        _goblem.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void OnStateUpdate()
    {
        _goblem.StartCoroutine(StartDamege(_goblem._gStat.Attack, _goblem.transform.position, 0.5f, 0.5f)); //오크 스텟은 플레이어 공격력으로 교체 예정
    }
    public IEnumerator StartDamege(int damage, Vector3 playerPosition, float delay, float pushBack)//넉백처리 중요!
    {
        yield return new WaitForSeconds(delay);

        try//이걸 실행해보고 문제가 없다면 실행
        {

            Vector3 diff = playerPosition - _goblem.transform.position;
            diff = diff / diff.sqrMagnitude;
            _goblem._nav.isStopped = true;
            _goblem.GetComponent<Rigidbody>().
            AddForce((_goblem.transform.position - new Vector3(diff.x, diff.y, 0f)) * 50f * pushBack);

        }
        catch (MissingReferenceException e)// 문제가 있다면 에러메세지 출력
        {
            Debug.Log(e.ToString());
        }
        //예외처리문
    }
}
