using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float laserDistance = 100.0f;
    private Transform _laserPoint;
    private Transform _grabberPoint;

    private LineRenderer _lineLaser;
    private LineRenderer _lineGrabber;
    //private Transform m_transform;

    [Header("Laser Stats")]
    public float laserDamage = 80.0f;

    [Header("Grabber Stats")]
    public float grabberForce = 60.0f;

    private void Awake()
    {
        //m_transform = GetComponent<Transform>();
        _lineLaser = GameObject.Find("LaserArm_1").GetComponent<LineRenderer>();
        _lineGrabber = GameObject.Find("TractorArm_1").GetComponent<LineRenderer>();
        _laserPoint = GameObject.Find("LaserPoint").transform;
        _grabberPoint = GameObject.Find("GrabberPoint").transform;
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
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                
            }
            else
            {
                target = Asteroid.selectedAsteroid.transform.position;
                Asteroid.selectedAsteroid.Damage(laserDamage * Time.deltaTime);
            }
            DrawLaser(_laserPoint.position, target);
        }
        else
        {
            _lineLaser.enabled = false;
        }
        if (Input.GetButton("Fire2"))
        {
            Vector3 target;
            if (Asteroid.selectedAsteroid == null)
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                
            }
            else
            {
                target = Asteroid.selectedAsteroid.transform.position;
                Asteroid.selectedAsteroid.rb.AddForce((_grabberPoint.transform.position - Asteroid.selectedAsteroid.transform.position).normalized * grabberForce);
            }
            DrawGrabber(_grabberPoint.position, target);
        }
        else
        {
            _lineGrabber.enabled = false;
        }
        

        

    }
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
