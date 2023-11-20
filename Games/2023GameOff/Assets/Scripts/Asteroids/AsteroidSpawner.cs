using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns asterioids on game start. 
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] Asteroid asteroidPrefab;
    [SerializeField] Mineral[] possibleMinerals;
    [SerializeField] float mineralChance;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;
    /// <summary>
    /// Asteroids cannot spawn in a circle this many units from 0, 0.
    /// </summary>
    [SerializeField] float minDistanceFromBase;
    /// <summary>
    /// Required spacing between asteroids
    /// </summary>
    [SerializeField] int overlapCheckRadius;
    /// <summary>
    /// Used in Physics.OverlapCircle to only detect asteroids, not the player, base, etc.
    /// </summary>
    [SerializeField] ContactFilter2D overlapContactFilter;

    void Start()
    {
        StartCoroutine(CreateAsteroidsAsync());
    }

    IEnumerator CreateAsteroidsAsync()
    {
        //Chose amount within range
        int amount = Random.Range(minAmount, maxAmount);
        for (int i = 0; i < amount; i++)
        {
            CreateAsteroid();
            //Waits a frame for collider to activate.
            //Code uses collison to check for overlaps. Must wait for collider to be active first.
            //Will show asteroids popping in one at a time. Shouldn't be too much of an issue.
            yield return null;
        }
    }

    void CreateAsteroid()
    {
        //random position
        Vector3 pos = new Vector3(Random.Range(-WorldWrapAround.worldSize, WorldWrapAround.worldSize), Random.Range(-WorldWrapAround.worldSize, WorldWrapAround.worldSize), 0);

        //Must be certain distance away from base
        if (Vector3.Distance(Vector3.zero, pos) < minDistanceFromBase)
        {
            //Debug.Log("Too Close");
            return;
        }
        //Prevent overlapping with other asteroids. Needs work
        if (CheckOverlap(pos))
        {
            return;
        }

        //create object an assign position
        GameObject obj = Instantiate(asteroidPrefab.gameObject);
        obj.transform.position = pos;

        //Assign random mineral.
        //This will give shine effect to asteroid, and make it drop the mineral
        //TODO: make this a weighted probability so valuable minerals show up less often
        if (Random.value > mineralChance)
        {
            obj.GetComponent<Asteroid>().SetMineral(possibleMinerals[Random.Range(0, possibleMinerals.Length)]);
        }
    }

    /// <summary>
    /// Returns true if there is an overlap with another asteroid.
    /// Creates a circle centered arround given 'point'
    /// Uses physics to check colliders in this circle
    /// The overlapContactFilter only detects collisions with the 'Asteroid' tag.
    /// </summary>
    bool CheckOverlap(Vector3 point)
    {
        Collider2D[] results = new Collider2D[5];
        int objectsDetected = Physics2D.OverlapCircle(point, overlapCheckRadius, overlapContactFilter, results);

        if (objectsDetected > 0)
        {
            //Debug.Log($"{point} overlaps with {results[0].transform.position}");
            return true;
        }
        else
        {
            return false;
        }
    }
}
