using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour {
    [SerializeField] private float cooldownLength = 0.35f;
    
    private Collider2D _collider;
    private GameObject _popupUIGameObject;
    private Popup _popupUI;
    private float lastPopupTime = -Mathf.Infinity;

    private void Awake() {
        _popupUIGameObject = transform.Find("BuildingUI").gameObject;
        _popupUIGameObject.SetActive(true);
        
        _popupUI = _popupUIGameObject.GetComponent<Popup>();
        
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

            if (!GameManager.instance.eventSystem.IsPointerOverGameObject() && Time.time - lastPopupTime > cooldownLength) {
                if (_collider.OverlapPoint(mouseWorldPosition)) {
                    _popupUI.TogglePopup();
                }
                else {
                    _popupUI.HidePopup();
                }
                
                lastPopupTime = Time.time;
            }
        }
    }

    private void OnCameraTargetChange(CameraTarget oldTarget, CameraTarget newTarget) {
        if (newTarget.id != "BASE") {
            _popupUI.HidePopup();
        }
    }
}