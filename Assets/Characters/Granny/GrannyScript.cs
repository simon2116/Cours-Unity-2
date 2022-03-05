using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrannyScript : MonoBehaviour
{
    NavMeshAgent GrannyAgent;
    Animator GrannyAnimator;
    Transform target;
    AudioSource GrannyAudioSource;

    [SerializeField] float idleDistance = 40f, walkDistance = 30f, attackDistance = 8f;

    [SerializeField] Transform playerTarget;

    [SerializeField] AudioClip sndClaireHurt, sndHit;


 
    private int hit = 0;
    private bool attack = false;
    public float GrannyDmg = 30f;
    public UIbarScript pbHealth;
    private bool Dead;
    void Start()
    {
        
        GrannyAnimator = GetComponent<Animator>();
        GrannyAgent = GetComponent<NavMeshAgent>();
        GrannyAudioSource = GetComponent<AudioSource>();
        







    }
    void Update()
    {
       
        float playerdistance = Vector3.Distance(GrannyAgent.transform.position, playerTarget.transform.position);

        if (Dead == false && attack==false)
        {


            if (playerdistance < idleDistance)
            {
                target = playerTarget;
            }
            else
                GrannyAgent.ResetPath();
            attack = false;

            if (target == playerTarget)
            {
                GrannyAgent.SetDestination(target.position);

                if (GrannyAgent.remainingDistance > idleDistance)
                {
                    GrannyAgent.speed = 0;

                    GrannyAnimator.SetBool("Run", false);

                }
                else
                {
                    if (GrannyAgent.remainingDistance > walkDistance)
                    {
                        GrannyAgent.speed = 0;

                        GrannyAnimator.SetBool("Run", false);
                        Vector3 relativePos = target.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(relativePos);

                        Quaternion current = transform.localRotation;

                        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
                    }
                    else
                    {
                        if (GrannyAgent.remainingDistance > attackDistance)
                        {
                            GrannyAgent.speed = 6f;
                            GrannyAnimator.SetBool("Run", true);
                            GrannyAnimator.SetBool("Attack", false);
                        }
                        else
                        {
                            GrannyAgent.speed = 0f;
                            GrannyAnimator.SetBool("Attack", true);
                           
                            
                            
                        }
                    }
                }

            }


        }
        else
            GrannyAgent.GetComponent<NavMeshAgent>().enabled = false;

    }

    public void DamageToClaire()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().KnockBack();
        pbHealth.Val -= GrannyDmg;
        StartCoroutine(Hit());
        GrannyAudioSource.PlayOneShot(sndClaireHurt);

        if (pbHealth.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();

            GameObject[] Granny = GameObject.FindGameObjectsWithTag("Granny");

            foreach (GameObject m in Granny)
            {
                m.GetComponent<EnemyNavMeshScript>().enabled = false;
                m.GetComponent<Animator>().SetBool("Attack", false);
                m.GetComponent<Animator>().SetBool("Run", false);

            }
            GameObject[] granny = GameObject.FindGameObjectsWithTag("Granny");

            foreach (GameObject m in granny)
            {
                m.GetComponent<GrannyScript>().enabled = false;
                m.GetComponent<Animator>().SetBool("Attack", false);
                m.GetComponent<Animator>().SetBool("Run", false);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {                                  
            Hurt();           
        }
    }
    private void Hurt()
    {
        if (hit < 3)
        {
            attack = true;
            GrannyAgent.GetComponent<SphereCollider>().enabled = false;
            hit++;
            GrannyAudioSource.PlayOneShot(sndHit);
            GrannyAnimator.SetBool("Run", false);
            GrannyAnimator.SetBool("Hurt", true);
            StartCoroutine(HitGranny());
        }
        else
        {
            isDead();
        }
    }
    private void isDead()
    {
    
        
        {
            
            GrannyAudioSource.PlayOneShot(sndHit);
            GrannyAgent.speed = 0;
            GrannyAnimator.SetBool("Dead", true);
            GrannyAnimator.SetBool("Run", false);

            GrannyAnimator.SetBool("Attack", false);
            GrannyAgent.GetComponent<Rigidbody>().isKinematic = true;
            GrannyAgent.GetComponent<SphereCollider>().enabled = false;
            GrannyAgent.GetComponent<BoxCollider>().enabled = false;
            GrannyAgent.GetComponent<NavMeshAgent>().enabled = false;
           



        }
    }
    IEnumerator HitGranny()
    {
        
        Vector3 Movement = transform.forward * Time.deltaTime * 2f;
        GrannyAgent.ResetPath();
        GrannyAudioSource.PlayOneShot(sndHit);
        yield return new WaitForSeconds(2f);
        GrannyAnimator.SetBool("Hurt", false);
        GrannyAnimator.SetBool("Run", true);
        GrannyAgent.speed = 6;
        GrannyAgent.Move(Movement);
        yield return new WaitForSeconds(2f);
        GrannyAnimator.SetBool("Run", false);
        attack = false;






    }
    IEnumerator Hit()
    {

        GrannyAudioSource.PlayOneShot(sndHit);
        yield return new WaitForSeconds(0.8f);

    }

}
