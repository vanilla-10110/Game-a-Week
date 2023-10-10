//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReg : MonoBehaviour
{

    AttackHandler attackHandler;
    //EnemyAttack enemyAttack;

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
            int damage = Mathf.RoundToInt(attackHandler.Stats[2]);
            if (attackHandler.Stats[3] > Random.Range(0, 11))
            {
                damage *= 2;//critical damage
                Debug.LogWarning("CRIT");
            }
             
            enemyHealth -= Mathf.RoundToInt(attackHandler.Stats[2]);
            Debug.LogAssertion(enemyHealth);
            
            if (enemyHealth <= 0)
            {
                // RAGDOLL ANIMATION
            }
        }
        if (collision.collider.CompareTag("enemyWeapon"))
        {
            
            
            Debug.Log("what the fuk is wrong with you, dont get hit");



        }
            
    }
}
