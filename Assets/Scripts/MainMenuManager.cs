using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";
    public GameObject settingsPanel;

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

    public void QuitGame()
    {
        Application.Quit();
    }
}
