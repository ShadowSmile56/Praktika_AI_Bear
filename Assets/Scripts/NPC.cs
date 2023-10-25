using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update
    float timer;
    private Animator animator;
    float wakeUpRange = 5;
    Transform player;
    NavMeshAgent agent;
    private bool wakeup = false;
    private bool view= false;
    AudioSource audio;
    float RangeRun = 10;
    float RangeWalk = 15;
    public GameObject point;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("SampleScene");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (!wakeup)
        {
            if (distance < wakeUpRange)
            {
                
                animator.transform.LookAt(player);
                animator.Play("Bear_Buff");
                audio.Play();
                wakeup = true;
                view = true;
            }
        }
        else
        {
            if (view)
            {
                if (distance > RangeRun && distance < RangeWalk)
                {
                    animator.transform.LookAt(player);
                    animator.Play("Bear_WalkForward");
                    agent.speed = 2;
                    agent.SetDestination(player.transform.position);
                }
                else if (distance > wakeUpRange && distance < RangeRun)
                {
                    agent.speed = 3;
                    animator.transform.LookAt(player);
                    animator.Play("Bear_RunForward");
                    agent.SetDestination(player.transform.position);
                }
                else if (distance > RangeWalk)
                {
                    
                    agent.speed = 4;
                    animator.Play("Bear_RunForward");
                    if (agent.transform.position != point.transform.position)
                        agent.SetDestination(point.transform.position);
                    else
                    {
                        view = false;
                    }
                }
            }
            else
            {
               
                animator.Play("Bear_Eat");
                wakeup = false;
            }
        }

    }
}
