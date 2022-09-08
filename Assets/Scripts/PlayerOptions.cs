using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Classes;

public class PlayerOptions : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI onButton;
    [SerializeField] TextMeshProUGUI offButton;

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
        musicManager = FindObjectOfType<MusicManager>();
        sceneLoader = FindObjectOfType<SceneLoader>();
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
        if (startingMusicOn) { TurnMusicOn(); } else { TurnMusicOff(); }
        PreferencesController.SetMasterVolume(startingVolume);
        sceneLoader.LoadMainMenu();
    }
}
