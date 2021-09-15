using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    [SerializeField] private GameObject _clickPanel;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private Audio _audio = new Audio();

    private void Awake()
    {
        _audio.SourceSFX = gameObject.AddComponent<AudioSource>();
        _audio.SourceMusic = gameObject.AddComponent<AudioSource>();

        _audio.PlayMusic();
    }

    private void Start()
    {
        SetMusicVolume(AudioDataStore.Instance.MusicVolume);
        SetSoundVolume(AudioDataStore.Instance.SfxVolume);

        UpdateOptions();

        MoveCamera();        
    }

    public void UpdateOptions()
    {
        _musicSlider.value = _audio.MusicVolume;
        _soundSlider.value = _audio.SfxVolume;
    }

    private void MoveCamera()
    {
        _camera.DOMove(new Vector3(0.16f, 1.8f, -1.4f), 5f).SetLoops(-1, LoopType.Yoyo);
        _camera.DOLocalRotate(new Vector3(12, -10, 0), 5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void PlayClickSound()
    {
        _audio.PlaySound("Click");
    }

    public void SetMusicVolume(float volume)
    {
        _audio.MusicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        _audio.SfxVolume = volume;
    }

    public void ShowWindow(GameObject window)
    {
        _audio.PlaySound("NotepadSheet");
        window.SetActive(true);
        _clickPanel.SetActive(false);        
        window.GetComponentInChildren<Animator>().SetTrigger("Open");
    }

    public void HideWindow(GameObject window)
    {
        _clickPanel.SetActive(true);        
        window.SetActive(false);
    }

    public void StartGame()
    {
        _camera.DOKill(true);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
