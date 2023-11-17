using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [Header("")]
    public float thrusterRotateSpeed = 5f;
    public float armsRotateSpeed = 5f;
    private Vector2 lastVelocity = Vector2.zero;
    private Vector2 currentAcceleration = Vector2.zero;
    private Vector2 _forces;
    private Transform _arms;
    private Transform _thruster;
    private Rigidbody2D _rb;
    private Animator _thrusterAnimator;
    
    [Header("Thruster Stats")]
    public float thrustForce = 75.0f;
    public float maxSpeed = 30.0f;
    public float fuelEfficiency = 1.0f;
    public float fuelCapacity = 100.0f;
    public bool autoBrake = false;


    


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _arms = transform.Find("Arms");
        _thruster = transform.Find("Thruster");
        _thrusterAnimator = GameObject.Find("Thruster_1").GetComponent<Animator>(); 
        
    }
    
    void Update()
    {
        MovePlayer();
        if (_forces != Vector2.zero)
        {
            _thrusterAnimator.Play("Flame", 1);
            RotateThruster();
        } else
        RotateArms();
    }

    void MovePlayer()
    {
        _forces = new Vector2(Input.GetAxisRaw("Horizontal") * thrustForce * Time.deltaTime, Input.GetAxisRaw("Vertical") * thrustForce * Time.deltaTime);
        _rb.AddForce(_forces);
        
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
       // Debug.Log(_rb.velocity);
    }


    private void RotateThruster()
    {
        float angle = Mathf.Atan2(_forces.y, _forces.x) * Mathf.Rad2Deg;
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
