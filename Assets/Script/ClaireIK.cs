using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaireIK : MonoBehaviour
{
    Animator animator;
public Transform RightHandTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnAnimatorIK()
    {
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

        animator.SetIKPosition(AvatarIKGoal.RightHand, RightHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, RightHandTarget.rotation);
    }
}
