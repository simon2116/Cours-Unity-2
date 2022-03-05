using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WolfieScript : MonoBehaviour
{
    NavMeshAgent WolfieAgent;
    Animator WolfieAnimator;
    Transform target;
    AudioSource WolfieAudioSource;

    [SerializeField] float idleDistance = 20f, walkDistance = 15f, attackDistance = 2f;

    [SerializeField] Transform playerTarget;

    [SerializeField] AudioClip sndClaireHurt, sndPop, sndChimes, sndHit;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject spirit;
    [SerializeField] private GameObject key;

    public float WolfieDmg = 20f;
    public UIbarScript pbHealth;
    private bool Pop;
    void Start()
    {

        WolfieAnimator = GetComponent<Animator>();
        WolfieAgent = GetComponent<NavMeshAgent>();
        WolfieAudioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        Pop = WolfieAnimator.GetBool("Pop");
        float playerdistance = Vector3.Distance(WolfieAgent.transform.position, playerTarget.transform.position);

        if (Pop == false)
        {
            if (playerdistance > idleDistance)
            {
                target = playerTarget;
            }
            else
                WolfieAgent.ResetPath();

            if (target == playerTarget)
            {
                WolfieAgent.SetDestination(target.position);

                if (WolfieAgent.remainingDistance > idleDistance)
                {
                    WolfieAgent.speed = 0;
                    
                    WolfieAnimator.SetBool("Run", false);
                    WolfieAnimator.SetBool("Attack", false);

                }
                else
                {
                    if (WolfieAgent.remainingDistance > walkDistance)
                    {
                        WolfieAgent.speed = 0;
                        
                        WolfieAnimator.SetBool("Run", false);
                        WolfieAnimator.SetBool("Attack", false);
                        Vector3 relativePos = target.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(relativePos);

                        Quaternion current = transform.localRotation;

                        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime);
                    }
                    else
                    {
                        if (WolfieAgent.remainingDistance > attackDistance)
                        {
                            WolfieAgent.speed = 4f;
                            WolfieAnimator.SetBool("Run", true);
                            WolfieAnimator.SetBool("Attack", false);
                        }
                        else
                        {
                            WolfieAgent.speed = 0f;
                            WolfieAnimator.SetBool("Attack", true);
                        }
                    }
                }

            }
           

        }
        else
            WolfieAgent.GetComponent<NavMeshAgent>().enabled = false;

    }
    public void DamageToClaire()
    {

        pbHealth.Val -= WolfieDmg;
        StartCoroutine(Hit());
        WolfieAudioSource.PlayOneShot(sndClaireHurt);

        if (pbHealth.Val == 0)
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
        if (other.gameObject.CompareTag("Player"))
        {
            particle.SetActive(true);
            WolfieAudioSource.PlayOneShot(sndPop);
            WolfieAgent.speed = 0;
            WolfieAnimator.SetBool("Pop", true);
            WolfieAnimator.SetBool("Run", false);
            
            WolfieAnimator.SetBool("Attack", false);
            WolfieAgent.GetComponent<Rigidbody>().isKinematic = true;
            WolfieAgent.GetComponent<SphereCollider>().enabled = false;
            WolfieAgent.GetComponent<BoxCollider>().enabled = false;
            WolfieAgent.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(Pause());



        }
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(10f);
        WolfieAudioSource.PlayOneShot(sndChimes);
        Instantiate(spirit, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Instantiate(key, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    IEnumerator Hit()
    {

        WolfieAudioSource.PlayOneShot(sndHit);
        yield return new WaitForSeconds(0.8f);

    }

}
