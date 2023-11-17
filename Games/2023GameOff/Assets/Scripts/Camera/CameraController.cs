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
    
    private void Update() {
        CameraTarget currentTarget = GetCurrentTarget();

        Vector3 oldPosition = transform.position;
        
        transform.position = Vector3.Lerp(transform.position, currentTarget.transform.position, movementSpeed * Time.deltaTime);
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, currentTarget.orthographicSize, zoomSpeed * Time.deltaTime);
        
        MoveEvent?.Invoke(oldPosition, transform.position);
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