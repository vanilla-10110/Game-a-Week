using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class spellScript : MonoBehaviour
{

    public Transform spellSpawnPoint;
    public GameObject[] spells;
    public ParticleSystem castSmoke;
    public Transform smokeSpawn;

    int spellIndex = 0;

    public float spellVelocity;
    public void ShootSpell()
    {
        GameObject spawnedSpell = Instantiate(spells[spellIndex]);
        spawnedSpell.transform.position = spellSpawnPoint.position;
        spawnedSpell.GetComponent<Rigidbody>().velocity = spellSpawnPoint.forward * spellVelocity;
        Destroy(spawnedSpell, 5);
        
        castSmoke.Play();
    }
}
