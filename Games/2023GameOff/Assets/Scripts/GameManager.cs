using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public EventSystem eventSystem;
    
    private void Awake() {
        if (instance != null) {
            Destroy(instance.gameObject);
        }

        instance = this;
    }
}