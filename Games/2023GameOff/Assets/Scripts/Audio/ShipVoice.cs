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

// warning played bools
    private bool O2WarningPlayed = true;
    private bool FuelWarningPlayed = true;
    private bool HullWarningPlayed = true;
    private bool PowerWarningPlayed = true;



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
        hull = (gameObject.GetComponent<PlayerController>().hullHealth);
        //print(hull);
        power = (gameObject.GetComponent<LaserShooter>().powerAmount / gameObject.GetComponent<LaserShooter>().powerMaxCapacity * 100f);
        testO2Warning();
        testFuelWarning();
        testHullWarning();
        testPowerWarning();
    }

    void testO2Warning()
    {
        //O2 Warning
        if (O2 < lowO2 || O2WarningPlayed == false) //check if O2 is low or if sound has not been played yet
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is not playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_O2", O2); //set warning value
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "O2"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound
                O2WarningPlayed = true; //set played bool to true so it doesn't repeat
            }
            if (PlaybackState(shipvoice) == FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is playing
            {
                O2WarningPlayed = false; 
            }
        }
        setLowO2();
    }

    void setLowO2()
    {
        if (O2 > 50f)
            lowO2 = 50f;
        if (O2 < 50f & O2 > 25f)
            lowO2 = 25f;
        if (O2 < 25f & O2 > 15f)
            lowO2 = 15f;
        if (O2 < 15f & O2 > 5f)
            lowO2 = 5f;
        if (O2 < 5f & O2 > 0f)
            lowO2 = 0f;
        if (O2 < 0f)
            lowO2 = -10000f;
    }
    void testFuelWarning()
    {
        //fuel Warning
        if (fuel < lowFuel || FuelWarningPlayed == false) //check if fuel is low or if sound has not been played yet
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is not playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_FUEL", fuel); //set warning value
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "FUEL"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound
                FuelWarningPlayed = true; //set played bool to true so it doesn't repeat
            }
            if (PlaybackState(shipvoice) == FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is playing
            {
                FuelWarningPlayed = false; 
            }
        }
        setLowFuel();
    }

    void setLowFuel()
    {
        if (fuel > 50f)
            lowFuel = 50f;
        if (fuel < 50f & O2 > 25f)
            lowFuel = 25f;
        if (fuel < 25f & O2 > 15f)
            lowFuel = 15f;
        if (fuel < 15f & O2 > 5f)
            lowFuel = 5f;
        if (fuel < 5f & O2 > 0f)
            lowFuel = 0f;
        if (fuel < 0f)
            lowFuel = -10000f;
    }

    void testHullWarning()
    {
        //hull Warning
        if (hull < lowHull || HullWarningPlayed == false) //check if hull is low or if sound has not been played yet
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is not playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_HULL", hull); //set warning value
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "HULL"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound
                HullWarningPlayed = true; //set played bool to true so it doesn't repeat
            }
            if (PlaybackState(shipvoice) == FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is playing
            {
                HullWarningPlayed = false; 
            }
        }
        setLowHull();
    }

    void setLowHull()
    {
        if (hull > 50f)
            lowHull = 50f;
        if (hull < 50f & O2 > 25f)
            lowHull = 25f;
        if (hull < 25f & O2 > 15f)
            lowHull = 15f;
        if (hull < 15f & O2 > 5f)
            lowHull = 5f;
        if (hull < 5f & O2 > 0f)
            lowHull = 0f;
        if (hull < 0f)
            lowHull = -10000f;
    }

    void testPowerWarning()
    {
        //power Warning
        if (power < lowPower || PowerWarningPlayed == false) //check if hull is low or if sound has not been played yet
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is not playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "POWER"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound
                PowerWarningPlayed = true; //set played bool to true so it doesn't repeat
            }
            if (PlaybackState(shipvoice) == FMOD.Studio.PLAYBACK_STATE.PLAYING) //do if sound is playing
            {
                PowerWarningPlayed = false; 
            }
        }
        setLowPower();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_POWER", power); //set warning value, it's here for the power because it affects all warning sounds
    }

    void setLowPower()
    {
        if (power > 50f)
            lowPower = 50f;
        if (power < 50f & O2 > 25f)
            lowPower = 25f;
        if (power < 25f & O2 > 15f)
            lowPower = 15f;
        if (power < 15f & O2 > 5f)
            lowPower = 5f;
        if (power < 5f & O2 > 0f)
            lowPower = 0f;
        if (power < 0f)
            lowPower = -10000f;
    }
 

    //playback check method
    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }
}
