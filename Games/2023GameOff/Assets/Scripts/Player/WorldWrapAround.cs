using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This should be attached to player. Continuosly checks if player is outside of world boundry. If so, moves them to other side of the boundary.
/// </summary>

public class WorldWrapAround : MonoBehaviour
{
    /// <summary>
    /// Limit of how far player can travel. Also influences asteroid spawning.
    /// </summary>
    public static int worldSize { get; } = 200;

    private void Update()
    {
        WrapOutOfBounds();
    }

    void WrapOutOfBounds()
    {
        Vector3 newTransform = transform.position;
        //Check X
        if (transform.position.x > worldSize)
        {
            newTransform.x = -worldSize;
        }
        if (transform.position.x < -worldSize)
        {
            newTransform.x = worldSize;
        }
        //Check Y
        if (transform.position.y > worldSize)
        {
            newTransform.y = -worldSize;
        }
        if (transform.position.y < -worldSize)
        {
            newTransform.y = worldSize;
        }

        //Apply change, if nessecary
        if (newTransform != transform.position)
        {
            transform.position = newTransform;

            CameraController.main.TeleportToTarget();
        }
    }
}
