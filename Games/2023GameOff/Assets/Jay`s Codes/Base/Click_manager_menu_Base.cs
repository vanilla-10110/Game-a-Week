using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_manager_menu_Base : MonoBehaviour
{
    public GameObject Options_Pause_Screen;
    public GameObject Options_Sound_Screen;
    public GameObject Options_Controll_Screen;

    public GameObject Menu_Pause_Screen;
    private int Esc_int = 0;
    void Start()
    {
        Menu_Pause_Screen.SetActive(false);
        Options_Pause_Screen.SetActive(false);
        Options_Sound_Screen.SetActive(false);
        Options_Controll_Screen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu_Pause_Screen.SetActive(true);
            Debug.Log("ESC Pressed");
            Esc_int++;

            if (Esc_int > 1)
            {
                Menu_Pause_Screen.SetActive(false);
                Esc_int = 0;
            }
        }
    }

    public void on_click_Pause_unHide()
    {
        Options_Pause_Screen.SetActive(true);
    }

    public void on_click_Sound_unHide()
    {
        Options_Sound_Screen.SetActive(true);
    }

    public void on_click_Controll_unHide()
    {
        Options_Controll_Screen.SetActive(true);
    }

    public void on_click_Unpause()
    {
        Esc_int = 0;
        Options_Pause_Screen.SetActive(false);
    }

    public void on_click_Options_Generally_Hide()
    {
        Options_Sound_Screen.SetActive(false);
        Options_Controll_Screen.SetActive(false);
    }
}
