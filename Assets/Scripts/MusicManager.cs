using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;

    [SerializeField] AudioClip[] songs;
    
    AudioSource audioSource;

    int soundtrackIndex = 1;

    void Start() {
        if(MusicManager.instance == null) {
            DontDestroyOnLoad(this);
            MusicManager.instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PreferencesController.GetMasterVolume();
            PlaySong();
        } else {
            Destroy(gameObject);
        }

    }

    private void Update() {
        if(!audioSource.isPlaying && PreferencesController.GetMusicOn()) {
            soundtrackIndex = soundtrackIndex == songs.Length - 1 ? 0 : soundtrackIndex + 1;
            PlaySong();
        }
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }

    public void PlaySong() {
        if(songs == null || songs.Length == 0) { return; }
        audioSource.Stop();
        audioSource.clip = songs[soundtrackIndex];
        audioSource.Play();
    }

    public void TurnMusicOff() {
        audioSource.Stop();
    }

    public void TurnMusicOn() {
        PlaySong();
    }

}
