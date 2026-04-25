using UnityEngine;
using Yarn.Unity;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }

    public static bool ShouldLoadSave = false;

    [Header("Yarn References")]
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage variableStorage;

    private string SaveFilePath => Application.persistentDataPath + "/savegame.json";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (ShouldLoadSave)
        {
            LoadGame();
            ShouldLoadSave = false;
        }
        else
        {
            if (dialogueRunner != null)
            {
                GameManager.LoadState(35, 45);
                variableStorage.Clear();
                
                dialogueRunner.Stop();
                dialogueRunner.StartDialogue("Start");
            }
        }
    }

    [YarnCommand("save_game")]
    public static void SaveGame()
    {
        GameSaveData data = new GameSaveData();
        
        data.progress = GameManager.GetProgress();
        data.stress = GameManager.GetStress();

        data.currentYarnNode = Instance.dialogueRunner.Dialogue.CurrentNode;

        var (floatVars, stringVars, boolVars) = Instance.variableStorage.GetAllVariables();

        data.floatKeys = floatVars.Keys.ToArray();
        data.floatValues = floatVars.Values.ToArray();

        data.stringKeys = stringVars.Keys.ToArray();
        data.stringValues = stringVars.Values.ToArray();

        data.boolKeys = boolVars.Keys.ToArray();
        data.boolValues = boolVars.Values.ToArray();

        data.currentBackgroundId = VisualManager.Instance.currentBgId;
        data.leftCharacterId = VisualManager.Instance.currentLeftCharId;
        data.rightCharacterId = VisualManager.Instance.currentRightCharId;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Instance.SaveFilePath, json);

        Debug.Log($"Гра збережена за шляхом: {Instance.SaveFilePath}");
    }

    public void LoadGame()
    {
        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("Файл збереження не знайдено!");
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        GameSaveData data = JsonUtility.FromJson<GameSaveData>(json);

        GameManager.LoadState(data.progress, data.stress);

        if (!string.IsNullOrEmpty(data.currentBackgroundId))
        {
            VisualManager.SetBackground(data.currentBackgroundId);
        }

        if (!string.IsNullOrEmpty(data.leftCharacterId))
        {
            VisualManager.ShowCharacterLeft(data.leftCharacterId);
        }
        else
        {
            VisualManager.HideCharacterLeft();
        }

        if (!string.IsNullOrEmpty(data.rightCharacterId))
        {
            VisualManager.ShowCharacterRight(data.rightCharacterId);
        }
        else
        {
            VisualManager.HideCharacterRight();
        }

        var floatVars = new Dictionary<string, float>();
        if (data.floatKeys != null)
        {
            for (int i = 0; i < data.floatKeys.Length; i++)
            {
                floatVars[data.floatKeys[i]] = data.floatValues[i];
            }
        }

        var stringVars = new Dictionary<string, string>();
        if (data.stringKeys != null)
        {
            for (int i = 0; i < data.stringKeys.Length; i++)
            {
                stringVars[data.stringKeys[i]] = data.stringValues[i];
            }
        }

        var boolVars = new Dictionary<string, bool>();
        if (data.boolKeys != null)
        {
            for (int i = 0; i < data.boolKeys.Length; i++)
            {
                boolVars[data.boolKeys[i]] = data.boolValues[i];
            }
        }

        Instance.variableStorage.SetAllVariables(floatVars, stringVars, boolVars);

        dialogueRunner.Stop();
        dialogueRunner.StartDialogue(data.currentYarnNode);

        Debug.Log($"Гра завантажена з: {SaveFilePath}");
    }
}
