using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _playerRB;   
    private Vector2 _playerForces = Vector2.zero;


    [Header("Thruster Stats")]
    public float thrustForce = 75.0f;
    public float fuelEfficiency = 1.0f;
    public float fuelCapacity = 100.0f;
    public bool autoBrake = false;

    void Start()
    {
        _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        _playerForces = new Vector2(Input.GetAxisRaw("Horizontal") * thrustForce * Time.deltaTime, Input.GetAxisRaw("Vertical") * thrustForce * Time.deltaTime);
        _playerRB.AddForce(_playerForces);
    }
}
