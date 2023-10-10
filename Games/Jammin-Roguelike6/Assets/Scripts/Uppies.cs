using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uppies : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;



    Vector3 lastLocation;


    private void Awake()
    {
        player = GameObject.Find("Platyer");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.isGrounded == true)
        {
            lastLocation = player.transform.position;
        }
    }




    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.transform.position = lastLocation;
        }
    }
}
