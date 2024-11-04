using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimIK : MonoBehaviour
{
    Animator anim;
    [SerializeField] float weight = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        anim.SetLookAtWeight(weight);
        anim.SetLookAtPosition(Managers.Game._player.transform.position + Managers.Game._player._cc.center);
    }
}
