using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";

    [Header("UI Panels")]
    public GameObject settingsPanel;

    [Header("Audio")]
    public AudioMixer mainMixer;
    public Slider musicSlider, sfxSlider;

    [Header("Text & Gameplay")]
    public Slider textSpeedSlider;
    public Slider fontSizeSlider;
    public Toggle autoForwardToggle;

    private void Start()
    {
        LoadAudioSetting("MusicVol", musicSlider, "MusicVolume");
        LoadAudioSetting("SFXVol", sfxSlider, "SFXVolume");

        if (textSpeedSlider != null)
        {
            textSpeedSlider.value = PlayerPrefs.GetFloat("TextSpeed", 30f);
            textSpeedSlider.onValueChanged.AddListener(SetTextSpeed);
        }
            
        if (fontSizeSlider != null)
        {
            fontSizeSlider.value = PlayerPrefs.GetInt("FontSize", 24);
            fontSizeSlider.onValueChanged.AddListener(SetFontSize);
        }

        if (autoForwardToggle != null)
        {
            autoForwardToggle.isOn = PlayerPrefs.GetInt("AutoForward", 0) == 1;
            autoForwardToggle.onValueChanged.AddListener(SetAutoForward);
        }
            
    }

    private void LoadAudioSetting(string exposedParam, Slider slider, string prefsKey)
    {
        float vol = PlayerPrefs.GetFloat(prefsKey, 0.75f);
        slider.value = vol;
        SetGroupVolume(exposedParam, vol);
    }

    public void SetMusicVolume(float value) => SetAudio("MusicVol", "MusicVolume", value);
    public void SetSFXVolume(float value) => SetAudio("SFXVol", "SFXVolume", value);

    private void SetAudio(string exposedParam, string prefsKey, float value)
    {
        SetGroupVolume(exposedParam, value);
        PlayerPrefs.SetFloat(prefsKey, value);
    }

    private void SetGroupVolume(string parameter, float value)
    {
        float dB = value > 0 ? Mathf.Log10(value) * 20 : -80f;
        mainMixer.SetFloat(parameter, dB);
    }

    public void SetTextSpeed(float speed)
    {
        PlayerPrefs.SetInt("TextSpeed", Mathf.RoundToInt(speed));
        PlayerPrefs.Save();
    }

    public void SetFontSize(float size)
    {
        PlayerPrefs.SetInt("FontSize", Mathf.RoundToInt(size));
        PlayerPrefs.Save();
    }

    public void SetAutoForward(bool isOn)
    {
        PlayerPrefs.SetInt("AutoForward", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ContinueGame()
    {
        // TODO: Реалізувати збереження та завантаження прогресу
    }

    public void OpenSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
