using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkControl : MonoBehaviour {

    Animator animator;
    public Transform RighHandTarget, LookObj;
    public bool IkActive = false;

	void Start () {
        animator = GetComponent<Animator>();
	}

    private void OnAnimatorIK()
    {
        if(IkActive)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(LookObj.position);

            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

            animator.SetIKPosition(AvatarIKGoal.RightHand, RighHandTarget.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, RighHandTarget.rotation);
        }
        
    }
}
