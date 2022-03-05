using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaireBehaviourScript : MonoBehaviour
{

    Animator claireAnimator;
    float axisH, axisV;
    AudioSource claireAudioSource;
    CapsuleCollider claireCapsule;

    [SerializeField]
    float walkSpeed = 2f, runSpeed = 8f, rotSpeed = 8f, jumpForce = 250f;
    Rigidbody rb;
    private bool CharacterJump = false;
    const float timeout = 40.0f;
    [SerializeField] float countdown = timeout;
    [SerializeField] AudioClip sndJump, sndImpact, sndLeftFoot, sndRightFoot;
    bool switchFoot;

    [SerializeField] bool isJumping=false;



    private void Awake()
    {
        claireAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        claireAudioSource = GetComponent<AudioSource>();
        claireCapsule = GetComponent<CapsuleCollider>();

    }



    private void Update()
    {
        axisH = Input.GetAxis("Horizontal");
        axisV = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterJump = true;
        }



        if (axisV > 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.Translate(Vector3.forward * runSpeed * axisV * Time.deltaTime);
                claireAnimator.SetFloat("Running", axisV);
            }
            else
                claireAnimator.SetFloat("Running", 0);
            claireAnimator.SetBool("Walk", true);
            transform.Translate(Vector3.forward * walkSpeed * axisV * Time.deltaTime);
        }

        else
        {
            claireAnimator.SetBool("Walk", false);
        }

        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * axisH);
        if (axisH != 0 && axisV == 0)
        {
            claireAnimator.SetFloat("h", axisH);
        }
        else
        {
            claireAnimator.SetFloat("h", 0);
        }
        if (axisV < 0)
        {
            claireAnimator.SetBool("WalkBackward", true);
            transform.Translate(Vector3.forward * walkSpeed * axisV * Time.deltaTime);
        }
        else
            claireAnimator.SetBool("WalkBackward", false);
        //IdleDanceTwerk
        if (axisH == 0 && axisV == 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                claireAnimator.SetBool("Twerk", true);
                transform.Find("Anaconda").GetComponent<AudioSource>().enabled = true;
            }
        }
        else
        {
            countdown = timeout;
            claireAnimator.SetBool("Twerk", false);
            transform.Find("Anaconda").GetComponent<AudioSource>().enabled = false;

        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            ClaireDead();
        }
        //ColliderHeight
        if(isJumping)

        claireCapsule.height = claireAnimator.GetFloat("colheight");


    }
    private void FixedUpdate()
    {
        if ((CharacterJump) && !isJumping)
        {
            isJumping = true;
            CharacterJump = false;
            rb.AddForce(Vector3.up * jumpForce);
            claireAnimator.SetTrigger("Jump");
            claireAudioSource.PlayOneShot(sndJump);
        }
       
        
    }

    public void ClaireDead()
    {
        claireAnimator.SetTrigger("Dead");
        transform.Find("Anaconda").GetComponent<AudioSource>().enabled = false;
        GetComponent<ClaireBehaviourScript>().enabled = false;
    }
    public void PlaySoundImpact()
    {
        claireAudioSource.PlayOneShot(sndImpact);
    }

    public void PlayFootStep()
    {
        if (!claireAudioSource.isPlaying)
        {
            switchFoot = !switchFoot;
                if(switchFoot)
            {
               
                claireAudioSource.PlayOneShot(sndLeftFoot);

            }
            else
            {
           
                claireAudioSource.PlayOneShot(sndRightFoot);
            }
        }
    }
    public void SwitchJumping()
    {
        isJumping = false;
    }
}
