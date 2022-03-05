using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLockScript : MonoBehaviour
{
    
    Animator animator;
    AudioSource audiosource;
    [SerializeField] AudioClip SndPop, SndUnlock, SndTing;
    [SerializeField] GameObject Smoke, Door, Kid;
    private bool havekey;
    
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        havekey = GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().HaveKey;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")&&havekey)
        {
            StartCoroutine(Pause());
            GetComponent<BoxCollider>().enabled = false;
        }
          IEnumerator Pause()
        {
            audiosource.PlayOneShot(SndUnlock);
            yield return new WaitForSeconds(1f);
            audiosource.PlayOneShot(SndTing);
            animator.SetBool("Fall", true);
            yield return new WaitForSeconds(1f);
            Smoke.SetActive(true);
            audiosource.PlayOneShot(SndPop);
            yield return new WaitForSeconds(0.2f);
            Door.GetComponent<DoorScript>().OpenDoor = true;
            GetComponentInChildren<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(2f);
            Kid.GetComponent<KidScript>().inCage = false;
            GameObject.Find("CanvasUIprogressBar").GetComponent<AddChildScript>().AddChild();
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().HaveKey=false;
            Destroy(gameObject);
           
        }
    }

}
