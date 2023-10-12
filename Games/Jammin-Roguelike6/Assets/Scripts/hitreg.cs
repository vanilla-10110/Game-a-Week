//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HitReg : MonoBehaviour
{
    enemyDeathCount chestManager;

    GameObject player;
    
    AttackHandler attackHandler;
    //EnemyAttack enemyAttack;

    ContactPoint contact;

    private FMOD.Studio.EventInstance instanceDEATH;
    private FMOD.Studio.EventInstance instanceHIT;
    private FMOD.Studio.EventInstance instanceCRIT;



    public int enemyHealth = 100;
    public int health = 100;
    public GameObject ragdoll;
    public GameObject blod;
    private void Awake()
    {
        player = GameObject.Find("Platyer");
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
        chestManager = GameObject.Find("chestManager").GetComponent<enemyDeathCount>();


        instanceDEATH = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_DEATH");
        instanceHIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT");
        instanceCRIT = FMODUnity.RuntimeManager.CreateInstance("event:/SKELETON/SKELETON_HIT_CRIT");
    }

    private void Start()
    {
        enemyHealth = 100 + Mathf.RoundToInt(attackHandler.Stats[3]);
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

            contact = collision.contacts[0];
            //blodEffect.transform.position = transform.position;
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
        
        Vector3 pos = contact.point;
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        GameObject blodEffect = Instantiate(blod, pos, rot);
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
