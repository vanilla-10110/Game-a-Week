using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float speed;
    /// <summary>
    /// Destroy laser after this many seconds
    /// </summary>
    const float maxLifetime = 5;
    float timer;

    void Start()
    {

        Vector3 target;
        if (Asteroid.selectedAsteroid == null)
        {
            //If no asteroid selected, go to mouse
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
        }
        else
        {
            //Otherwise, lock on to asteroid
            target = Asteroid.selectedAsteroid.transform.position;
        }

        //Do vector stuff I copied off stackoverflow
        Vector2 direction = target - transform.position;
        //TODO: Dunno how to rotate laser to point to asteroid

        direction.Normalize();
        Vector2 laserVelocity = direction * speed;

        //Set velocity on rb
        GetComponent<Rigidbody2D>().velocity = laserVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision with asteroid
        if (collision.TryGetComponent(out Asteroid asteroid))
        {
            //Deal damage
            asteroid.Damage(1);
        }
    }

    //Destroy the laser if it sticks arround too long
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > maxLifetime)
        {
            Destroy(gameObject);
        }
    }
}
