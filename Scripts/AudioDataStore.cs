using UnityEngine;

public class AudioDataStore : MonoBehaviour
{
    private static AudioDataStore _instance;

    [SerializeField] private float _sfxVolume = 0.75f;
    [SerializeField] private float _musicVolume = 0.75f;

    public static AudioDataStore Instance
    {
        get
        {
            return _instance;
        }
    }

    public float SfxVolume { get => _sfxVolume; set => _sfxVolume = value; }
    public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this) Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SaveSoundVolume(float soundValue)
    {
        _sfxVolume = soundValue;      
    }

    public void SaveMusicVolume(float musicValue)
    { 
        _musicVolume = musicValue;
    }
}


