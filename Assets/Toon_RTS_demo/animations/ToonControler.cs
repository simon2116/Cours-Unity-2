using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToonControler : MonoBehaviour
{
    Animator myAnimator;
    float v;
    void Start ()
    {
        myAnimator = GetComponent<Animator>();

    }
    void Update()
    {
        
        v = Input.GetAxis("Vertical");
        myAnimator.SetFloat("v", v);
        if (v > 0)
            transform.Translate(Vector3.forward * 2f * v * Time.deltaTime);
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("Attack");
        }
        
    }
}
