using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSatteliteSFX : MonoBehaviour
{
    private FMOD.Studio.EventInstance collisionSound;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionSound = FMODUnity.RuntimeManager.CreateInstance("event:/SPACE/COLLISION/METAL"); //create instance for sound
        collisionSound.setParameterByName("IMPACT_SPEED", collision.relativeVelocity.magnitude); //set impact force parameter
        collisionSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject)); // set instance location to game object
        collisionSound.start(); //play sound at instance location
        collisionSound.release(); //release instance from memory

        //Debug.Log("an Asteroid collided with a relative velocity of " + collision.relativeVelocity.magnitude);
    }
}
