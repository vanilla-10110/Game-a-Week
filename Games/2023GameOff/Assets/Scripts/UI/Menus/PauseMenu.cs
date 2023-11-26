using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject menu;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        menu.SetActive(!menu.activeSelf);
        Time.timeScale = Time.timeScale > 0.0f ? 0.0f : 1.0f;
    }
}