using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _UIManager : MonoBehaviour
{
    private readonly _InputManager input;

    /*
    // ! Settings uniques 
    
    public static bool settings = false;
    public Button mainMenuButton;

    private void Start() {
        if (this.mainMenuButton) {
            this.mainMenuButton.gameObject.SetActive(SceneManager.GetActiveScene().name != "MainMenu");
        }
        
    }

    private void LateUpdate() {
        if (this.input.escape) {
            if (settings) this.CloseSettings();
            else if(!settings) this.OpenSettings(); 
        }
    }

    public void CloseSettings() {
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync("Pause");
        settings = !settings;
    }

    public void OpenSettings() {
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == "Pause") {
                return;
            }
        }
        SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive);
        Time.timeScale = 0.0f;
        settings = !settings;
    }
    
    public void ShowCredits() {
        SceneManager.LoadSceneAsync("Credits", LoadSceneMode.Additive);
    }

    public void HideCredits() {
        SceneManager.UnloadSceneAsync("Credits");
    }
    */

    public void ChangeScene(string sceneName) {
        Time.timeScale = 1.0f;
        //if(settings) settings = !settings;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
