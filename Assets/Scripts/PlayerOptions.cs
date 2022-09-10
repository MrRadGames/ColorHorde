using UnityEngine;
using TMPro;
using Assets.Classes;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI onButton;
    [SerializeField] TextMeshProUGUI offButton;

    [SerializeField] Slider slider;

    bool startingMusicOn = false;
    bool musicOn = false;
    float startingVolume = 0f;
    float volume = 0f;

    MusicManager musicManager;
    GameColor gameColor = new GameColor();

    SceneLoader sceneLoader;


    private void Start() {
        musicOn = PreferencesController.GetMusicOn();
        startingMusicOn = musicOn;
        if(onButton != null && offButton != null) {
            SetMusicButtonColors();
        }

        volume = PreferencesController.GetMasterVolume();
        startingVolume = volume;
        slider.value = startingVolume;
        musicManager = MusicManager.instance;
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void Update() {
        if(Mathf.Abs(volume-slider.value) > Mathf.Epsilon) {
            musicManager.SetVolume(slider.value);
            volume = slider.value;
            PreferencesController.SetMasterVolume(volume);
        }
        
    }

    public void TurnMusicOff() {
        musicOn = false;
        PreferencesController.SetMusicOn(false);
        SetMusicButtonColors();
        musicManager.TurnMusicOff();
    }

    public void TurnMusicOn() {
        musicOn = true;
        PreferencesController.SetMusicOn(true);
        SetMusicButtonColors();
        musicManager.TurnMusicOn();
    }

    private void SetMusicButtonColors() {
        onButton.color = musicOn ? gameColor.GetBlue() : new Color(1, 1, 1);
        offButton.color = musicOn ? new Color(1, 1, 1) : gameColor.GetBlue();
    }

    public void SaveOptions() {
        sceneLoader.LoadMainMenu();
    }

    public void BackPressed() {
        if (startingMusicOn && !musicOn) { TurnMusicOn(); } else { TurnMusicOff(); }
        musicManager.SetVolume(startingVolume);
        PreferencesController.SetMasterVolume(startingVolume);
        sceneLoader.LoadMainMenu();
    }
}
