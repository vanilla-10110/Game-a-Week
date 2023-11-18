using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour {
    [SerializeField] private EventReference popOnSound;
    [SerializeField] private EventReference popOffSound;
    
    private GameObject _popupUI;
    private Animator _popupUIAnimator;
    private Collider2D _collider;
    private bool _uiToggledOn = false;

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

            if (!GameManager.instance.eventSystem.IsPointerOverGameObject()) {
                if (_collider.OverlapPoint(mouseWorldPosition)) {
                    TogglePopup();
                }
                else {
                    HidePopup();
                }
            }
        }
    }

    public void TogglePopup() {
        if (_uiToggledOn) {
            HidePopup();
        }
        else {
            ShowPopup();
        }
    }

    public void ShowPopup() {
        _popupUIAnimator.SetTrigger("On");
        FMODUnity.RuntimeManager.PlayOneShot(popOnSound);
        _uiToggledOn = true;
    }
    
    public void HidePopup() {
        _popupUIAnimator.SetTrigger("Off");
        FMODUnity.RuntimeManager.PlayOneShot(popOffSound);
        _uiToggledOn = false;
    }

    private void OnCameraTargetChange(CameraTarget oldTarget, CameraTarget newTarget) {
        if (newTarget.id != "BASE" && _uiToggledOn) {
            HidePopup();
        }
    }
}