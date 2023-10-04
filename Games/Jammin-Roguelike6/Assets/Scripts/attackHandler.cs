using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class attackHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public GameObject weaponHolder;
    

    void Start()
    {
        //animator = weaponHolder.GetComponent<Animation>();
        //GetChild(0).
    }

    string GetActiveWeaponName()
    {
        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                //Debug.Log(child.name);
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
               //Debug.Log(child.name);
                animator = child.GetComponent<Animator>();
                return animator;
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        
    }
    //public void ShootSpell()
    //{
    //    GameObject spawnedSpell = Instantiate(spells[spellIndex]);
    //    spawnedSpell.transform.position = spellSpawnPoint.position;
    //    spawnedSpell.GetComponent<Rigidbody>().velocity = spellSpawnPoint.forward * spellVelocity;
    //    Destroy(spawnedSpell, 5);
    //}

    private void Attack()
    {


        if (GetActiveWeaponName() == "staff")
        {
            
            AnimatorStateInfo currentState = GetActiveWeaponAnimator().GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("staffIdle1"))
            {
                
                GetActiveWeaponAnimator().Play("staffAttack");
                //Rigidbody instantiatedProjectile = Instantiate(spells[spellIndex], spellSpawnPoint.position, spellSpawnPoint.rotation)
                //    as Rigidbody;
                //instantiatedProjectile.velocity = transform.InverseTransformDirection(spellSpawnPoint.forward * spellVelocity);
                //
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
