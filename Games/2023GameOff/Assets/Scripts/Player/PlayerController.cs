using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [Header("")]
    private float _thrusterRotateSpeed = 8f;
    private float _armsRotateSpeed = 5f;
    private Vector2 _forces;
    private Transform _arms;
    private Transform _thruster;
    private Transform _basePointer;
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
        _basePointer = transform.Find("Base Pointer");
    }
    
    void Update()
    {
        MovePlayer();
        if (Input.GetKey(KeyCode.Space))
        {
            Brake();
        }  
        if (_forces != Vector2.zero)
        {
            RotateThruster();
        }
        RotateArms();
        RotatePointer();

        

    }

    void MovePlayer()
    {
        _forces = new Vector2(Input.GetAxisRaw("Horizontal") * thrustForce * Time.deltaTime, Input.GetAxisRaw("Vertical") * thrustForce * Time.deltaTime);
        _rb.AddForce(_forces);
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);

    }


    private void RotateThruster()
    {
        float angle = Mathf.Atan2(_forces.y, _forces.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _thruster.rotation = Quaternion.Slerp(_thruster.rotation, rotation, _thrusterRotateSpeed * Time.deltaTime);
    }

    private void RotateArms()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 direction = mousePosition - objectScreenPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        _arms.rotation = Quaternion.Slerp(_arms.rotation, rotation, _armsRotateSpeed * Time.deltaTime);
    }
    private void RotatePointer()
    {
        Vector2 basePosition = Vector2.zero;
        Vector2 objectScreenPosition = transform.position;
        
        Vector2 direction = basePosition - objectScreenPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _basePointer.rotation = Quaternion.Slerp(_basePointer.rotation, rotation, 15f * Time.deltaTime);
    }

    private void Brake()
    {
        if (_forces == Vector2.zero)
        {
            _forces += -_rb.velocity * _rb.mass;
            _rb.AddForce(_forces);
        }
    }
}
