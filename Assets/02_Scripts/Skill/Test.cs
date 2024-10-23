using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class Test : MonoBehaviour
{
    [SerializeField] List<GameObject> target = new List<GameObject>();
    LineRenderer _lineRenderer;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer.startWidth = 1f;
        _lineRenderer.endWidth = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine(target);
    }
    public void DrawLine(List<GameObject> positions)
    {
        if (_lineRenderer.enabled == false)
            _lineRenderer.enabled = true;
        _lineRenderer.positionCount = positions.Count;
        _lineRenderer.loop = false;

        for (int i = 0; i < positions.Count; i++)
        {
            _lineRenderer.SetPosition(i, positions[i].transform.position);
        }
    }

}
