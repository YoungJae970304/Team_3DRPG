using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
[RequireComponent(typeof(LineRenderer))]
public class ChainLightingEffect : MonoBehaviour
{
    LineRenderer _lineRenderer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();

    }
    // Start is called before the first frame update
    public void Init(Vector3 startPos)
    {
        transform.position = startPos;
        _lineRenderer.enabled = false;

        _lineRenderer.startWidth = 1.5f;
        _lineRenderer.endWidth = 1.5f;

        StartCoroutine(DrawLine(Managers.Game._monsters));

        Destroy(gameObject, 1.5f);
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
            yield return new WaitForSeconds(0.1f);
        }
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
