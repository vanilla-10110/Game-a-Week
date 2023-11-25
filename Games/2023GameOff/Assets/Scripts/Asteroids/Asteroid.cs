using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gives each rock and stone a random appearance
/// </summary>
public class Asteroid : SpaceObject
{
    //Particles if asteroid has a mineral
    [SerializeField] ParticleSystem shinyParticles;

    /// <summary>
    /// Probability for there to be a mineral in it. 0 - impossible, 1 - certain
    /// </summary>
    public float mineralChance;

    /// <summary>
    /// Minerals that this object can contain
    /// </summary>
    public Mineral[] possibleMinerals;

    /// <summary>
    /// Valuable asteroids drop minerals. If false, drop nothing.
    /// </summary>
    public bool isValuable { get { return mineral != null; } }
    /// <summary>
    /// The current mineral inside the asteroid. Set by spawner.
    /// </summary>
    private Mineral mineral;

    protected override void OnInitialise()
    {
        base.OnInitialise();

        //create collider after sprite change to ensure the hitbox matches visuals.
        //Do not add collider to sprite before object creation or things could go breaky
        objectCollider = gameObject.AddComponent<PolygonCollider2D>();

        //Assign random mineral.
        //This will give shine effect to asteroid, and make it drop the mineral
        //TODO: make this a weighted probability so valuable minerals show up less often
        if (possibleMinerals != null && Random.value > mineralChance)
        {
            var m = possibleMinerals[Random.Range(0, possibleMinerals.Length)];
            SetMineral(m);
        }
    }

    public void SetMineral(Mineral m)
    {
        //something went wrong, theres no mineral
        if (m == null)
        {
            return;
        }

        shinyParticles.Play(false);
        mineral = m;
    }

    protected override void DestroyObject()
    {
        base.DestroyObject();

        //Create mineral, if it has one
        if (isValuable)
        {
            GameObject createdMineral = Instantiate(mineral.gameObject);
            //Positon where asteroid is
            createdMineral.transform.position = transform.position;
            //And give same velocity
            createdMineral.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        }

        //Stop shine particles immediately
        shinyParticles.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);

        //play explosion sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/SPACE/ROCK_EXPLODE", gameObject.transform.position);
    }

}
