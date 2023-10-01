using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class attackHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    //private Animation anim;
    public GameObject weaponHolder;
    void Start()
    {
        //anim = weaponHolder.GetComponent<Animation>();
        //GetChild(0).
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("uhhhh what???");
            animator.Play("swordAttack");
        }
    }
}
