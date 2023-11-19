using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives each rock and stone a random appearance
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //give random appearance
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        //create collider after sprite change to ensure the hitbox matches visuals.
        //Do not add collider to sprite before object creation or things could go breaky
        gameObject.AddComponent<PolygonCollider2D>();
    }

}
