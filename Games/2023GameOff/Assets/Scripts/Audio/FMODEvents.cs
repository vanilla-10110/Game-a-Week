using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Asteroid SFX")]
    [field: SerializeField] public EventReference asteroidExplode { get; private set; }

    public static FMODEvents instance { get; private set; }
    private void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD events instance in the scene"); 
        }
        instance = this;
    }
    

}
