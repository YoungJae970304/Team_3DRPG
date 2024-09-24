using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InterectController : MonoBehaviour
{
    Player _player = Managers.Game._player;
    [SerializeField] int _interectRange;
    [SerializeField] Camera _main;

    [SerializeField] List<IInterectable> _target = new List<IInterectable>();
    IInterectable _current;
    private float _currentDis = 100;
    int i = 0;
    private void Start()
    {
        _currentDis = 100;
    }

    private void LateUpdate()
    {
        InterectCheck();
    }
    [ContextMenu("상호작용 테스트")]
    public void InterectCheck()
    {
        /*
        IEnumerable<Transform> target = _target.Where(t => Util.ChackFOV(_player.transform, t, _angle, (int)_interectRange));
        if (target!=null) {
            foreach (Transform a in target)
            {
                Logger.Log(a.name);
            }
        }
        */
        _currentDis = _interectRange;
        for (i = 0; i < _target.Count; i++) {
            if (!CheckInCamera(_target[i].transform)) { continue; }
            
            float distance = Vector3.Distance(Managers.Game._player.transform.position, _target[i].transform.position);
            Logger.Log(distance.ToString());
            if (distance < _currentDis) {
                _currentDis = distance;
                if (_current != _target[i]) {
                    _current.UIPopUp(false);
                    _current = _target[i];
                    _current.UIPopUp(true);
                }
                
            }
        }
        
        if (_current != null) {
            Logger.Log(_current.name);
            _current = null;
        }
        
    }
    public void Interection() {
        if (_current == null) { return; }
            _current.Interection(Managers.Game._player.gameObject);
    }


    public bool CheckInCamera(Transform t) {

        Vector3 pos = _main.WorldToViewportPoint(t.position);
        Logger.Log(pos.z.ToString());
        if (0 <= pos.x && pos.x <= 1 && 0 <= pos.y && pos.y <= 1 && 0 <= pos.z&& _interectRange>= pos.z) {
           return true;
        }

        return false;

    }
}
