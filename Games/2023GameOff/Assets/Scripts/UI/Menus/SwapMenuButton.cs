using UnityEngine;

public class SwapMenuButton : MonoBehaviour {
    [SerializeField] private GameObject[] onMenus;
    [SerializeField] private GameObject[] offMenus;

    public void Swap() {
        foreach (GameObject onMenu in onMenus) {
            onMenu.SetActive(true);
        }
        
        foreach (GameObject offMenu in offMenus) {
            offMenu.SetActive(false);
        }
    }
}