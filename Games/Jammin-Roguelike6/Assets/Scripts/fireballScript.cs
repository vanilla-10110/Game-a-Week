using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public ParticleSystem explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Detonate()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Transform fire = transform.Find("fireSFX");
        //fire.GetComponent<ParticleSystem>().Pause();

        var emission = fire.GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        fire.parent = null;
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        Detonate();

    }
}
