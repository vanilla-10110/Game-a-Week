using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour {
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}