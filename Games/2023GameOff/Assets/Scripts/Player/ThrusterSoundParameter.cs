using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrusterSoundParameter : MonoBehaviour
{
    private float Thrust;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 || Input.GetKey(KeyCode.Space))
        {
            Thrust = 1;
        }
        
        else
        {
            Thrust = 0;
        }
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("THRUST_POWER", Thrust);
    }
}
