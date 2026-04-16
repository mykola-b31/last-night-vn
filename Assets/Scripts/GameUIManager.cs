using CsvHelper.Configuration.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [HeaderPrefix("UI Elements")]
    public Slider progressSlider;
    public Slider stressSlider;

    private void Start()
    {
        if (progressSlider != null) progressSlider.maxValue = 100;
        if (stressSlider != null) stressSlider.maxValue = 100;

        UpdateProgressUI(GameManager.Instance.progress);
        UpdateStressUI(GameManager.Instance.stress);
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
