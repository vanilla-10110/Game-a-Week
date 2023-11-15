using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CameraFocusBox : MonoBehaviour {
    [SerializeField] private new CameraController camera;
    [SerializeField] private new Transform target;
    [SerializeField] private float minimumMovementThreshold;
    [SerializeField] private CameraTarget cameraTarget;

    private Collider2D _collider;
    private Vector3 _lastTargetPosition;
    private bool _cameraFocused;

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
            if (!_cameraFocused) {
                OnActivateCameraFocus();

                _cameraFocused = true;
            }
        }
        else {
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
