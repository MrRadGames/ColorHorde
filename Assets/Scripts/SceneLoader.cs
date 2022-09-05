using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    
    [SerializeField] int timeToWait = 1;

    int splashScreenDuration = 3;
    int currentSceneIndex;

    void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0) {
            StartCoroutine(WaitForTime(splashScreenDuration));
        }
    }

    IEnumerator WaitForTime(int seconds) {
        yield return new WaitForSeconds(seconds);
        LoadNextScene();
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSplashScreen() {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptionsScreen() {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
