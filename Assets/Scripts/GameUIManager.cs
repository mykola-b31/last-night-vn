using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;
using System.Collections;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressSlider;
    public Slider stressSlider;

    [Header("Yarn Spinner UI")]
    public LinePresenter yarnLinePresenter;
    public TextMeshProUGUI yarnDialogueText;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI textLastLine;

    [Header("Save Indicator")]
    public GameObject saveIcon;
    public int blinkCount = 5;
    public float blinkSpeed = 0.5f;

    private void Start()
    {
        if (progressSlider != null) progressSlider.maxValue = 100;
        if (stressSlider != null) stressSlider.maxValue = 100;

        ApplySettings();

        UpdateProgressUI(GameManager.Instance.progress);
        UpdateStressUI(GameManager.Instance.stress);
    }

    private void ApplySettings()
    {
        if (yarnLinePresenter != null)
        {
            yarnLinePresenter.lettersPerSecond = PlayerPrefs.GetInt("TextSpeed", 30);
            yarnLinePresenter.autoAdvance = PlayerPrefs.GetInt("AutoForward", 0) == 1;
        }

        int savedFontSize = PlayerPrefs.GetInt("FontSize", 24);

        if (yarnDialogueText != null)
        {
            yarnDialogueText.fontSize = savedFontSize;
        }

        if (characterNameText != null)
        {
            characterNameText.fontSize = savedFontSize;
        }

        if (textLastLine != null)
        {
            textLastLine.fontSize = savedFontSize * 0.85f;
        }
    }

    private void OnEnable()
    {
        GameManager.OnProgressChanged += UpdateProgressUI;
        GameManager.OnStressChanged += UpdateStressUI;
        SettingsManager.OnSettingsChanged += ApplySettings;
        SaveLoadManager.OnGameSaved += TriggerSaveIcon;
    }

    private void OnDisable()
    {
        GameManager.OnProgressChanged -= UpdateProgressUI;
        GameManager.OnStressChanged -= UpdateStressUI;
        SettingsManager.OnSettingsChanged -= ApplySettings;
        SaveLoadManager.OnGameSaved -= TriggerSaveIcon;
    }

    private void UpdateProgressUI(int newProgress)
    {
        if (progressSlider != null) progressSlider.value = newProgress;
    }

    private void UpdateStressUI(int newStress)
    {
        if (stressSlider != null) stressSlider.value = newStress;
    }

    private void TriggerSaveIcon()
    {
        if (saveIcon != null)
        {
            StopAllCoroutines();
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            saveIcon.SetActive(true);
            yield return new WaitForSeconds(blinkSpeed);
            saveIcon.SetActive(false);
            yield return new WaitForSeconds(blinkSpeed);
        }

        saveIcon.SetActive(false);
    }
}
