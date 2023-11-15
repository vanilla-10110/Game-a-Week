using System;
using UnityEngine;

public class ArmControls : MonoBehaviour
{
    //do some headings silly
    public float thrusterRotateSpeed = 5f;
    public float armsRotateSpeed = 5f;
    private Vector2 lastVelocity = Vector2.zero;
    private Vector2 currentAcceleration = Vector2.zero;
    private Rigidbody2D rb;
    private Transform _arms;
    private Transform _thruster;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _arms = transform.Find("Arms");
        _thruster = transform.Find("Thruster");
    }

    private void Update()
    {
        //might combine into a single rotate function
        RotateThruster();
        RotateArms();
    }

    private void RotateThruster()
    {
        //Does not rotate based on acceleration (force applied) and instead rotates
        currentAcceleration = (rb.velocity - lastVelocity) / Time.deltaTime;
        float angle = Mathf.Atan2(currentAcceleration.y, currentAcceleration.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _thruster.rotation = Quaternion.Slerp(_thruster.rotation, rotation, thrusterRotateSpeed * Time.deltaTime);
    }

    private void RotateArms()
    {
        //i think this works but i could very well have missed something
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 direction = mousePosition - objectScreenPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _arms.rotation = Quaternion.Slerp(_arms.rotation, rotation, armsRotateSpeed * Time.deltaTime);
    }
}
