using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipVoice : MonoBehaviour
{

    private FMOD.Studio.EventInstance shipvoice;
    private float lowO2;
    private bool O2warningMuted = false;

    // Start is called before the first frame update
    void Start()
    {
        shipvoice = FMODUnity.RuntimeManager.CreateInstance("event:/AI/AI_WARNING");
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
        //define variables and set parameters
        float O2Percent = (gameObject.GetComponent<PlayerController>().O2Amount / gameObject.GetComponent<PlayerController>().maxO2Capacity * 100f);
        float fuelPercent = (gameObject.GetComponent<PlayerController>().fuelAmount / gameObject.GetComponent<PlayerController>().maxFuelCapacity * 100f);
        float hullPercent = (gameObject.GetComponent<PlayerController>().hullHealth / 100f);
        float powerPercent = (gameObject.GetComponent<LaserShooter>().powerAmount / gameObject.GetComponent<LaserShooter>().powerMaxCapacity * 100f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_O2", O2Percent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_FUEL", fuelPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_HULL", hullPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_POWER", powerPercent);

        //O2 Warning
        if (O2Percent < lowO2 & O2warningMuted == false) //check if o2 is low
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "O2"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound

                //set new low o2 value
                if (O2Percent < 50f & O2Percent > 25f)
                {
                    lowO2 = 25f;
                }
                if (O2Percent < 25f & O2Percent > 15f)
                {
                    lowO2 = 15f;
                }
                if (O2Percent < 15f & O2Percent > 5f)
                {
                    lowO2 = 5f;
                }
                if (O2Percent < 5f)
                {
                    lowO2 = 0f;
                }
                if (O2Percent < 0f)
                {
                    O2warningMuted = true; //mute o2 warning after o2 reaches 0
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
