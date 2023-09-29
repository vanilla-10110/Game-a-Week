using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitreg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("macdonals"); 
        if (collision.collider.tag == "sword") Debug.Log("we went crazy");
    }
}
