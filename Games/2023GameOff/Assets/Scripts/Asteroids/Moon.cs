using UnityEngine;

/// <summary>
/// A moon is like an asteroid but it makes lots of asteroids
/// they can't have stuff in them yet
/// </summary>
public class Moon : SpaceObject
{
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] int spawnAmount;

    protected override void OnInitialise()
    {
        base.OnInitialise();

        objectCollider = GetComponent<CircleCollider2D>();
    }

    protected override void DestroyObject()
    {
        base.DestroyObject();

        //make a lot of asteroids
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject obj = Instantiate(asteroidPrefab);
            obj.GetComponent<Asteroid>().velocityModifier = 3;

            obj.transform.position = transform.position;
        }

        //play explosion sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/SPACE/ROCK_EXPLODE", gameObject.transform.position);
    }
}