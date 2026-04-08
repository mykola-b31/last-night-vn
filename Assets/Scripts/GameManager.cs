using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Metrics")]

    [Range(0, 100)] public int progress = 35;
    [Range(0, 100)] public int stress = 45;

    [Header("UI Elements")]
    public Slider progressSlider;
    public Slider stressSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (progressSlider != null) progressSlider.maxValue = 100;
        if (stressSlider != null) stressSlider.maxValue = 100;

        UpdateUI();
    }

    [YarnFunction("get_progress")]
    public static int GetProgress()
    {
        return Instance.progress;
    }

    [YarnFunction("get_stress")]
    public static int GetStress()
    {
        return Instance.stress;
    }

    [YarnCommand("add_progress")]
    public static void AddProgress(int amount)
    {
        Instance.progress = Mathf.Clamp(Instance.progress + amount, 0, 100);
        Instance.UpdateUI();
        Debug.Log($"Прогрес збільшено на {amount}. Поточний прогрес: {Instance.progress}%");
    }

    [YarnCommand("add_stress")]
    public static void AddStress(int amount)
    {
        Instance.stress = Mathf.Clamp(Instance.stress + amount, 0, 100);
        Instance.UpdateUI();
        Debug.Log($"Стрес змінився на {amount}. Поточний стрес: {Instance.stress}%");
    }

    private void UpdateUI()
    {
        if (progressSlider != null) progressSlider.value = Instance.progress;
        if (stressSlider != null) stressSlider.value = Instance.stress;
    }

}