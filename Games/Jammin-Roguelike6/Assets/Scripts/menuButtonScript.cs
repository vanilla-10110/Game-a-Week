using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtonScript : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject mainMenu;
  


    public void Commence()
    {
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

    void ReduceSound()
    {
        //decrease music volume here
    }
    void IncreaseSound()
    {
        //increase music volume here
    }

}
