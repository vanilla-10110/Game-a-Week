using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class enemyController : MonoBehaviour
{
    //movement
    public NavMeshAgent navMeshAgent;
    public int speed = 3;
    public float viewDistance;

    Animator animator;

    Transform player;

    //combat
    public int health;
    public float meleeRange;



    private void Awake()
    {
        player = GameObject.Find("Platyer").transform;
    }


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (Vector3.Distance(transform.position, player.position) <= viewDistance && !currentState.IsName("Attack"))
        {
            transform.LookAt(player.position);


            currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (!currentState.IsName("Run"))
            {
                animator.Play("Run");
            }




            if (Vector3.Distance(transform.position, player.position) <= meleeRange)
            {
                navMeshAgent.destination = transform.position;
                MeleeAttack();
            }
            else
            {
                


                navMeshAgent.destination = player.position;

            }
            
        }
        else
        {
            currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (!currentState.IsName("Idle") && !currentState.IsName("Attack"))
            {
                animator.Play("Idle");
            }
        }



        if (health == 0)
        {
            Die();
        }
    }
    void MeleeAttack()
    {
        
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (!currentState.IsName("Attack"))
        {
            animator.Play("Attack");
        }
       
        

    }
    void Die()
    {

    }
 
}
//wait until player is in visible range then move toward player.////////////// if player is in combat range then attack player.