using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuButtonScript : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
    public GameObject volumeSettings;

    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;
    float volume;

    FMOD.Studio.Bus musicBus;
    FMOD.Studio.Bus SFXBus;


    private void Start()
    {
        musicBus = FMODUnity.RuntimeManager.GetBus("bus:/MUSIC");
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }

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
        volume = Mathf.Pow(10.0f, musicVolumeSlider.value / 20f);
        musicBus.setVolume(volume);
    }
    public void SFXVolume()
    {
        volume = Mathf.Pow(10.0f, SFXVolumeSlider.value / 20f);
        SFXBus.setVolume(volume);
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
