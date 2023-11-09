using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class General_Base_Upgrades : MonoBehaviour
{
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;
    public GameObject Upgrade_Material;
    public int Upgrade_Cost = 3;

    private int Input_Upgrade_Material = 2;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Level1.SetActive(true);
        Level2.SetActive(false);
        Level3.SetActive(false);
        Level4.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Debug.Log("E has been pressed while player was in trigger");
            Input_Upgrade_Material++;
            if(Input_Upgrade_Material >= Upgrade_Cost)
            {
                Level2.SetActive(true);
                Level1.SetActive(false);
            }
        }
    }
}
