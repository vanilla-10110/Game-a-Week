using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Zone : MonoBehaviour {
    public List<Collider2D> objectsInZone = new List<Collider2D>();
    
    private Collider2D _collider;
    
    private void Awake() {
        _collider = GetComponent<Collider2D>();

        if (!_collider.isTrigger) {
            Debug.LogError($"The collider the zone {name} is not set to be a trigger, the zone logic will not function properly.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        objectsInZone.Add(other);
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        objectsInZone.Remove(other);
    }
}