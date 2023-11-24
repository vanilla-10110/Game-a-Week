using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.Rendering.DebugUI;

public class LaserShooter : MonoBehaviour
{
    public float laserDistance = 100.0f;
    private Transform _laserPoint;
    private Transform _grabberPoint;

    private LineRenderer _lineLaser;
    private LineRenderer _lineGrabber;
    //private Transform m_transform;

    private FMOD.Studio.EventInstance lasersound;
    private FMOD.Studio.EventInstance laserhitsound;
    private FMOD.Studio.EventInstance tractorsound;
    //declare FMOD instances

    [Header("Laser Stats")]
    public float laserDamage = 80.0f;
    public float grabberForce = 60.0f;
    public float powerMaxCapacity = 100.0f;
    public float powerEfficiency = 1.0f;

    private void Awake()
    {
        _lineLaser = GameObject.Find("LaserArm_1").GetComponent<LineRenderer>();
        _lineGrabber = GameObject.Find("TractorArm_1").GetComponent<LineRenderer>();
        _laserPoint = GameObject.Find("LaserPoint").transform;
        _grabberPoint = GameObject.Find("GrabberPoint").transform;

        //FMOD
        lasersound = FMODUnity.RuntimeManager.CreateInstance("event:/SHIP/SHIP_LASER_BEAM");
        laserhitsound = FMODUnity.RuntimeManager.CreateInstance("event:/SPACE/ROCK_DRILL_HIT");
        tractorsound = FMODUnity.RuntimeManager.CreateInstance("event:/SHIP/SHIP_TRACTOR_BEAM");
        //Create FMOD instances
        lasersound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        laserhitsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        //set instance locations immediately so console doesn't throw warnings

    }
    private void Update()
    { 
        FireLaser();
    }

    private void FireLaser()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 target;
            if (Asteroid.selectedAsteroid == null)
            {
               // if (Physics2D.Raycast(_laserPoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized))

                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;

                laserhitsound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                
            }
            else
            {
                target = Asteroid.selectedAsteroid.transform.position;
                Asteroid.selectedAsteroid.Damage(laserDamage * Time.deltaTime);

                laserhitsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target));
                if (PlaybackState(laserhitsound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
                {
                    laserhitsound.start(); //play sound at instance location
                }
            }
            DrawLaser(_laserPoint.position, target);
            lasersound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target)); //set instance location
            if (PlaybackState(lasersound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            {
                lasersound.start(); //play sound at instance location
            }
            
        }
        else
        {
            _lineLaser.enabled = false;
            lasersound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop sound
            laserhitsound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop sound
        }
        if (Input.GetButton("Fire2"))
        {
            Vector3 target;
            if (Asteroid.selectedAsteroid == null)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject)); //set instance location
            }
            else
            {
                target = Asteroid.selectedAsteroid.transform.position;
                Asteroid.selectedAsteroid.rb.AddForce((_grabberPoint.transform.position - Asteroid.selectedAsteroid.transform.position).normalized * grabberForce);
                tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target)); //set instance location
            }
            DrawGrabber(_grabberPoint.position, target);

            if (PlaybackState(tractorsound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            {
                tractorsound.start(); //play sound at instance location
            }
            
        }
        else
        {
            _lineGrabber.enabled = false;
            tractorsound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop sound
        }
        
    }

    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }
    //creates a method that declares an eventinstance as a parameter and returns the PLAYBACK_STATE of the instance

    
    void DrawLaser(Vector2 startPos, Vector2 endPos)
    {
        _lineLaser.enabled = true;
        _lineLaser.SetPosition(0, startPos);
        _lineLaser.SetPosition(1, endPos);
    }
    void DrawGrabber(Vector2 startPos, Vector2 endPos)
    {
        _lineGrabber.enabled = true;
        _lineGrabber.SetPosition(0, startPos);
        _lineGrabber.SetPosition(1, endPos);
    }
    
}
