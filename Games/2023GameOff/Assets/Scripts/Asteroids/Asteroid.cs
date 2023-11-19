using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gives each rock and stone a random appearance
/// </summary>
public class Asteroid : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite susSprite;
    private SpriteRenderer spriteRenderer;

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
        gameObject.AddComponent<PolygonCollider2D>();
    }

}
