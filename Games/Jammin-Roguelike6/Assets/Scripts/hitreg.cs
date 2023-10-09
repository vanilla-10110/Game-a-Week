//using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitreg : MonoBehaviour
{

    AttackHandler attackHandler;

    private void Awake()
    {
        attackHandler = GameObject.Find("Platyer").GetComponent<AttackHandler>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        //compare tag better lol
        if ((collision.collider.CompareTag("sword") && attackHandler.isAttacking) || collision.collider.CompareTag("spell"))
            Debug.LogWarning("Hit!");
        if (collision.collider.tag == "enemySword")
            Debug.Log("what the fuk is wrong with you, dont get hit");
    }
}
