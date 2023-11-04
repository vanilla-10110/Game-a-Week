using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _playerRB;
    Vector2 _playerForces = Vector2.zero;

    public float thrustForce = 75f;
    
    void Start()
    {
        _playerRB = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        _playerForces = new Vector2(Input.GetAxisRaw("Horizontal") * thrustForce * Time.deltaTime, Input.GetAxisRaw("Vertical") * thrustForce * Time.deltaTime);
        MovePlayer();
    }

    void MovePlayer()
    {
        _playerRB.AddForce(_playerForces);
    }
}
