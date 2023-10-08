using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class AttackHandler : MonoBehaviour
{
    [Header("Objects")]
    Animator animator;
    public GameObject weaponHolder;


    [Header("Stats")]
    //public Dictionary<string, float> upgradeStats = new Dictionary<string, float>()
    //{
    //    {"moveSpeed", 0f},
    //    {"attackSpeed", 0f},
    //    {"attackDamage", 0f},
    //    {"criticalStrikeChance", 0f},
    //    {"fireDOT", 0f},
    //    {"shockDOT", 0f},
    //    {"acidDOT", 0f},
    //    {"voidDOT", 0f}
    //};


    public float[] Stats =
    {
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f,
        0f
    };

    string GetActiveWeaponName()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return child.name;
            }
        }
        return "None";
    }       
    Animator GetActiveWeaponAnimator()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                animator = child.GetComponent<Animator>();
                return animator;
            }
        }
        return null;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        
    }
   


    public void Attack()
    {

        if (GetActiveWeaponName() == "staff")
        {

            AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("staffIdle1"))
            {
                GetActiveWeaponAnimator().Play($"staffAttack{Random.Range(1, 4)}");
            }           
        }


        if (GetActiveWeaponName() == "sword")
        {
            int attackIndex = -1;
            AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("swordIdle1"))
            {
                attackIndex = 0;
            }
            else if (currentState.IsName("swordIdle2"))
            {
                attackIndex = 1;
            }
            else if (currentState.IsName("swordIdle3"))
            {
                attackIndex = 2;
            }
            else if (currentState.IsName("swordIdle4"))
            {
                attackIndex = 3;
            }
            if (attackIndex != -1)
            {
                GetActiveWeaponAnimator().Play($"swordAttack{attackIndex + 1}");
            }
        }
    }
}