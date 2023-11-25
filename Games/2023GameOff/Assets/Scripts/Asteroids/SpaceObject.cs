using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for things like asteroids, sattelites, and moons
/// </summary>
public class SpaceObject : MonoBehaviour
{
    /// <summary>
    /// The currently selected object. Null if nothing.
    /// </summary>
    public static SpaceObject selectedAsteroid { get; private set; }

    /// <summary>
    /// Current hitpoints of asteroid
    /// </summary>
    public float health { get; private set; }

    /// <summary>
    /// This is based off how big sprite is.
    /// </summary>
    public float size { get; private set; }

    [Header("Appearance")]
    [SerializeField] Sprite[] sprites;
    [Header("Forces")]
    //Velocity
    [SerializeField] float minVel;
    [SerializeField] float maxVel;
    //Angular Velocity
    [SerializeField] float minAngVel;
    [SerializeField] float maxAngVel;

    /// <summary>
    /// Multiplied to get more velocity if you want
    /// </summary>
    public float velocityModifier = 1;

    /// <summary>
    /// Multiplied by size for rigidbody mass
    /// </summary>
    [SerializeField] float baseMass;

    [Header("Other")]
    //Health to begin with
    [SerializeField] float startingHealth;
    //Particles when destroyed
    [SerializeField] ParticleSystem destroyParticles;

    //Component cache
    [HideInInspector] public Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D objectCollider;

    private void Start()
    {
        OnInitialise();
    }

    /// <summary>
    /// Override this to do things on Awake
    /// By default this:
    /// - sets health
    /// - gets components
    /// - chooses random sprite
    /// - gets size and calculates mass
    /// - adds velocity and angualr velocity
    /// </summary>
    protected virtual void OnInitialise()
    {
        //Set health
        health = startingHealth;

        //Get components
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        //Random sprite, if availible
        if (sprites != null && sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }

        //Store size of object
        size = spriteRenderer.bounds.size.magnitude;
        //Make asteroids heavier based on mass
        rb.mass = baseMass * size;

        //add random velocity and rotational velocity
        rb.velocity = new Vector2(Random.Range(minVel, maxVel), Random.Range(minVel, maxVel)) * velocityModifier;
        rb.angularVelocity = Random.Range(minAngVel, maxAngVel);
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
            DestroyObject();
        }
    }

    /// <summary>
    /// Override this to do things when the object is destroyed. 
    /// By default, disables sprite renderer and collider and plays destroy particles
    /// </summary>
    protected virtual void DestroyObject()
    {
        //Deactivate sprite renderer and collider.
        //objectCollider.enabled = false;
        //spriteRenderer.enabled = false;

        //Unchild the particles (if not part of main object) and play them
        destroyParticles.transform.parent = null;
        destroyParticles.Play(false);

        Destroy(gameObject);
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

    private void Update()
    {
        WrapAround();
    }

    /// <summary>
    /// if on edge of world move to the other edge
    /// </summary>
    void WrapAround()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -WorldWrapAround.worldSize, WorldWrapAround.worldSize);
        pos.y = Mathf.Clamp(pos.y, -WorldWrapAround.worldSize, WorldWrapAround.worldSize);

        if (pos != transform.position)
            transform.position = pos;
    }
}
