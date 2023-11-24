using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Gives each rock and stone a random appearance
/// </summary>
public class Asteroid : MonoBehaviour
{
    [Header("Appearance")]
    [SerializeField] Sprite[] sprites;
    [Header("Number Stuff")]
    //Velocity
    [SerializeField] float minVel;
    [SerializeField] float maxVel;
    //Angular Velocity
    [SerializeField] float minAngVel;
    [SerializeField] float maxAngVel;
    //Health to begin with
    [SerializeField] float startingHealth;
    /// <summary>
    /// Multiplied by size for rigidbody mass
    /// </summary>
    [SerializeField] float baseMass;

    [Header("Effects")]
    //Particles when destroyed
    [SerializeField] ParticleSystem destroyParticles;
    //Particles if asteroid has a mineral
    [SerializeField] ParticleSystem shinyParticles;

    /// <summary>
    /// Current hitpoints of asteroid
    /// </summary>
    public float health { get; private set; }

    /// <summary>
    /// Valuable asteroids drop minerals. If false, drop nothing.
    /// </summary>
    public bool isValuable { get { return mineral != null; } }
    /// <summary>
    /// The current mineral inside the asteroid. Set by spawner.
    /// </summary>
    private Mineral mineral;

    public float size { get; private set; }

    /// <summary>
    /// The currently selected object. Null if nothing.
    /// </summary>
    public static Asteroid selectedAsteroid { get; private set; }

    //Component cache
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();


        if (sprites != null && sprites.Length > 0)
        {
            // choose a random rock
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        //Store size of asteroid
        size = spriteRenderer.bounds.size.magnitude;
        //Make asteroids heavier based on mass
        rb.mass = baseMass * size;

        //create collider after sprite change to ensure the hitbox matches visuals.
        //Do not add collider to sprite before object creation or things could go breaky
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();


        //add random velocity and rotational velocity
        rb.velocity = new Vector2(Random.Range(minVel, maxVel), Random.Range(minVel, maxVel));
        rb.angularVelocity = Random.Range(minAngVel, maxAngVel);

        health = startingHealth;
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


    /// <summary>
    /// Deal 'dmg' points of damage to asteroid. If health is depleted, destory
    /// </summary>
    /// <param name="dmg"></param>
    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            DestroyAsteroid();
        }
    }

    private void DestroyAsteroid()
    {
        //Deactivate sprite renderer and collider.
        polygonCollider.enabled = false;
        spriteRenderer.enabled = false;

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
        //Begin destroy particle system. The PS will destroy the object after done
        destroyParticles.Play(false);

        //play explosion sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/SPACE/ROCK_EXPLODE", gameObject.transform.position);
    }

    //Use pointer exit and enter to determine if asteroid is hovered over by mouse
    public void OnMouseOver()
    {
        selectedAsteroid = this;
        spriteRenderer.color = Color.gray;
    }

    public void OnMouseExit()
    {
        selectedAsteroid = null;
        spriteRenderer.color = Color.white;
    }
}
