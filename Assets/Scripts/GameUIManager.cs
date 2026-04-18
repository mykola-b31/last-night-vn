using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressSlider;
    public Slider stressSlider;

    [Header("Yarn Spinner UI")]
    public LinePresenter yarnLinePresenter;
    public TextMeshProUGUI yarnDialogueText;

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

        if (yarnDialogueText != null)
        {
            yarnDialogueText.fontSize = PlayerPrefs.GetInt("FontSize", 24);
        }
    }

    private void OnEnable()
    {
        GameManager.OnProgressChanged += UpdateProgressUI;
        GameManager.OnStressChanged += UpdateStressUI;
    }

    private void OnDisable()
    {
        GameManager.OnProgressChanged -= UpdateProgressUI;
        GameManager.OnStressChanged -= UpdateStressUI;
    }

    private void UpdateProgressUI(int newProgress)
    {
        if (progressSlider != null) progressSlider.value = newProgress;
    }

    private void UpdateStressUI(int newStress)
    {
        if (stressSlider != null) stressSlider.value = newStress;
    }
}
