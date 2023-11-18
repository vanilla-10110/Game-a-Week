using System.Collections.Generic;
using UnityEngine;

public class ChildrenStateManager : MonoBehaviour {
    [SerializeField] private string currentState;
    
    private Dictionary<string, GameObject> children;

    private void Awake() {
        children = new Dictionary<string, GameObject>();

        for (int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            
            children.Add(child.name, child);
        }
        
        SetState(currentState);
    }

    public GameObject GetCurrentStateChild() {
        if (children.ContainsKey(currentState)) {
            return children[currentState];
        }

        return null;
    }

    public void SetState(string value) {
        if (value == currentState) {
            return;
        }
        
        GameObject currentStateGameObject = GetCurrentStateChild();

        if (currentStateGameObject != null) {
            currentStateGameObject.SetActive(false);
        }

        currentState = value;
        currentStateGameObject = GetCurrentStateChild();

        if (currentStateGameObject != null) {
            currentStateGameObject.SetActive(true);
        }
    }
}
