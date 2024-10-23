using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkill2Effect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    // 리스트를 멤버 변수로 선언하여 매번 새로 생성하지 않도록 함
    private List<ParticleSystem.Particle> _enterParticles = new List<ParticleSystem.Particle>();

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    Transform _originPlayerPos;

    private void OnEnable()
    {
        _originPlayerPos = Managers.Game._player._playerModel.transform;

        Managers.Game._player._damageAlbes.Clear();
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

    private void OnParticleTrigger()
    {
        List<ParticleSystem.Particle> enterParticles = new List<ParticleSystem.Particle>();
        int numEnter = GetComponent<ParticleSystem>().GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enterParticles);

        for (int i = 0; i < numEnter; i++)
        {
            // 파티클 주변의 콜라이더 검출
            Collider[] hits = Physics.OverlapSphere(enterParticles[i].position, 0.1f, LayerMask.NameToLayer("Monster"));

            foreach (Collider other in hits)
            {
                if (other.CompareTag("Monster"))
                {
                    if (other.TryGetComponent<IDamageAlbe>(out var damageAlbe))
                    {
                        // 데미지 적용
                        if (!Managers.Game._player._damageAlbes.Contains(damageAlbe))
                        {
                            damageAlbe.Damaged(Managers.Game._player._playerStatManager.ATK);
                            Logger.LogWarning("데미지 적용");
                        }
                        // 콜라이더로 담을 때
                        Managers.Game._player._damageAlbes.Add(damageAlbe);
                    }
                }
            }
        }
    }
}
