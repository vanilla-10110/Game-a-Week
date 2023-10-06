using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {
        base.Interact();
        Debug.Log("You got it!");
    }


}
