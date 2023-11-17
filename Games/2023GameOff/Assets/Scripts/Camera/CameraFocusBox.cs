using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraFocusBox : MonoBehaviour {
    [SerializeField] private new CameraController camera;
    [SerializeField] private new Transform target;
    [SerializeField] private float minimumMovementThreshold;
    [SerializeField] private float minimumTime;
    [SerializeField] private CameraTarget cameraTarget;

    private Collider2D _collider;
    private Vector3 _lastTargetPosition;
    private bool _cameraFocused;
    private float _enterTime = -1.0f;

    private void Awake() {
        _collider = GetComponent<Collider2D>();
        
        _lastTargetPosition = camera.transform.position;
    }

    private void OnDestroy() {
        OnDeactivateCameraFocus();
    }

    private void FixedUpdate() {
        float targetMovement = Vector3.Distance(target.position, _lastTargetPosition) / Time.deltaTime;
        
        if (_collider.OverlapPoint(target.position) && targetMovement <= minimumMovementThreshold) {
            if (_enterTime < 0.0f) {
                _enterTime = Time.time;
            }
            
            if (Time.time - _enterTime >= minimumTime && !_cameraFocused) {
                OnActivateCameraFocus();

                _cameraFocused = true;
            }
        }
        else {
            _enterTime = -1.0f;
            
            if (_cameraFocused) {
                OnDeactivateCameraFocus();

                _cameraFocused = false;
            }
        }
        
        _lastTargetPosition = target.position;
    }

    private void OnActivateCameraFocus() {
        camera.AddTarget(cameraTarget);
    }
    
    private void OnDeactivateCameraFocus() {
        camera.RemoveTarget(cameraTarget.id);
    }
}
