using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider slider;
    GameObject player;
    public GameObject deathScreen;
    public GameObject UI;
    public GameObject deathCam;
    private void Awake()
    {
        slider.value = 100f;
        player = GameObject.Find("Platyer");
    }


    
    private void Update()
    {
        if (slider.value <= 0)
        {
            Die();
        }
    }
    public void TakeDamage()
    {
       
        slider.value -= 5f;
    }

    void Die()
    {
        UI.SetActive(false);
        player.SetActive(false);
        deathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        deathCam.SetActive(true);

        FMODUnity.RuntimeManager.PlayOneShot("event:/MUSIC/MUSIC_STINGER_DEATH");






    }


}
