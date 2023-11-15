using UnityEngine;

[System.Serializable]
public class CameraTarget {
    public string id;
    public int priority;
    public Transform transform;
    public float orthographicSize;

    public CameraTarget(string id, int priority, Transform transform, float orthographicSize) {
        this.id = id;
        this.priority = priority;
        this.transform = transform;
        this.orthographicSize = orthographicSize;
    }
}