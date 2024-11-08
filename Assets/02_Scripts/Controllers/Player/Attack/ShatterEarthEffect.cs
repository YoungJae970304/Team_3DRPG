using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEarthEffect : MonoBehaviour
{
    [Header("콜라이더 Center")]
    [SerializeField] private Vector3 _centerStart = new Vector3(0, 1, 1);  // 시작 위치
    [SerializeField] private Vector3 _centerIncrement = new Vector3(0, 0, 1);  // 증가값

    [Header("콜라이더 Size")]
    [SerializeField] private Vector3 _sizeStart = new Vector3(2, 4, 2);  // 시작 크기
    [SerializeField] private Vector3 _sizeIncrement = new Vector3(0, 0, 2);  // 증가값

    [Header("진행 관련")]
    [SerializeField] private int _steps = 12;  // 총 단계 수
    [SerializeField] private float _stepInterval = 0.1f;  // 단계 간 간격

    private BoxCollider _boxCollider;
    private int _currentStep;
    private float _timer;

    // 파티클 시스템 수명 관련 변수
    ParticleSystem _particle;

    private void Awake()
    {
        _boxCollider = gameObject.GetOrAddComponent<BoxCollider>();
        _particle = GetComponentInParent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Managers.Game._player._damageAlbes.Clear();
        StartCoroutine(ParticleDestroy());
    }

    private void Start()
    {
        // 초기 상태 설정
        _boxCollider.center = _centerStart;
        _boxCollider.size = _sizeStart;
    }

    private void Update()
    {
        if (_currentStep >= _steps) return;

        _timer += Time.deltaTime;
        if (_timer >= _stepInterval)
        {
            _timer = 0f;
            _currentStep++;

            // 현재 단계에 맞는 값 계산
            _boxCollider.center = _centerStart + (_centerIncrement * _currentStep);
            _boxCollider.size = _sizeStart + (_sizeIncrement * _currentStep);
        }
    }

    IEnumerator ParticleDestroy()
    {
        if (_particle == null) yield break;

        yield return new WaitForSeconds(_particle.main.duration);

        Managers.Resource.Destroy(_particle.gameObject);
    }

    // 시작 위치로 리셋
    public void Reset()
    {
        _currentStep = 0;
        _timer = 0f;
        _boxCollider.center = _centerStart;
        _boxCollider.size = _sizeStart;
    }

    private void OnDisable()
    {
        Reset();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            if (other.TryGetComponent<IDamageAlbe>(out var damageAlbe))
            {
                // 데미지 적용
                if (!Managers.Game._player._damageAlbes.Contains(damageAlbe))
                {
                    //damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
                    damageAlbe.Damaged(Managers.Game._player._skillBase._damage);
                    Managers.Game._player._effectController.HitEffectsOn("MageStonehit", other.transform);
                    Managers.Sound.Play("Mage/mage_skill2_hit");
                    damageAlbe.StatusEffect?.SpawnEffect<BurnEffect>(3, Managers.Game._player._skillBase._damage/2);
                }
                // 콜라이더로 담을 때
                Managers.Game._player._damageAlbes.Add(damageAlbe);
            }
        }
    }
}
