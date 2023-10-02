using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class attackHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject weaponHolder;

    void Start()
    {
        //anim = weaponHolder.GetComponent<Animation>();
        //GetChild(0).
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    // Update is called once per frame
    void Update()
    {
        Attack(animator);
    }
    private void Attack(Animator anim)
    {
        if (Input.GetMouseButton(0) && !isPlaying(animator, "swordAttack1") && !isPlaying(animator, "swordAttack2") && !isPlaying(animator, "swordAttack3") && !isPlaying(animator, "swordAttack4"))
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("swordIdle1"))
            {
                animator.Play("swordAttack1");
            }
            else if (currentState.IsName("swordIdle2"))
            {
                animator.Play("swordAttack2");
            }
            else if (currentState.IsName("swordIdle3"))
            {
                animator.Play("swordAttack3");
            }
            else if (currentState.IsName("swordIdle4"))
            {
                animator.Play("swordAttack4");
            }
            
        }
    }
}
