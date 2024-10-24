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

        // 처음에는 모든 포인트를 시작 위치로 설정
        _lineRenderer.positionCount = positions.Count + 1;
        Vector3 startPos = Managers.Game._player.transform.position + Managers.Game._player._cc.center;

        // 모든 포인트를 시작 위치로 초기화
        for (int i = 0; i <= positions.Count; i++)
        {
            _lineRenderer.SetPosition(i, startPos);
        }
        // 그 다음 순차적으로 위치 업데이트
        for (int i = 0; i < positions.Count; i++)
        {
            if (Vector3.Distance(Managers.Game._player.transform.position, positions[i].transform.position) < 10)
            {
                if (positions[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    Vector3 targetPos = positions[i].transform.position + positions[i]._characterController.center;
                    _lineRenderer.SetPosition(i + 1, targetPos);
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);


                }
            }
            yield return new WaitForEndOfFrame();
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
