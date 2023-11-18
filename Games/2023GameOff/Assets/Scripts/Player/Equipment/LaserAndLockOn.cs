using UnityEngine;

public class LaserAndLockOn : MonoBehaviour
{
    private GameObject _lockOnCrosshair;
    
    private Vector3 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private bool _isLocked = false;

}
