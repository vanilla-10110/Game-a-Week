using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public GameObject explosion;

    HitReg hitReg;
    public float radius = 5.0F;
    public float power = 10.0F;
    public float explodeDamage = 10;

    public FMOD.Studio.EventInstance instance;

    private void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance("event:/ENVIRONMENT/FB");
        instance.start();

    }
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
            if (hit.CompareTag("enemy"))
            {
                 if (GameObject.Find("skeleton") != null) hitReg = GameObject.Find("skeleton").GetComponent<HitReg>();

                 hitReg.enemyHealth -= Mathf.RoundToInt(explodeDamage);
                 Debug.LogAssertion(hitReg.enemyHealth);
                 hitReg.DieQuestionMark();
            }
        }
        //GetComponent<Collider>().enabled = false;
        Destroy(explosion, 2f);

        //sfx
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();



        explosion.transform.parent = null;
        if (transform.Find("fireSFX") != null)
        {
            Transform fire = transform.Find("fireSFX");
            if (fire.GetComponent<ParticleSystem>() != null)
            {
                ParticleSystem.EmissionModule emission = fire.GetComponent<ParticleSystem>().emission;
                emission.enabled = false;
                fire.parent = null;
                Destroy(fire.gameObject, 5f);
            }
        }
        
            

        
        

        Destroy(gameObject, 0.1f);
        //explosionGO.GetComponent<Explosion>().Explode();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Detonate();
    }
}
