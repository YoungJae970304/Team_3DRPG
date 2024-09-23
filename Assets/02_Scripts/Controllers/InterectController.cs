using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InterectController : MonoBehaviour
{
    GameObject _player = Managers.Game._player;
    [SerializeField] int _angle;
    [SerializeField] int _radius;
    [SerializeField]float _interectRange =2;

    List<Transform> _target = new List<Transform>();
    Transform _current;
    float _currentDis = 100;
    int i = 0;
    private void LateUpdate()
    {
        for (i = 0; i < _target.Count; i++) {
            if (!Util.ChackFOV(_player.transform, _target[i], _angle, (int)_interectRange)) { continue; }
            float distance = Vector3.Distance(_player.transform.position, transform.position);
            if (distance < _currentDis) {
                _currentDis = distance;
                _current = _target[i];
            }
        }
            

    }
}
