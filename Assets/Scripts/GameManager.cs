using UnityEngine;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int progress = 0;
    public int stress = 0;

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

    [YarnCommand("add_progress")]
    public static void AddProgress(int amount)
    {
        Instance.progress += amount;
        Debug.Log($"Прогрес збільшено на {amount}. Поточний прогрес: {Instance.progress}%");
    }

    [YarnCommand("add_stress")]
    public static void AddStress(int amount)
    {
        Instance.stress += amount;
        Debug.Log($"Стрес змінився на {amount}. Поточний стрес: {Instance.stress}%");
    }

}