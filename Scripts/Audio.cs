using System;
using UnityEngine;

[Serializable]
public class Audio
{
    [SerializeField] private float _sfxVolume = 1f;
    [SerializeField] private float _musicVolume = 1f;

    [SerializeField] private AudioClip[] _sounds;
    [SerializeField] private AudioClip _defaultClip;
    [SerializeField] private AudioClip _gameMusic;

    private AudioSource _sourceSFX;
    private AudioSource _sourceMusic;

    public AudioSource SourceSFX { get => _sourceSFX; set => _sourceSFX = value; }
    public AudioSource SourceMusic { get => _sourceMusic; set => _sourceMusic = value; }

    public float SfxVolume
    {
        get
        {
            return _sfxVolume;
        }
        set
        {
            _sfxVolume = value;
            SourceSFX.volume = _sfxVolume;

            AudioDataStore.Instance.SaveSoundVolume(value);
        }
    }


    public float MusicVolume
    {
        get
        {
            return _musicVolume;
        }
        set
        {
            _musicVolume = value;
            SourceMusic.volume = _musicVolume;

            AudioDataStore.Instance.SaveMusicVolume(value);
        }
    }

    public void PlaySound(string clipName)
    {
        SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }

    public void PlayMusic()
    {        
        SourceMusic.clip = _gameMusic;
        SourceMusic.volume = MusicVolume;
        SourceMusic.loop = true;
        SourceMusic.Play();
    }

    private AudioClip GetSound(string clipName)
    {
        for (var i = 0; i < _sounds.Length; i++)
            if (_sounds[i].name == clipName) return _sounds[i];

        Debug.LogError("Can not find clip " + clipName);
        return _defaultClip;
    }
}


