using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CleanUp : MonoBehaviour
{

    private void Start()
    {
        Timer();
    }

    void Update()
    {
        // Get the second child (index 1)
        Transform secondChild = transform.GetChild(1);

        // If the second child exists and is active
        if (secondChild != null && secondChild.gameObject.activeInHierarchy)
        {
            // Wait for 20 seconds then call the SelfDestruct method
            Invoke("Delete", 20f);

        }
    }

    void Timer()
    {
        Invoke("Delete", 120f);
    }

    void Delete()
    {
        // Destroy this GameObject
        Destroy(gameObject);
    }
}