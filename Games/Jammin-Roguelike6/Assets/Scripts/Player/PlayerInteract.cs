using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerInteract : MonoBehaviour
{
    public Camera _mainCamera;
    public float distance = 3f;

    public TextMeshProUGUI e; 

    public LayerMask layerMask;

    public InputAction interact;
    void Start()
    {
        //_interact = GetComponent<InputAction>();
    }

    
    void Update()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null) { 
            
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                //Debug.Log(interactable.promptMessage);
                e.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.BaseInteract();
                }

                
            }
        } else e.gameObject.SetActive(false);

    }
}
