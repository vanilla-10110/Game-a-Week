using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public GameObject explosion;

    public float radius = 5.0F;
    public float power = 10.0F;
    public void Detonate()
    {
        //ParticleSystem explosionGO = Instantiate(explosion, transform.position, transform.rotation);

        explosion.GetComponent<ParticleSystem>().Play();
        Light light = gameObject.GetComponent<Light>(); 
        //light.color = Color.white;
        light.intensity = 10;
        light.range = 20;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 0F, ForceMode.Impulse);
        }
        //GetComponent<Collider>().enabled = false;
        Destroy(explosion, 2f);


        explosion.transform.parent = null;
        Transform fire = transform.Find("fireSFX");
        ParticleSystem.EmissionModule emission = fire.GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        fire.parent = null;

        Destroy(gameObject, 0.1f);
        //explosionGO.GetComponent<Explosion>().Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }
}
