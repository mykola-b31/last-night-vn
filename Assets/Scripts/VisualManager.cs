using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System.Collections.Generic;

public class VisualManager : MonoBehaviour
{
    public static VisualManager Instance { get; private set; }

    [Header("UI Elements")]
    public Image backgroundImage;
    public Image leftCharacterImage;
    public Image rightCharacterImage;

    [System.Serializable]
    public struct SpriteData
    {
        public string idName;
        public Sprite sprite;
    }

    [Header("Sprite Databases")]
    public List<SpriteData> backgrounds;
    public List<SpriteData> characters;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    [YarnCommand("set_bg")]
    public static void SetBackground(string bgName)
    {
        foreach (var bg in Instance.backgrounds)
        {
            if (bg.idName == bgName)
            {
                Instance.backgroundImage.sprite = bg.sprite;
                Instance.backgroundImage.enabled = true;
                return;
            }
        }
        Debug.LogWarning($"Фон з назвою '{bgName}' не знайдено!");
    }

    [YarnCommand("show_char_left")]
    public static void ShowCharacterLeft(string charName)
    {
        foreach (var ch in Instance.characters)
        {
            if (ch.idName == charName)
            {
                Instance.leftCharacterImage.sprite = ch.sprite;
                Instance.leftCharacterImage.enabled = true;
                return;
            }
        }
        Debug.LogWarning($"Персонажа з назвою '{charName}' не знайдено!");
    }

    [YarnCommand("show_char_right")]
    public static void ShowCharacterRight(string charName)
    {
        foreach (var ch in Instance.characters)
        {
            if (ch.idName == charName)
            {
                Instance.rightCharacterImage.sprite = ch.sprite;
                Instance.rightCharacterImage.enabled = true;
                return;
            }
        }
        Debug.LogWarning($"Персонажа з назвою '{charName}' не знайдено!");
    }

    [YarnCommand("hide_char_left")]
    public static void HideCharacterLeft()
    {
        Instance.leftCharacterImage.enabled = false;
    }

    [YarnCommand("hide_char_right")]
    public static void HideCharacterRight()
    {
        Instance.rightCharacterImage.enabled = false;
    }
}
