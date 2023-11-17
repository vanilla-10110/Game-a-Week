using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Vector3 _mousePosition;

    private void Update()
    {
        //cursor goes to the z layer of the screen so the cam doesnt pick it up
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePosition + new Vector3(0, 0, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Rock")) Debug.LogWarning("jeb issue right here");
    }
}
