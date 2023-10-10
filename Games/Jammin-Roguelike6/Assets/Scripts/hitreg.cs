//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReg : MonoBehaviour
{

    AttackHandler attackHandler;

    public int enemyHealth = 100;
    public int health = 100;
    public GameObject ragdoll;
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
            Debug.Log(enemyHealth);
            
            if (enemyHealth <= 0)
            {
                
                
                
                Debug.Log(ragdoll);

                ragdoll.transform.position = gameObject.transform.position;
                ragdoll.transform.rotation = gameObject.transform.rotation;
                ragdoll.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        if (collision.collider.tag == "enemyWeapon")
            Debug.Log("what the fuk is wrong with you, dont get hit");



        }
            
    }
}
