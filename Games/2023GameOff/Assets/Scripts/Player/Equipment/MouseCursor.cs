using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Vector3 _mousePosition;

    private void Update()
    {
        
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = _mousePosition + new Vector3(0, 0, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Rock")) Debug.LogWarning("jeb issue right here");
    }
}
