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
        yield return new WaitForSeconds(0.5f);
        if (_lineRenderer.enabled == false)
            _lineRenderer.enabled = true;

        // 플레이어도 포함해야 하니 +1 해줌
        _lineRenderer.positionCount = positions.Count + 1;
        _lineRenderer.loop = false;

        _lineRenderer.SetPosition(0, Managers.Game._player.transform.position + Managers.Game._player._cc.center);
        //prevPos = Managers.Game._player.transform.position + Managers.Game._player._cc.center;

        for (int i = 0; i < positions.Count; i++)
        {
            if (Vector3.Distance(Managers.Game._player.transform.position, Managers.Game._monsters[i].transform.position) < 10)
            {
                if (positions[i].TryGetComponent<IDamageAlbe>(out var damageable))
                {
                    _lineRenderer.SetPosition(i + 1, positions[i].transform.position + positions[i]._characterController.center);
                    damageable.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
            }


            yield return new WaitForSeconds(0.05f);
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
