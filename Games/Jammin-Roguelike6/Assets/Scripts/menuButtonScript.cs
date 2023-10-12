using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtonScript : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject volumeSettings;


    public void Commence()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("DungeonGen");
    }

    public void OpenOptions()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        ReduceSound();

    }
    public void CloseOptions()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        volumeSettings.SetActive(false);
        IncreaseSound();
    }


    public void Quit()
    {
        Application.Quit();       
    }

    public void ResetHighscore()
    {
        PlayerPrefs.SetInt("highScore", 0);

    }

    
    public void OpenVolumeSettings()
    {
        volumeSettings.SetActive(true);
        settingsMenu.SetActive(false);
    }


    public void MusicVolume()
    {
        //music slider
    }
    public void SFXVolume()
    {
        //sfx slider
    }




    void ReduceSound()
    {
        //decrease music volume here
    }
    void IncreaseSound()
    {
        //increase music volume here
    }

   





}
