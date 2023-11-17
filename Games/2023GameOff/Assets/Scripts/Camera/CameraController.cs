using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnMove(Vector3 oldPosition, Vector3 newPosition);

public class CameraController : MonoBehaviour {
    public event OnMove MoveEvent;
    
    public List<CameraTarget> targets = new List<CameraTarget>();

    [SerializeField] private new Camera camera;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float zoomSpeed;
    
    private void Awake() {
        CameraTarget currentTarget = GetCurrentTarget();

        if (currentTarget != null) {
            camera.orthographicSize = currentTarget.orthographicSize;
        }
    }
    
    private void Update() {
        CameraTarget currentTarget = GetCurrentTarget();

        Vector3 myPosition = transform.position;
        Vector3 targetPosition = currentTarget.transform.position;

        float movementDistance = Vector3.Distance(targetPosition, myPosition);
        if (movementDistance > 0.0001f) {
            Vector3 movementDirection = (targetPosition - myPosition).normalized;
            
            transform.position += movementSpeed * Time.deltaTime * Mathf.Sqrt(movementDistance) * movementDirection;
        }

        float orthographicSizeDifference = currentTarget.orthographicSize - camera.orthographicSize;
        float orthographicSizeDistance = Mathf.Abs(orthographicSizeDifference);
        if (orthographicSizeDistance > 0.0001f) {
            float orthographicSizeDirection = orthographicSizeDifference / orthographicSizeDistance;
            
            camera.orthographicSize += zoomSpeed * Time.deltaTime * Mathf.Sqrt(orthographicSizeDistance) * orthographicSizeDirection;
        }
        
        MoveEvent?.Invoke(myPosition, transform.position);
    }
    
    public CameraTarget AddTarget(CameraTarget target) {
        targets.Add(target);
        return target;
    }

    public CameraTarget AddTarget(string id, int priority, Transform transform, float orthographicSize) {
        CameraTarget target = new CameraTarget(id, priority, transform, orthographicSize);
        return AddTarget(target);
    }
    
    public void RemoveTarget(CameraTarget target) {
        targets.Remove(target);
    }

    public void RemoveTarget(string id) {
        CameraTarget target = targets.FirstOrDefault(target => id == target.id);

        if (target != null) {
            RemoveTarget(target);
        }
    }

    public CameraTarget GetCurrentTarget() {
        return targets.Aggregate((target1, target2) => target1.priority > target2.priority ? target1 : target2);
    }
}