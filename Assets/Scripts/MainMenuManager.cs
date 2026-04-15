using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";
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
        // TODO: Реалізувати вікно налаштувань
        Debug.Log("Відкрито налаштування");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
