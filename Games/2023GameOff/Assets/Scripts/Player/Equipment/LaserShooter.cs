using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float laserDistance = 100.0f;
    private Transform _laserPoint;
    private LineRenderer _lineRenderer;
    private Transform m_transform;

    [Header("Laser Stats")]
    public float laserDamage = 80;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        _lineRenderer = GameObject.Find("LaserArm_1").GetComponent<LineRenderer>();
        _laserPoint = GameObject.Find("LaserPoint").transform;
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
            Draw2DRay(_laserPoint.position, target);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
        

        

    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);
    }
}
