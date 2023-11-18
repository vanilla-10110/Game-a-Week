using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour { 
    [SerializeField] private GameObject popupUI;
    
    private Collider2D _collider;

    private void Awake() {
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
        popupUI.gameObject.SetActive(!popupUI.gameObject.activeSelf);
    }

    public void ShowPopup() {
        popupUI.gameObject.SetActive(true);
    }
    
    public void HidePopup() {
        popupUI.gameObject.SetActive(false);
    }
}