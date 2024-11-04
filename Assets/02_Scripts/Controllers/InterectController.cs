using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterectController : MonoBehaviour
{
    Player _player;                                                         //타겟 플레이어
    [SerializeField] float _interectRange;                                    //상호작용 범위
    [SerializeField] Camera _main;                                          //판정할 카메라
    [SerializeField] Interectable _current = null;                                           //현재 선택된 상호작용대상
    [SerializeField] List<Interectable> _target = new List<Interectable>(); //상호작용대상을 저장하고 관리할 리스트
    public Interectable _lastObj;                                           //이전에 선택된 상호작용대상                                    
    int i = 0;

    float _currentDis;

    PlayerCam _playerCam;
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        //변수 초기화
        _player = Managers.Game._player;
        _playerCam = _player._playerCam;
        _target = FindObjectsByType<Interectable>(FindObjectsSortMode.None).ToList();
        _main = Camera.main;
    }

    private void LateUpdate()
    {
        InterectCheck();
    }
    [ContextMenu("상호작용 테스트")]
    public void InterectCheck()
    {
        //카메라에 보이는 상태인지 확인하고
        //카메라에 보이는 경우 가장 가까운 오브젝트 저장
        
        _currentDis = _interectRange;

        for (i = 0; i < _target.Count; i++)
        {
            if (!CheckInCamera(_target[i].transform)) { continue; }

            float distance = Vector3.Distance(_player.transform.position, _target[i].transform.position);
            if (distance < _currentDis)
            {
                _currentDis = distance;
                _current = _target[i];
            }
        }
        //이전에 선택된 오브젝트가 아니면
        if (_lastObj != _current)
        {
            if (_lastObj != null)
            {//이전 오브젝트 상호작용 UI 비활성화
                _lastObj.UIPopUp(false);
            }
            _lastObj = _current;

            if (_lastObj != null)
            {//상호작용 UI 표시
                _lastObj.UIPopUp(true);
                
            }
        }
        _current = null;
    }
    //상호작용 함수
    public void Interection() {
        if (_lastObj == null) { return; }
            _lastObj.Interection(_player.gameObject);
    }

    //카메라 내부에 있고 일정거리에 있는지 확인하는 함수
    public bool CheckInCamera(Transform t) {

        Vector3 pos = _main.WorldToViewportPoint(t.position);
        if (0 <= pos.x && pos.x <= 1 && 0 <= pos.y && pos.y <= 1 && 0 <= pos.z&& _interectRange >= pos.z) {
           return true;
        }

        return false;

    }
}
