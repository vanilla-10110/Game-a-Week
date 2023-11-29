using FMOD.Studio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;
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
    private FMOD.Studio.EventInstance laserhitmetalsound;
    private FMOD.Studio.EventInstance tractorsound;
    //declare FMOD instances

    [Header("Laser Stats")]
    public float laserDamage = 80.0f;
    public float grabberForce = 250.0f;
    public float powerMaxCapacity = 200.0f;
    public float powerEfficiency = 1.0f;
    public float powerAmount;

    private void Awake()
    {
        _lineLaser = GameObject.Find("LaserArm_1").GetComponent<LineRenderer>();
        _lineGrabber = GameObject.Find("TractorArm_1").GetComponent<LineRenderer>();
        _laserPoint = GameObject.Find("LaserPoint").transform;
        _grabberPoint = GameObject.Find("GrabberPoint").transform;

        powerAmount = powerMaxCapacity;

        fmodsetup();

    }
    private void Update()
    {
        FireLaser();
    }

    private void FireLaser()
    {
        if (Input.GetButton("Fire1") && powerAmount > 0)
        {
            Vector3 target;
            if (Asteroid.selectedAsteroid == null)
            {
                // if (Physics2D.Raycast(_laserPoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized))
                
                RaycastHit2D hit = Physics2D.Raycast(_laserPoint.position, Camera.main.ScreenToWorldPoint(Input.mousePosition - _laserPoint.transform.position).normalized);

                if (hit.collider.CompareTag("Rock"))
                {
                    target = hit.transform.position;
                }
                else
                {
                    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    target.z = 0;
                }

                

                powerAmount -= powerEfficiency * Time.deltaTime / 2;
                laserhitsound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop hit sounds
            }
            else
            {
                target = Asteroid.selectedAsteroid.transform.position;
                //Debug.Log(laserDamage * Time.deltaTime);
                Asteroid.selectedAsteroid.Damage(laserDamage * Time.deltaTime);

                powerAmount -= powerEfficiency * Time.deltaTime * 2;

                 //laser hit sound
                laserhitsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target)); 
                PlayHitSound();
                
            }

            DrawLaser(_laserPoint.position, target);
            lasersound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target)); //set instance location
            PlayLaserSound();

        }
        else
        {
            _lineLaser.enabled = false;
            lasersound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            laserhitsound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop laser hit sound
        }
        if (Input.GetButton("Fire2") && powerAmount > 0)
        {
            Vector3 target;
            if (SpaceObject.selectedAsteroid == null && Mineral.selectedMineral == null)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                powerAmount -= powerEfficiency * Time.deltaTime / 2;

                

                tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject)); //puts tractor beam emitter on tractor beam arm
            }
            else
            {
                bool velocityAdd;
                GameObject selection;
                if (Asteroid.selectedAsteroid == null)
                {
                    selection = Mineral.selectedMineral.gameObject;
                    velocityAdd = true;
                }
                else 
                { 
                    selection = SpaceObject.selectedAsteroid.gameObject; 
                    velocityAdd = false;
                }

                target = selection.transform.position;
                selection.GetComponent<Rigidbody2D>().AddForce((new Vector2(_grabberPoint.transform.position.x, _grabberPoint.transform.position.y) - new Vector2(selection.transform.position.x, selection.transform.position.y)).normalized * (grabberForce + selection.GetComponent<Rigidbody2D>().mass) + gameObject.GetComponent<Rigidbody2D>().velocity * selection.GetComponent<Rigidbody2D>().mass * (velocityAdd ? 1 : 0));

                //selection.GetComponent<Rigidbody2D>().AddForce((_grabberPoint.transform.position - selection.transform.position).normalized * (grabberForce + selection.GetComponent<Rigidbody2D>().mass) + gameObject.GetComponent<Rigidbody2D>().velocity);

                powerAmount -= powerEfficiency * Time.deltaTime;

                tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(target)); //puts tractor beam emitter on targeted object
            }
            DrawGrabber(_grabberPoint.position, target);
            PlayTractorSound();

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

        var direction = (endPos - startPos).normalized;
        //the hardcoded number at the end makes the laser longer     
        var realEndPos = startPos + direction * 30;

        _lineLaser.SetPosition(1, realEndPos);

    }
    void DrawGrabber(Vector2 startPos, Vector2 endPos)
    {
        _lineGrabber.enabled = true;
        _lineGrabber.SetPosition(0, startPos);
        _lineGrabber.SetPosition(1, endPos);

        //this doesn't work dunno why
        //var direction = (endPos - startPos).normalized;
        ////the hardcoded number at the end makes the laser longer     
        //var realEndPos = startPos + direction * 30;

        //_lineLaser.SetPosition(1, realEndPos);
    }

    void fmodsetup()
    {
        //Create FMOD instances
        lasersound = FMODUnity.RuntimeManager.CreateInstance("event:/SHIP/SHIP_LASER_BEAM");
        laserhitsound = FMODUnity.RuntimeManager.CreateInstance("event:/SPACE/LASER_HIT");
        tractorsound = FMODUnity.RuntimeManager.CreateInstance("event:/SHIP/SHIP_TRACTOR_BEAM");

        //set instance locations immediately so console doesn't throw warnings
        lasersound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        laserhitsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        tractorsound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
    void PlayLaserSound()
    {
        if (PlaybackState(lasersound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            {
                lasersound.start(); //play sound at instance location
            }
    }
    void PlayTractorSound()
    {
        if (PlaybackState(tractorsound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
        {
            tractorsound.start(); //play sound at instance location
        }
    }
    void PlayHitSound()
    {
        if (PlaybackState(laserhitsound) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            laserhitsound.start(); //play sound at instance location
    }
   

}
