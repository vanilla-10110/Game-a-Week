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
    [SerializeField] Sprite susSprite;
    [Header("Number Stuff")]
    //Velocity
    [SerializeField] float minVel;
    [SerializeField] float maxVel;
    //Angular Velocity
    [SerializeField] float minAngVel;
    [SerializeField] float maxAngVel;
    //Health to begin with
    [SerializeField] float startingHealth;

    [Header("Effects")]
    [SerializeField] ParticleSystem destroyParticles;
    [SerializeField] ParticleSystem shinyParticles;

    /// <summary>
    /// Current hitpoints of asteroid
    /// </summary>
    public float health { get; private set; }

    /// <summary>
    /// Valuable asteroids drop minerals. If false, drop nothing.
    /// </summary>
    public bool isValuable { get { return mineral != null; } }
    private Mineral mineral;

    public static Asteroid selectedAsteroid { get; private set; }
    private bool selected;

    //Component cache
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        

        //1 in 100 chance for amogus asteroid
        if (Random.Range(0, 100) == 1)
        {
            spriteRenderer.sprite = susSprite;
        }
        //otherwise choose a random rock
        else
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        //create collider after sprite change to ensure the hitbox matches visuals.
        //Do not add collider to sprite before object creation or things could go breaky
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        //add random velocity and rotational velocity
        rb.velocity = new Vector2(Random.Range(minVel, maxVel), Random.Range(minVel, maxVel));
        rb.angularVelocity = Random.Range(minAngVel, maxAngVel);

        health = startingHealth;
    }

    public void SetMineral(Mineral m)
    {
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
            createdMineral.transform.position = transform.position;
        }

        //Stop shine particles immediately
        shinyParticles.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        //Begin destroy particle system. The PS will destroy the object after done
        destroyParticles.Play(false);
    }

    //Use pointer exit and enter to determine if asteroid is hovered over by mouse
    public void OnMouseOver()
    {
        selected = true;
        selectedAsteroid = this;
        spriteRenderer.color = Color.gray;
    }

    public void OnMouseExit()
    {
        selected = false;
        selectedAsteroid = null;
        spriteRenderer.color = Color.white;
    }
}
