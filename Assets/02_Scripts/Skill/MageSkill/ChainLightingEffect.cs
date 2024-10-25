using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(LineRenderer))]
public class ChainLightingEffect : MonoBehaviour
{
    LineRenderer _lineRenderer;
    [SerializeField] float speed = 2f;
    [SerializeField] float endspeed ;
    [SerializeField] float time = 0;

    Color color = Color.white;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }
    // Start is called before the first frame update
    public void Init(Vector3 startPos)
    {
        transform.position = startPos;
        _lineRenderer.enabled = false;

        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;

        StartCoroutine(DrawLine(Managers.Game._monsters));

        
    }
    public IEnumerator EffectPade()
    {
        time = 0;
        _lineRenderer.enabled = true;
        color = Color.white;
        while (true)
        {
            time += Time.deltaTime;
            _lineRenderer.SetColors(color, color);
            if (speed - time < endspeed)
            {
                color = Color.Lerp(Color.white, Color.black, 1 - (speed - time));
                if (color == Color.black) { break; }
            }


            yield return null;
        }

        _lineRenderer.enabled = false;
        Destroy(gameObject);
    }
    IEnumerator DrawLine(List<Monster> positions)
    {
        if (!_lineRenderer.enabled)
            _lineRenderer.enabled = true;

        // 중간에 몬스터가 죽게되면 실시간으로 for문의 반복 횟수에 영향을 받기때문에 스킬 사용시 저장용으로 리스트 생성
        List<Monster> monstersCopy = new List<Monster>(positions);

        // 처음에는 모든 포인트를 시작 위치로 설정
        _lineRenderer.positionCount = 1;
        Vector3 startPos = Managers.Game._player.transform.position + Managers.Game._player._cc.center;

        _lineRenderer.SetPosition(0, startPos);

        // 그 다음 순차적으로 위치 업데이트
        for (int i = 0; i < monstersCopy.Count; i++)
        {
            if (Vector3.Distance(Managers.Game._player.transform.position, monstersCopy[i].transform.position) < 10)
            {
                // 새로운 포인트를 추가할 때마다 positionCount 증가
                _lineRenderer.positionCount = i + 2;

                //Vector3 targetPos = monstersCopy[i].transform.position + (Vector3.up * monstersCopy[i]._characterController.height * 0.5f);
                Vector3 targetPos = monstersCopy[i].transform.position + monstersCopy[i]._characterController.center;
                _lineRenderer.SetPosition(i + 1, targetPos);

                if (monstersCopy[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        /*
        Monster monster=new Monster();
        // 최대 전이 수만큼 반복
        const int chainCount = 5; 
        Monster last = null;
        float lastDis = 0;
        float dis = 0;
        Monster[] _checked = new Monster[chainCount];
        for (int i = 0; i < chainCount; i++)
        {
            if (Vector3.Distance(Managers.Game._player.transform.position, positions[i].transform.position) < 10)//조건 사라짐
            {
                if (monster.TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    _checked[i ] = monster;
                    Vector3 targetPos = monster.transform.position + monster._characterController.center;
                    _lineRenderer.SetPosition(i + 1, targetPos);
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);

                    // monster와 제일 가까운 다른 몬스터를 positions에서 찾고 Monster에 할당
                    // 이미 전이 한 대상일 경우 전이 X
                    
                    foreach (var mon in positions)
                    {
                        if (_checked.Contains(mon)) { continue; }

                        dis = Vector3.Distance(mon.transform.position, monster.transform.position); ;
                        if (last == null) {
                            last = mon;
                            lastDis = dis;
                            continue;
                        }
                        else if(lastDis> dis) {
                            last = mon;
                            lastDis = dis;
                        }
                    }
                    if (lastDis > 10) { break; }
                    monster = last;
                    
                }
            }
        }*/
            //페이드 아웃부분
            time = 0;
        _lineRenderer.enabled = true;
        color = Color.white;
        while (true)
        {
            time += Time.deltaTime;
            _lineRenderer.SetColors(color, color);
            if (speed - time < endspeed)
            {
                color = Color.Lerp(Color.white, Color.black, 1 - (speed - time));
                if (color == Color.black) { break; }
            }


            yield return null;
        }

        _lineRenderer.enabled = false;
        Destroy(gameObject);
    }

    //public void DrawLine(List<Monster> positions)
    //{
    //    if (_lineRenderer.enabled == false)
    //        _lineRenderer.enabled = true;
    //    _lineRenderer.positionCount = positions.Count;
    //    _lineRenderer.loop = false;

    //    for (int i = 0; i < positions.Count; i++)
    //    {
    //        _lineRenderer.SetPosition(i, positions[i].transform.position + Vector3.up);
    //    }
    //}


}
