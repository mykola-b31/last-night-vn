using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject settingsUIPanel;

    [Header("Scene Management")]
    public string mainMenuSceneName = "MainMenu";

    [Header("Audio")]
    public AudioMixer mainMixer;
    public Slider musicSlider, sfxSlider;

    [Header("Text & Gameplay")]
    public Slider textSpeedSlider, fontSizeSlider;
    public Toggle autoForwardToggle;

    public static event Action OnSettingsChanged;

    private void Start()
    {
        LoadAudioSetting("MusicVol", musicSlider, "MusicVolume");
        LoadAudioSetting("SFXVol", sfxSlider, "SFXVolume");

        if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        if (textSpeedSlider != null)
        {
            textSpeedSlider.value = PlayerPrefs.GetInt("TextSpeed", 30);
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
        if (slider != null) slider.value = vol;
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
        OnSettingsChanged?.Invoke();
    }

    public void SetFontSize(float size)
    {
        PlayerPrefs.SetInt("FontSize", Mathf.RoundToInt(size));
        PlayerPrefs.Save();
        OnSettingsChanged?.Invoke();
    }

    public void SetAutoForward(bool isOn)
    {
        PlayerPrefs.SetInt("AutoForward", isOn ? 1 : 0);
        PlayerPrefs.Save();
        OnSettingsChanged?.Invoke();
    }

    public void CloseSettings()
    {
        if (settingsUIPanel != null) settingsUIPanel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        CloseSettings(); 
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
