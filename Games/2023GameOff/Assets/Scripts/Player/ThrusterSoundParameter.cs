using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrusterSoundParameter : MonoBehaviour
{
    private float Thrust;
    private Vector3 previous;
    private float velocity;
    private float MaxThrust = 30;

    void Update()
    {
        GetVelocity();
        SetThrust();
    }
    void GetVelocity()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
    }
    void SetThrust()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetKey(KeyCode.Space))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (velocity > 1)
                {
                    Thrust = MaxThrust;
                   // print(velocity);
                }
                else
                {
                    Thrust = velocity;
                   //  print(velocity);
                }
            }
            else
            {
                Thrust = velocity;
                // print(velocity);
            }
        }

        else
        {
            Thrust = 0;
        }
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("THRUST_POWER", Thrust);
    }
}
