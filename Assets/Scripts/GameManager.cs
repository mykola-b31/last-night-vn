using System;
using Unity.VisualScripting;
using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Metrics")]
    [Range(0, 100)] public int progress = 35;
    [Range(0, 100)] public int stress = 45;

    public static event Action<int> OnProgressChanged;
    public static event Action<int> OnStressChanged;

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
    
    [YarnFunction("get_progress")]
    public static int GetProgress() => Instance.progress;

    [YarnFunction("get_stress")]
    public static int GetStress() => Instance.stress;

    [YarnCommand("add_progress")]
    public static void AddProgress(int amount)
    {
        Instance.progress = Mathf.Clamp(Instance.progress + amount, 0, 100);
        OnProgressChanged?.Invoke(Instance.progress);
        Debug.Log($"Прогрес збільшено на {amount}. Поточний прогрес: {Instance.progress}%");
    }

    [YarnCommand("add_stress")]
    public static void AddStress(int amount)
    {
        Instance.stress = Mathf.Clamp(Instance.stress + amount, 0, 100);
        OnStressChanged?.Invoke(Instance.stress);
        Debug.Log($"Стрес змінився на {amount}. Поточний стрес: {Instance.stress}%");
    }

    public static void LoadState(int savedProgress, int savedStress)
    {
        Instance.progress = savedProgress;
        Instance.stress = savedStress;
        
        OnProgressChanged?.Invoke(Instance.progress);
        OnStressChanged?.Invoke(Instance.stress);
        Debug.Log($"Завантажено стан: Прогрес - {Instance.progress}%, Стрес - {Instance.stress}%");
    }
}