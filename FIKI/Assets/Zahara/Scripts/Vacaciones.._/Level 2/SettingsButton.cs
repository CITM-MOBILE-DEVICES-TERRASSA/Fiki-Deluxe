using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public GameObject settingsCanvas;
    public bool isPaused = false;

    private void Start()
    {
        if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        settingsCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadMainLobby();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    void OnMouseDown()
    {

    }
}
