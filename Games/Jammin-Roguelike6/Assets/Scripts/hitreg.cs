//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReg : MonoBehaviour
{
    enemyDeathCount chestManager;

    GameObject player;
    
    AttackHandler attackHandler;
    //EnemyAttack enemyAttack;

    private FMOD.Studio.EventInstance instanceDEATH;
    private FMOD.Studio.EventInstance instanceHIT;
    private FMOD.Studio.EventInstance instanceCRIT;


    public int enemyHealth = 100;
    public int health = 100;
    public GameObject ragdoll;

    private void Awake()
    {
        player = GameObject.Find("Platyer");
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
        chestManager = GameObject.Find("chestManager").GetComponent<enemyDeathCount>();


        instanceDEATH = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_DEATH");
        instanceHIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT");
        instanceCRIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT_CRIT");
    }
    private void OnCollisionEnter(Collision collision)
    {
        //compare tag better lol
        if ((collision.collider.CompareTag("sword") && attackHandler.isAttacking && gameObject.name == "skeleton") || (collision.collider.CompareTag("spell") && gameObject.name == "skeleton"))
        {
            int damage = Mathf.RoundToInt(attackHandler.Stats[2]);
            if (Random.value < attackHandler.Stats[3])
            {
                damage *= 2;//critical damage   BUT IT DONT FUCKEN WORK
                Debug.LogWarning("CRIT");
            }
            

            enemyHealth -= damage;
            
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instanceHIT, GetComponent<Transform>(), GetComponent<Rigidbody>());
            instanceHIT.start();

            DieQuestionMark();
        }
        if (collision.collider.CompareTag("enemyWeapon") && player.GetComponent<AttackHandler>().skeleAttacking && gameObject.name == "Platyer")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/PC/PC_HIT_SLASH");
            PlayerTakeDamage();
        }
            
    }

    public void DieQuestionMark()
    {
        
        if (enemyHealth <= 0)
        {
            chestManager.deadCount++;
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(instanceDEATH, ragdoll.GetComponent<Transform>(), ragdoll.GetComponent<Rigidbody>());
            instanceDEATH.start();
            Debug.Log(ragdoll);
            ragdoll.transform.position = gameObject.transform.position;
            ragdoll.transform.rotation = gameObject.transform.rotation;
            ragdoll.SetActive(true);
            gameObject.SetActive(false);

        }
    }
    void PlayerTakeDamage()
    {
        HealthBar health = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        health.TakeDamage(); 
    }
}
