using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour { 
    private GameObject _popupUI;
    private Collider2D _collider;

    private void Awake() {
        _popupUI = transform.Find("BuildingUI").gameObject;
        
        _collider = GetComponent<Collider2D>();
    }

    private void Start() {
        CameraController.main.SetTargetEvent += OnCameraTargetChange;
    }
    
    private void OnDestroy() {
        CameraController.main.SetTargetEvent -= OnCameraTargetChange;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && CameraController.main.GetCurrentTarget().id == "BASE") {
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

    private void OnCameraTargetChange(CameraTarget oldTarget, CameraTarget newTarget) {
        if (newTarget.id != "BASE") {
            HidePopup();
        }
    }
}