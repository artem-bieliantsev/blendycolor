using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hud : MonoBehaviour
{
    private static Hud _instance;

    [SerializeField] private PhysicsRaycaster _raycaster;

    [SerializeField] private GameObject _levelVictoryWindow;
    [SerializeField] private GameObject _levelFailWindow;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;

    [SerializeField] private GameObject _cloud;
    [SerializeField] private Image _request;
    [SerializeField] private Text _percentValue;

    [SerializeField] private GameObject _mixButton;

    public static Hud Instance
    {
        get
        {           
            return _instance;
        }
    }

    public GameObject MixButton { get => _mixButton; set => _mixButton = value; }
    public GameObject Cloud { get => _cloud; set => _cloud = value; }

    private void Awake()
    {
        _instance = this;

        _raycaster.enabled = false;                
    }

    public void LoadAudioSettings()
    {
        SetMusicVolume(AudioDataStore.Instance.MusicVolume);
        SetSoundVolume(AudioDataStore.Instance.SfxVolume);

        UpdateOptions();
    }

    public void PlayClickSound()
    {
        Controller.Instance.AudioManager.PlaySound("Click");
    }    

    private void PlayNotepadSheetSound()
    {
        Controller.Instance.AudioManager.PlaySound("NotepadSheet");
    }

    public void SetMusicVolume(float volume)
    {
        Controller.Instance.AudioManager.MusicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        Controller.Instance.AudioManager.SfxVolume = volume;
    }

    public void UpdateOptions()
    {
        _musicSlider.value = Controller.Instance.AudioManager.MusicVolume;
        _soundSlider.value = Controller.Instance.AudioManager.SfxVolume;
    }

    public void ShowWindow(GameObject window)
    {
        window.SetActive(true);
        window.GetComponentInChildren<Animator>().SetTrigger("Open");

        PlayNotepadSheetSound();
    }

    public void HideWindow(GameObject window)
    {
        window.SetActive(false);
    } 

    public void ShowCloud()
    {
        _cloud.SetActive(true);
        _request.color = Controller.Instance.Level.ReferenceColor;
        _percentValue.text = "0%";
        _raycaster.enabled = true;
    }

    public void HideCloud()
    {
        _cloud.SetActive(false);
        _raycaster.enabled = false;
    }

    public void ShowVictoryWindow()
    {
        ShowWindow(_levelVictoryWindow);
    }

    public void ShowFailWindow()
    {
        ShowWindow(_levelFailWindow);
    }

    public void CountCompareValue()
    {
        StartCoroutine(Count(Controller.Instance.CompareValue, 0.02f));
    }

    private void LevelResults()
    {
        if (Controller.Instance.CompareValue >= 90)
        {
            ShowVictoryWindow();
        }
        else
        {
            ShowFailWindow();
        }
    }

    private IEnumerator Count(int to, float delay)
    {
        _raycaster.enabled = false;

        int count = 0;
        for (int i = 1; i <= to; i++)
        {

            yield return new WaitForSeconds(delay);
            count++;
            
            _percentValue.text = $"{count}%";           
        }

        yield return new WaitForSeconds(2f);

        LevelResults();

        _raycaster.enabled = true;
    }
}
