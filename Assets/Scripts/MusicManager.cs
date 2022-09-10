using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] AudioClip[] songs;
    
    AudioSource audioSource;

    int soundtrackIndex = 1;

    private void Awake() {
        if (MusicManager.instance == null) {
            DontDestroyOnLoad(this);
            MusicManager.instance = this;
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = PreferencesController.GetMasterVolume();
            if (PreferencesController.GetMusicOn()) {
                PlaySong();
            }
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        

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
        soundtrackIndex = soundtrackIndex == songs.Length - 1 ? 0 : soundtrackIndex + 1;
    }

    public void TurnMusicOn() {
        PlaySong();
    }

}
