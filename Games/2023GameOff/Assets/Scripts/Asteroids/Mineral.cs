using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    /// <summary>
    /// Name of mineral. Other properties could be added later
    /// </summary>
    public string mineralName;

    //If touching player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.usedByEffector == false)
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
}
