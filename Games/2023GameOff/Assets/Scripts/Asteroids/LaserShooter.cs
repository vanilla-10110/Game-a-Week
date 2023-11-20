using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float shootSpeed;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        //Shoot on click
        if (Asteroid.selectedAsteroid != null && Input.GetMouseButton(0) && timer > shootSpeed)
        {
            Shoot();
            timer = 0;
        }
    }

    void Shoot()
    {
        //Rest of logic handled on the laser
        GameObject obj = Instantiate(laserPrefab);
        obj.transform.position = transform.position;
    }
}
