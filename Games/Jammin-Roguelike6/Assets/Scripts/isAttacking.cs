using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isAttacking : MonoBehaviour
{
    AttackHandler attackHandler;
    GameObject player;
    private void Awake()
    {
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
        player = GameObject.Find("Platyer");
    }

    public void Zounds()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/PC/SWORD_SWING");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.name == "1 handed sword")
        {
            if (collision.collider.CompareTag("Player"))
            {
                SkeleAttack();
            }
        }
    }

    public void SkeleAttack()
    {
        player.GetComponent<AttackHandler>().skeleAttacking = false;
    }

    public void ResetAttack()
    {
        attackHandler.isAttacking = false;
    }
}

