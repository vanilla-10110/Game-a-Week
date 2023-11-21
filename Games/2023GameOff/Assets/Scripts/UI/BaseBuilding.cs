using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class BaseBuilding : MonoBehaviour {
    public string _name;
    [TextArea] public string description;
    
    [SerializeField] private float cooldownLength = 0.35f;
    [SerializeField] private Transform player;
    [SerializeField] private Collider2D interactArea;

    private Collider2D _collider;
    private GameObject _popupUIGameObject;
    private BaseBuildingPopup _popupUI;
    private float _lastPopupTime = -Mathf.Infinity;
    private int _currentLevel = 0;

    private void Awake() {
        _popupUIGameObject = transform.Find("BuildingUI").gameObject;
        _popupUIGameObject.SetActive(true);
        
        _popupUI = _popupUIGameObject.GetComponent<BaseBuildingPopup>();
        
        _collider = GetComponent<Collider2D>();
        
        SetPopupInfo();
        SetLevel(0);
    }

    private void Start() {
        CameraController.main.SetTargetEvent += OnCameraTargetChange;
        _popupUI.upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }
    
    private void OnDestroy() {
        _popupUI.upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);
        CameraController.main.SetTargetEvent -= OnCameraTargetChange;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && CameraController.main.GetCurrentTarget().id == "BASE") {
            Vector2 mouseScreenPosition = Input.mousePosition;
            Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

            if (interactArea.OverlapPoint(player.position) && !GameManager.instance.eventSystem.IsPointerOverGameObject() && Time.time - _lastPopupTime > cooldownLength) {
                if (_collider.OverlapPoint(mouseWorldPosition)) {
                    _popupUI.TogglePopup();
                }
                else {
                    _popupUI.HidePopup();
                }
                
                _lastPopupTime = Time.time;
            }
        }
    }

    private void OnCameraTargetChange(CameraTarget oldTarget, CameraTarget newTarget) {
        if (newTarget.id != "BASE") {
            _popupUI.HidePopup();
        }
    }

    private void OnUpgradeButtonClick() {
        IncrementLevel();
    }

    public void SetLevel(int value) {
        for (int i = _currentLevel; i < value; i++) {
            _popupUI.levelIndicators[i].SetState("Unlocked");
        }
        
        _currentLevel = value;
    }

    public void IncrementLevel(int amount = 1) {
        SetLevel(_currentLevel + amount);
    }

    private void SetPopupInfo() {
        _popupUI._name.text = _name;
        _popupUI.description.text = description;
    }
}