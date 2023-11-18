using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour { 
    private GameObject _popupUI;
    private Collider2D _collider;

    private void Awake() {
        _popupUI = transform.Find("BuildingUI").gameObject;
        
        _collider = GetComponent<Collider2D>();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mouseScreenPosition = Input.mousePosition;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            if (_collider.OverlapPoint(mouseWorldPosition)) {
                TogglePopup();
            }
        }
    }

    public void TogglePopup() {
        _popupUI.gameObject.SetActive(!_popupUI.gameObject.activeSelf);
    }

    public void ShowPopup() {
        _popupUI.gameObject.SetActive(true);
    }
    
    public void HidePopup() {
        _popupUI.gameObject.SetActive(false);
    }
}