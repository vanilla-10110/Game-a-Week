using TMPro;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    
    [Header("ship voice")]
    private FMOD.Studio.EventInstance shipvoice;
    private float lowO2;
    private bool O2warningMuted = false;


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

        shipvoice = FMODUnity.RuntimeManager.CreateInstance("event:/AI/AI_WARNING");
        lowO2 = 50f;
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
        SetVoiceParameters();
        PlayShipVoice();
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


    // SHIP'S VOICE
    private void SetVoiceParameters()
    {
        float O2Percent = (O2Amount / maxO2Capacity * 100f);
        float fuelPercent = (fuelAmount / maxFuelCapacity * 100f);
        float hullPercent = (hullHealth / 100f);
        float powerPercent = (gameObject.GetComponent<LaserShooter>().powerAmount / gameObject.GetComponent<LaserShooter>().powerMaxCapacity * 100f);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_O2", O2Percent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_FUEL", fuelPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_HULL", hullPercent);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("AI_POWER", powerPercent);
    }

    private void PlayShipVoice()
    {
        //define variables
        float O2Percent = (O2Amount / maxO2Capacity * 100f);
        float fuelPercent = (fuelAmount / maxFuelCapacity * 100f);
        float hullPercent = (hullHealth / 100f);
        float powerPercent = (gameObject.GetComponent<LaserShooter>().powerAmount / gameObject.GetComponent<LaserShooter>().powerMaxCapacity * 100f);

        //O2 Warning
        if (O2Percent < lowO2 & O2warningMuted == false) //check if o2 is low
        {
            if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "O2"); //set ship voice to play oxygen warning
                shipvoice.start(); //play warning sound

                //set new low o2 value
                if (O2Percent < 50f & O2Percent > 25f)
                {
                    lowO2 = 25f;
                }
                if (O2Percent < 25f & O2Percent > 15f)
                {
                    lowO2 = 15f;
                }
                if (O2Percent < 15f & O2Percent > 5f)
                {
                    lowO2 = 5f;
                }
                if (O2Percent < 5f)
                {
                    lowO2 = 0f;
                }
                if (O2Percent < 0f)
                {
                    O2warningMuted = true; //mute o2 warning after o2 reaches 0
                }
            }
        }

        //stuff to copy from
        /*if (PlaybackState(shipvoice) != FMOD.Studio.PLAYBACK_STATE.PLAYING) //check if sound is playing
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "POWER"); //set ship voice to play power warning
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "FUEL"); //set ship voice to play fuel warning
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "O2"); //set ship voice to play oxygen warning
            FMODUnity.RuntimeManager.StudioSystem.setParameterByNameWithLabel("WARNINGTYPE", "HULL"); //set ship voice to play hull integrity warning
            shipvoice.start(); //play warning sound
        }*/
    }

    FMOD.Studio.PLAYBACK_STATE PlaybackState(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE pS;
        instance.getPlaybackState(out pS);
        return pS;
    }

    
}
