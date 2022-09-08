using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    
    [SerializeField] int timeToWait = 1;

    int splashScreenDuration = 3;
    int currentSceneIndex;

    [SerializeField]
    public Animator transition;

    void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0) {
            StartCoroutine(WaitForSplash(splashScreenDuration));
        }
    }

    IEnumerator WaitForSplash(int seconds) {
        yield return new WaitForSeconds(seconds);
        LoadMainMenu();
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    IEnumerator LoadSceneWithAnimation(string sceneName) {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSplashScreen() {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadSceneWithAnimation("MainMenu"));
    }

    public void LoadOptionsScreen() {
        StartCoroutine(LoadSceneWithAnimation("Options"));
    }

    public void LoadInstructionsScreen() {
        StartCoroutine(LoadSceneWithAnimation("Instructions"));
    }

    public void LoadGameScene() {
        StartCoroutine(LoadSceneWithAnimation("GameScene"));
    }

    public void QuitGame() {
        Application.Quit();
    }
}
