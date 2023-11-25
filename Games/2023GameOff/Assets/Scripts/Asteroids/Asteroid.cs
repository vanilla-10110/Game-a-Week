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
