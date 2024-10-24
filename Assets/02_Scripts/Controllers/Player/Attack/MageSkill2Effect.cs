using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkill2Effect : MonoBehaviour
{
    [Header("콜라이더 Center")]
    [SerializeField] private Vector3 centerStart = new Vector3(0, 1, 1);  // 시작 위치
    [SerializeField] private Vector3 centerIncrement = new Vector3(0, 0, 1);  // 증가값

    [Header("콜라이더 Size")]
    [SerializeField] private Vector3 sizeStart = new Vector3(2, 4, 2);  // 시작 크기
    [SerializeField] private Vector3 sizeIncrement = new Vector3(0, 0, 2);  // 증가값

    [Header("진행 관련")]
    [SerializeField] private int steps = 12;  // 총 단계 수
    [SerializeField] private float stepInterval = 0.1f;  // 단계 간 간격

    private BoxCollider boxCollider;
    private int currentStep;
    private float timer;

    // 파티클 시스템 수명 관련 변수
    ParticleSystem particle;

    private void Awake()
    {
        boxCollider = gameObject.GetOrAddComponent<BoxCollider>();
        particle = GetComponentInParent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Managers.Game._player._damageAlbes.Clear();
        StartCoroutine(ParticleDestroy());
    }

    private void Start()
    {
        // 초기 상태 설정
        boxCollider.center = centerStart;
        boxCollider.size = sizeStart;
    }

    private void Update()
    {
        if (currentStep >= steps) return;

        timer += Time.deltaTime;
        if (timer >= stepInterval)
        {
            timer = 0f;
            currentStep++;

            // 현재 단계에 맞는 값 계산
            boxCollider.center = centerStart + (centerIncrement * currentStep);
            boxCollider.size = sizeStart + (sizeIncrement * currentStep);
        }
    }

    IEnumerator ParticleDestroy()
    {
        if (particle == null) yield break;

        yield return new WaitForSeconds(particle.main.duration);

        Managers.Resource.Destroy(particle.gameObject);
    }

    // 시작 위치로 리셋
    public void Reset()
    {
        currentStep = 0;
        timer = 0f;
        boxCollider.center = centerStart;
        boxCollider.size = sizeStart;
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
                    damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
                }
                // 콜라이더로 담을 때
                Managers.Game._player._damageAlbes.Add(damageAlbe);
            }
        }
    }
}
