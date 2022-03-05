using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KidScript : MonoBehaviour

{
    Animator animator;
    NavMeshAgent agent;
    AudioSource audioSource;
    public bool inCage=true;
    Transform player;
    [SerializeField] Transform target;

         
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent <AudioSource>();
        player=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (inCage)
        {
            agent.SetDestination(player.position);
            agent.isStopped = true;

        }
        else
        {
            animator.SetBool("Happy", true);
           
            if(agent.remainingDistance<=agent.stoppingDistance)
            {
                agent.isStopped = true;
                animator.SetBool("Run", false);
                agent.transform.rotation = target.rotation;
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("Run", true);
                agent.SetDestination(target.position);
                agent.speed = 5f;

            }

        }
    }
}
