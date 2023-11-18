using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour { 
    private GameObject _popupUI;
    private Animator _popupUIAnimator;
    private Collider2D _collider;

    private void Awake() {
        _popupUI = transform.Find("BuildingUI").gameObject;
        _popupUI.SetActive(true);
        
        _popupUIAnimator = _popupUI.GetComponent<Animator>();
        
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
        _popupUIAnimator.SetTrigger("Toggle");
    }

    public void ShowPopup() {
        _popupUIAnimator.SetTrigger("On");
    }
    
    public void HidePopup() {
        _popupUIAnimator.SetTrigger("Off");
    }

    private void OnCameraTargetChange(CameraTarget oldTarget, CameraTarget newTarget) {
        if (newTarget.id != "BASE") {
            HidePopup();
        }
    }
}