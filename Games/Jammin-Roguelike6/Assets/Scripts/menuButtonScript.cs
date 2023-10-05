using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void Commence()
    {
        SceneManager.LoadScene("Playground");
    }

    public void Quit()
    {
        Application.Quit();       
    }
}
