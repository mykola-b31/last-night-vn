using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class CreditsManager : MonoBehaviour
{
    public static CreditsManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject creditsPanel;
    public RectTransform creditsTextRect;

    [Header("Settings")]
    public float scrollSpeed = 50f;
    public string mainMenuSceneName = "MainMenu";

    private bool isRolling = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (isRolling && creditsTextRect != null)
        {
            creditsTextRect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            float canvasHeight = creditsPanel.GetComponent<RectTransform>().rect.height;

            if (creditsTextRect.anchoredPosition.y >= creditsTextRect.rect.height + canvasHeight)
            {
                ReturnToMainMenu();
            }

            if (Input.anyKeyDown)
            {
                ReturnToMainMenu();
            }
        }
    }

    [YarnCommand("show_credits")]
    public static void ShowCredits()
    {
        if (Instance != null)
        {
            Instance.creditsPanel.SetActive(true);
            Instance.isRolling = true;
        }
    }

    private void ReturnToMainMenu()
    {
        isRolling = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
