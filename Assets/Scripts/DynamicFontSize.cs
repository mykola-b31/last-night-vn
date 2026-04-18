using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DynamicFontSize : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (textComponent != null)
        {
            textComponent.fontSize = PlayerPrefs.GetInt("FontSize", 24);
        }
    }
}
