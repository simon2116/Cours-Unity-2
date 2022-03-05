using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightControler : MonoBehaviour

{
    [SerializeField]
    Animator myAnimator;
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        myAnimator.SetFloat("h", h);
        myAnimator.SetFloat("v", v);

    }
}
