using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMeshScript : MonoBehaviour
{
    NavMeshAgent MouseAgent;
    Animator MouseAnimator;
    Transform target;
    AudioSource MouseAudioSource;

    [SerializeField] float idleDistance = 20f, walkDistance = 15f, attackDistance = 2f;

    [SerializeField] Transform sitTarget, playerTarget;

    [SerializeField] AudioClip sndClaireHurt, sndPop, sndChimes,sndHit;

    [SerializeField] private GameObject particle;
    
    [SerializeField] private GameObject spirit;

    public float MouseDmg = 10f;
    public UIbarScript pbHealth;
    private bool Pop;
    void Start()
    {
        
        MouseAnimator = GetComponent<Animator>();
        MouseAgent = GetComponent<NavMeshAgent>();
        MouseAudioSource = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        Pop = MouseAnimator.GetBool("Pop");
        float playerdistance = Vector3.Distance(MouseAgent.transform.position, playerTarget.transform.position);
        
        if (Pop == false)
        {
            if (playerdistance > idleDistance)
            {
               
                target = sitTarget;
            }
            else
                target = playerTarget;

            if (target == playerTarget)
            {
                MouseAgent.SetDestination(target.position);
                
                if (MouseAgent.remainingDistance > idleDistance)
                {
                    MouseAgent.speed = 0;
                    MouseAnimator.SetBool("Idle", false);
                    MouseAnimator.SetBool("Walk", false);

                }
                else
                {
                    if (MouseAgent.remainingDistance > walkDistance)
                    {
                        MouseAgent.speed = 0;
                        MouseAnimator.SetBool("Idle", true);
                        MouseAnimator.SetBool("Walk", false);
                        Vector3 relativePos = target.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(relativePos);

                        Quaternion current = transform.localRotation;

                        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
                    }
                    else
                    {
                        if (MouseAgent.remainingDistance > attackDistance)
                        {
                            MouseAgent.speed = 2f;
                            MouseAnimator.SetBool("Walk", true);
                            MouseAnimator.SetBool("Attack", false);
                        }
                        else
                        {
                            MouseAgent.speed = 0f;
                            MouseAnimator.SetBool("Attack", true);
                        }
                    }
                }

            }
            else
            {
                if (MouseAgent.remainingDistance > 2f)
                {
                    MouseAgent.SetDestination(target.position);
                    MouseAgent.speed = 1f;
                    MouseAnimator.SetBool("Walk", true);
                    MouseAnimator.SetBool("Idle", false);
                    MouseAnimator.SetBool("Attack", false);
                }
                else
                {
                    MouseAgent.speed = 0;
                    MouseAnimator.SetBool("Walk", false);
                    MouseAnimator.SetBool("Attack", false);
                    MouseAnimator.SetBool("Idle", false);
                    MouseAgent.ResetPath();
                }
            }
            
        }
        else
            MouseAgent.GetComponent<NavMeshAgent>().enabled = false;

    }
    public void DamageToClaire()
    {
        
        pbHealth.Val -= MouseDmg;
        StartCoroutine(Hit());
        MouseAudioSource.PlayOneShot(sndClaireHurt);

        if(pbHealth.Val == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<ClaireController>().ClaireDead();

            GameObject[] mouse = GameObject.FindGameObjectsWithTag("Mouse");

            foreach (GameObject m in mouse)
            {
                m.GetComponent<EnemyNavMeshScript>().enabled = false;
                m.GetComponent<Animator>().SetBool("Attack", false);
                m.GetComponent<Animator>().SetBool("Walk", false);

            }
            GameObject[] wolfie = GameObject.FindGameObjectsWithTag("Wolfie");
            foreach (GameObject m in wolfie)
            {
                m.GetComponent<WolfieScript>().enabled = false;
                m.GetComponent<Animator>().SetBool("Attack", false);
                m.GetComponent<Animator>().SetBool("Run", false);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            particle.SetActive(true);
            MouseAudioSource.PlayOneShot(sndPop);
            MouseAgent.speed = 0;
            MouseAnimator.SetBool("Pop", true);
            MouseAnimator.SetBool("Walk", false);
            MouseAnimator.SetBool("Idle", false);
            MouseAnimator.SetBool("Attack", false);
            MouseAgent.GetComponent<Rigidbody>().isKinematic = true;
            MouseAgent.GetComponent<SphereCollider>().enabled = false;
            MouseAgent.GetComponent<BoxCollider>().enabled = false;
            MouseAgent.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(Pause());



        }
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(10f);
        MouseAudioSource.PlayOneShot(sndChimes);
        Instantiate(spirit, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
    IEnumerator Hit()
    {
       
        MouseAudioSource.PlayOneShot(sndHit);
        yield return new WaitForSeconds(0.8f);
        
    }

}
