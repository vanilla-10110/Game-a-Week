//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReg : MonoBehaviour
{

    AttackHandler attackHandler;

    public int enemyHealth = 100;
    public int health = 100;

    private void Awake()
    {
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //compare tag better lol
        if ((collision.collider.CompareTag("sword") && attackHandler.isAttacking) || collision.collider.CompareTag("spell"))
        {
            enemyHealth -= 10;
            Debug.LogAssertion(enemyHealth);
            
            if (enemyHealth <= 0)
            {
                // RAGDOLL ANIMATION
            }
        }
        if (collision.collider.CompareTag("enemySword"))
        {
            
            
            Debug.Log("what the fuk is wrong with you, dont get hit");



        }
            
    }
}
