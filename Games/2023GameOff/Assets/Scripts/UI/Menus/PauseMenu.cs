using UnityEngine;

public class PauseMenu : MonoBehaviour {
    [SerializeField] private GameObject defaultOnMenu;
    [SerializeField] private GameObject defaultOffMenu;
    [SerializeField] private GameObject menu;
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        menu.SetActive(!menu.activeSelf);
        Time.timeScale = Time.timeScale > 0.0f ? 0.0f : 1.0f;

        if (menu.activeSelf) {
            defaultOnMenu.SetActive(true);
            defaultOffMenu.SetActive(false);
        }
    }
}