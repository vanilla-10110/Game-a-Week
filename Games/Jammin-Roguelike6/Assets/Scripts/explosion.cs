using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float radius = 5.0F;
    public float power = 10.0F;
    // Start is called before the first frame update
    void Start()
    {
        
    }
  
    //private void OnCollisionStay(Collision collision)
    //{
    //    Vector3 explosionPos = transform.position;

    //    Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
    //    foreach (Collider hit in colliders)
    //    {

    //        Rigidbody rb = hit.GetComponent<Rigidbody>();
    //        if (rb != null)
    //            rb.AddExplosionForce(power, explosionPos, radius, 0F, ForceMode.Impulse);


    //    }
    //    GetComponent<Collider>().enabled = false;
    //    Destroy(gameObject, 10f);
        
    //}
    //private void OnDrawGizmos()
    //{
    //    Vector3 explosionPos = transform.position;
    //    Gizmos.DrawWireSphere(explosionPos, radius);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
