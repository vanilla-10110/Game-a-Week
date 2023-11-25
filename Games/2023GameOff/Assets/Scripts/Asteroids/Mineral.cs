using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    /// <summary>
    /// Name of mineral. Other properties could be added later
    /// </summary>
    public string mineralName;

    public static Mineral selectedMineral { get; private set; }


    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //If touching player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp();
        }
    }

    /// <summary>
    /// Mineral is collected. Replace with whatever is needed
    /// </summary>
    void PickUp()
    {
        Debug.Log("Collected " + mineralName);
        Destroy(gameObject);
    }

    public void OnMouseOver()
    {
        selectedMineral = this;
        spriteRenderer.color = Color.gray;
    }

    public void OnMouseExit()
    {
        selectedMineral = null;
        spriteRenderer.color = Color.white;
    }
}
