using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        
        Instantiate(explosion, transform.position, transform.rotation);
        Transform fire = transform.Find("fireSFX");
        //fire.GetComponent<ParticleSystem>().Pause();

        var emission = fire.GetComponent<ParticleSystem>().emission;
        emission.enabled = false;
        fire.parent = null;
        Destroy(gameObject);

    }
}
