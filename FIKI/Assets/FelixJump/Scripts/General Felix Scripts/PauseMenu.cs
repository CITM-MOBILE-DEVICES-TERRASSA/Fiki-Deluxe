using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void PauseGame()
    {
        if (pausePanel == null)
        {
            Debug.LogError("Pause panel is not assigned!");
            return;
        }
        pausePanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void ResumeGame()
    {
        if (pausePanel == null)
        {
            Debug.LogError("Pause panel is not assigned!");
            return;
        }
        pausePanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}