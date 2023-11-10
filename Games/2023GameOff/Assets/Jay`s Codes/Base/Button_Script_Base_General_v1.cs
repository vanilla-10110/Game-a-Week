using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button_Script_Base_General_v1 : MonoBehaviour
{
    //private GameObject Base_Sprite;
    [Header("Put the respektive panels here")]
    public GameObject RD_Screen;
    public GameObject Hangar_Screen;
    public GameObject Fuel_Management_Screen;

    

    private void Start()
    {
        
        RD_Screen.SetActive(false);
        Hangar_Screen.SetActive(false);
        Fuel_Management_Screen.SetActive(false);
    }


    public void Screen_Manager_RD()
    {
        RD_Screen.SetActive(true);
    }

    public void Screen_Manager_Hangar()
    {
        Hangar_Screen.SetActive(true);
    }

    public void Screen_Manager_Fuel()
    {
        Fuel_Management_Screen.SetActive(true);
    }

    public void Screen_Manager_Return()
    {
        RD_Screen.SetActive(false);
        Hangar_Screen.SetActive(false);
        Fuel_Management_Screen.SetActive(false);
    }

    public void Screen_manager_Launch_to_Asteroid_Field()
    {
        SceneManager.LoadScene("Game");
        //SceneManager.UnloadScene("Base");
    }

    //This Script is property of the Glorius USSR(1945)//
}
