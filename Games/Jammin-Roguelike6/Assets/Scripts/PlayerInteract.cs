using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
public class PlayerInteract : MonoBehaviour
{
    public Camera _mainCamera;
    public float distance = 3f;

    public TextMeshProUGUI e; 

    public LayerMask layerMask;
    void Start()
    {
        
    }

    
    void Update()
    {
        Ray ray = new Ray(_mainCamera.transform.position, _mainCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, distance, layerMask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null) { 
            
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
                e.gameObject.SetActive(true);
            }
        } else e.gameObject.SetActive(false);

    }
}
