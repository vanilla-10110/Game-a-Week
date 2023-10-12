using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    
    public GameObject pauseMenuUI;
    public GameObject canvas;
    public GameObject deathCam;
    public GameObject playerCam;

    private void Update()
    {

        deathCam.transform.position = playerCam.transform.position;
        deathCam.transform.rotation = playerCam.transform.rotation;




        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
                ReduceSound();
            }
            else
            {
                Pause();
                IncreaseSound();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        canvas.SetActive(true);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        canvas.SetActive(false);
        Time.timeScale = 0f;
        gameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void ReduceSound()
    {
        //reduce sound here
    }
    void IncreaseSound()
    {
        //make it normal again here
    }



    public void LoadMenu()
    {
        SceneManager.LoadScene("TitleAndMenus");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
