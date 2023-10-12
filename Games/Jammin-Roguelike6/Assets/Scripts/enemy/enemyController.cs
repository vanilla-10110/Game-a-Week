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

    GameObject player;

    //combat
    public float meleeRange;
    private FMOD.Studio.EventInstance instanceFEET;
    private FMOD.Studio.EventInstance instanceDEATH;
    private FMOD.Studio.EventInstance instanceHIT;
    private FMOD.Studio.EventInstance instanceCRIT;
    private FMOD.Studio.EventInstance instanceSWING;


    private void Awake()
    {
        player = GameObject.Find("Platyer");
    }


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
        
        animator = GetComponent<Animator>();
        instanceFEET = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_FOOTSTEP");
        instanceDEATH = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_DEATH");
        instanceHIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT");
        instanceCRIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT_CRIT");
        instanceSWING = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_SWING");
    }

    public void RUNBITCH()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(instanceFEET, GetComponent<Transform>(), GetComponent<Rigidbody>());
        instanceFEET.start();
    }


    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);

        if (Vector3.Distance(transform.position, player.transform.position) <= viewDistance && !currentState.IsName("Attack"))
        {

            transform.LookAt(player.transform.position);
            
            currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (!currentState.IsName("Run"))
            {
                animator.Play("Run");
            }




            if (Vector3.Distance(transform.position, player.transform.position) <= meleeRange)
            {
                navMeshAgent.destination = transform.position;
                MeleeAttack();
            }
            else
            {
                


                navMeshAgent.destination = player.transform.position;

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



       
    }
    
    void MeleeAttack()
    {
        
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        if (!currentState.IsName("Attack"))
        {
            player.GetComponent<AttackHandler>().skeleAttacking = true;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instanceSWING, GetComponent<Transform>(), GetComponent<Rigidbody>());
            instanceSWING.start();
            animator.Play("Attack");
        }
       
        

    }
   
}
//wait until player is in visible range then move toward player.////////////// if player is in combat range then attack player.