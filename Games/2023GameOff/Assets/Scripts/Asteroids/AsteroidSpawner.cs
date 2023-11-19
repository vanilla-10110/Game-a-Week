using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns an amount (random between min and max range) of asterioids on game start
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;

    void Start()
    {
        CreateAsteroids();
    }

    void CreateAsteroids()
    {
        int amount = Random.Range(minAmount, maxAmount);
        for (int i = 0; i < amount; i++)
        {
            //random position. Can double up. Change later
            Vector3 pos = new Vector3(Random.Range(-WorldWrapAround.worldSize, WorldWrapAround.worldSize), Random.Range(-WorldWrapAround.worldSize, WorldWrapAround.worldSize), 0);

            //create object an assign position
            GameObject obj = Instantiate(asteroidPrefab.gameObject);
            obj.transform.position = pos;
        }
    }
}
