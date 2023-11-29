using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Dynamic;

public class AudioManager : MonoBehaviour
{

    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake() {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the Scene.");
        }    
        instance = this;
    }
    
    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetMusicArea(MusicArea area)
    {
        musicEventInstance.setParameterByName("area", (float) area);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}
