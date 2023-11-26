using TMPro;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    private float _thrusterRotateSpeed = 8f;
    private float _armsRotateSpeed = 8f;
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
    
    
    public bool autoBrake = false;
    

    [Header("Other Stats ig")]
    public float maxO2Capacity = 200.0f;
    public float O2DrainRate = 0.2f;
    public float O2Amount;

    public float maxFuelCapacity = 200.0f;
    public float fuelAmount;

    public float hullHealth = 100.0f;
    
    [Header("UI")]
    public TextMeshProUGUI hullIntegrityText;


    void Start()
    {

        O2Amount = maxO2Capacity;
        fuelAmount = maxFuelCapacity;

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
        ThrusterSound();
        O2Amount -= O2DrainRate * Time.deltaTime;
    }

    void MovePlayer()
    {
        _forces = new Vector2(Input.GetAxisRaw("Horizontal") * thrustForce * Time.deltaTime, Input.GetAxisRaw("Vertical") * thrustForce * Time.deltaTime);
        if (fuelAmount > 0 && O2Amount > 0)
        {
            fuelAmount -= _forces.magnitude * Time.deltaTime;
            O2Amount -= _forces.magnitude * Time.deltaTime / 4;
            _rb.AddForce(_forces);
            
        }
        
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
            fuelAmount -= _forces.magnitude / 50;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Rock"))
        {
            hullHealth -= (_rb.velocity - collision.collider.GetComponent<Rigidbody2D>().velocity).magnitude * 4; 
            Debug.Log(hullHealth);

        }
        if (collision.collider.CompareTag("Ship"))
        {
            hullHealth -= _rb.velocity.magnitude;
            Debug.Log(hullHealth);

        }
        hullIntegrityText.text = "HULL INTEGRITY: " + Mathf.RoundToInt(hullHealth).ToString();


    }

    private void ThrusterSound()
    {
        if (O2Amount > 0)
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("THRUST_POWER", _forces.magnitude);
            if (fuelAmount > 0)
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("THRUST_TYPE", "NORMAL");
            else
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("THRUST_TYPE", "O2HISS");
        }
        else
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("THRUST_POWER", 0f);

        // print(_forces.magnitude); //  for sound debuging
    }
}
