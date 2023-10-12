using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;


    public Animator cameraAnimator;
    
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;


    [Header("Ground Check")]

    public float playerHeight;
    public LayerMask Ground;
    public bool isGrounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public Camera mainCamera;

    [SerializeField] LayerMask layerMask;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        

    }
    private void FixedUpdate()
    {
        
        MovePlayer();
        
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            cameraAnimator.SetTrigger("run");
        }
        else
        {
            cameraAnimator.SetTrigger("idle");
        }

        if (isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!isGrounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);

        if (flatVelocity.magnitude > moveSpeed )
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, Ground);
        MyInput();
        SpeedControl();
        if (isGrounded) rb.drag = groundDrag;
        else rb.drag = 0;


        // Create a ray from the main camera
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Perform a raycast against all objects within layer 8 (which is the layer for "Everything")
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 8))
        {
            //Debug.Log("wow");
        }


        int i = 0;
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 30);
        foreach (Collider thing in colliders)
        {

            if (thing.CompareTag("enemy"))
            {
                i++;
                
            }
        }
        if (i > 0)  FMODUnity.RuntimeManager.PlayOneShot("event:/MUSIC/MUSIC_COMBAT_START");
        else FMODUnity.RuntimeManager.PlayOneShot("event:/MUSIC/MUSIC_COMBAT_END");

    }
}
