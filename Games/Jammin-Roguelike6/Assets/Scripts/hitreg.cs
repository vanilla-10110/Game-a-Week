using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitreg : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.tag == "sword") 
            Debug.Log("Hit!");
        if (collision.collider.tag == "enemySword")
            Debug.Log("what the fuk is wrong with you, dont get hit");
    }
}
