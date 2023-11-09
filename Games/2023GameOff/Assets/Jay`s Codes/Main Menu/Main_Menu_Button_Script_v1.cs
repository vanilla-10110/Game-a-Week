using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class Main_Menu_Button_Script_v1 : MonoBehaviour
{
   //public GameObject Base_Sprite;
    public GameObject Options_menu_Script;
    public GameObject Options_menu_Script_Sound;
    public GameObject Options_menu_Script_Controlls;

    

    private void Start()
    {
        SceneManager.UnloadScene("Base");
        SceneManager.UnloadScene("Game");
        Options_menu_Script.SetActive(false);
        Options_menu_Script_Sound.SetActive(false);
        Options_menu_Script_Controlls.SetActive(false);
    }

    public void On_Options_Click()
    {
        Options_menu_Script.SetActive(true);
    }

    public void On_Play_Click()
    {
        SceneManager.LoadScene("Base");
    }

    public void On_Quit_Click()
    {
        Application.Quit();
    }

    public void On_SoundOption_Click()
    {
        Options_menu_Script_Sound.SetActive(true);
    }

    public void On_ControllOption_Click()
    {
        Options_menu_Script_Controlls.SetActive(true);
       
    }

    public void Return_To_Options()
    {
        Options_menu_Script_Sound.SetActive(false);
        Options_menu_Script_Controlls.SetActive(false);
    }

    public void Return_To_menu()
    {
        Options_menu_Script.SetActive(false);
    }
}
