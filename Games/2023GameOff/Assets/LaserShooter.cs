using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float laserDistance = 100.0f;
    private Transform _laserPoint;
    private LineRenderer _lineRenderer;
    private Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        _lineRenderer = GetComponent<LineRenderer>();
        _laserPoint = GameObject.Find("LaserPoint").transform;
    }
    private void Update()
    {
        FireLaser();
    }

    private void FireLaser()
    {
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right);
            Draw2DRay(_laserPoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(_laserPoint.position, _laserPoint.transform.right * laserDistance);
        }

        void Draw2DRay(Vector2 startPos, Vector2 endPos)
        {
            _lineRenderer.SetPosition(0, startPos);
            _lineRenderer.SetPosition(1, endPos);
        }

    }
}
