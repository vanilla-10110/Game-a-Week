using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spellScript : MonoBehaviour
{
    [Header("Objects")]
    public Transform spellSpawnPoint;
    public Transform smokeSpawn;
    public ParticleSystem castSmoke;
    public GameObject[] spells;
    GameObject player;

    

    [Header("Values")]
    int spellIndex = 0;
    public float spellVelocity = 10f;
    public void ShootSpell()
    {
        player = GameObject.Find("Platyer");
        GameObject spawnedSpell = Instantiate(spells[spellIndex]);
        spawnedSpell.transform.position = spellSpawnPoint.position;
        //                                                                                          (                                           physics.realistic                                           )
        spawnedSpell.GetComponent<Rigidbody>().velocity = spellSpawnPoint.forward * spellVelocity/* + player.GetComponent<Rigidbody>().transform.forward * player.GetComponent<Rigidbody>().velocity.magnitude*/;
        Destroy(spawnedSpell, 5);
        ParticleSystem castSmoked = Instantiate(castSmoke, smokeSpawn.transform);
        castSmoke.Play();
        Destroy(castSmoked);
    }

}
