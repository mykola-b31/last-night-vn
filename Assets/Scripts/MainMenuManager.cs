using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public string gameSceneName = "SampleScene";

    [Header("UI References")]
    public Button continueButton;

    public GameObject settingsPanel;

    private void Start()
    {
        string saveFilePath = Application.persistentDataPath + "/savegame.json";
        if (continueButton != null)
        {
            continueButton.interactable = File.Exists(saveFilePath);
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ContinueGame()
    {
        string saveFilePath = Application.persistentDataPath + "/savegame.json";
        
        if (File.Exists(saveFilePath))
        {
            SaveLoadManager.ShouldLoadSave = true;
            SceneManager.LoadScene(gameSceneName);
        }
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
