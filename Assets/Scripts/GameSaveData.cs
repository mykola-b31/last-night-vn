[System.Serializable]
public class GameSaveData
{
    public int progress;
    public int stress;

    public string currentYarnNode;

    public string[] floatKeys;
    public float[] floatValues;

    public string[] stringKeys;
    public string[] stringValues;

    public string[] boolKeys;
    public bool[] boolValues;

    public string currentBackgroundId;
    public string leftCharacterId;
    public string rightCharacterId;
}
