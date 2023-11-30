using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVoice : MonoBehaviour
{

    private FMOD.Studio.EventInstance shipvoice;


// percentages of ship resources
    private float O2;
    private float fuel;
    private float hull;
    private float power;

// values to trigger warning sound
    private float lowO2;
    private float lowFuel;
    private float lowHull;
    private float lowPower;

// warning muted bools
    private bool O2warningMuted = false;


// warning qued bools
    private bool O2warningQued = false;

    // Start is called before the first frame update
    void Start()
    {
        shipvoice = FMODUnity.RuntimeManager.CreateInstance("event:/AI/AI_WARNING"); //evemt for ship voice

        //low resource values
        lowO2 = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayShipVoice(); 
        // shipvoice.start(); //play voice every fram (for debugging)
    }

    private void PlayShipVoice()
    {
        //get the ships resource values as percentages
        O2 = (gameObject.GetComponent<PlayerController>().O2Amount / gameObject.GetComponent<PlayerController>().maxO2Capacity * 100f);
        fuel = (gameObject.GetComponent<PlayerController>().fuelAmount / gameObject.GetComponent<PlayerController>().maxFuelCapacity * 100f);
        hull = (gameObject.GetComponent<PlayerController>().hullHealth / 100f);
        power = (gameObject.GetComponent<LaserShooter>().powerAmount / gameObject.GetComponent<LaserShooter>().powerMaxCapacity * 100f);
        testO2Warning();
    }

/*
        Unused Parameter Update Code
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_FUEL", fuel);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_HULL", hull);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_POWER", power);
*/

    void testO2Warning()
    {
        //O2 Warning
        if (O2 < lowO2) //check if O2 is low
        {
            O2warningQued = true;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_O2", O2);
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING & O2warningMuted == false & O2warningQued == true) //check to make sure sound isn't playing, not set to mute, and qued
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "O2"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound
                O2warningQued = false;

                //set new low O2 value
                if (O2 < 50f & O2 > 25f)
                {
                    lowO2 = 25f;
                }
                if (O2 < 25f & O2 > 15f)
                {
                    lowO2 = 15f;
                }
                if (O2 < 15f & O2 > 5f)
                {
                    lowO2 = 5f;
                }
                if (O2 < 5f)
                {
                    lowO2 = 0f;
                }
                if (O2 < 0f)
                {
                    O2warningMuted = true; //mute O2 warning after O2 reaches 0
                }
            }
        }
    }

    //playback check method
    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }
}
