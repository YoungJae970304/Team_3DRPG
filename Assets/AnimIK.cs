using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimIK : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        if (leftHand != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHand.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHand.rotation);
        }

        if (rightHand != null)
        {
            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

            anim.SetIKPosition(AvatarIKGoal.RightHand, rightHand.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, rightHand.rotation);
        }
    }
}
