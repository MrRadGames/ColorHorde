using UnityEngine;

public class PreferencesController : MonoBehaviour
{
    // Keys
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MUSIC_ON_KEY = "music_on";

    // Constraints
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    const float DEFAULT_VOLUME = 0.5f;

    public static void SetMasterVolume(float volume) {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME) {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else {
            Debug.LogError("Master volume is out of range with value of: " + volume.ToString());
        }
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, DEFAULT_VOLUME);
    }

    public static void SetMusicOn(bool musicOn) {
        PlayerPrefs.SetInt(MUSIC_ON_KEY, musicOn ? 1 : 0);
    }

    public static bool GetMusicOn() {
        return PlayerPrefs.GetInt(MUSIC_ON_KEY, 1) == 0 ? false : true;
    }
}
