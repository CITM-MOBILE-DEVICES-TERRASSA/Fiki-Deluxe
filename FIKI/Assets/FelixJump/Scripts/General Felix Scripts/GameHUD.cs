using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        if (scoreText == null || highScoreText == null)
        {
            Debug.LogError("ScoreText or HighScoreText is not assigned!");
        }

        UpdateScoreTexts();
    }

    private void Update()
    {
        UpdateScoreTexts();
    }

    private void UpdateScoreTexts()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        scoreText.text = Score.Instance.GetScore(currentScene).ToString();
        highScoreText.text = Score.Instance.GetHighScore(currentScene).ToString();
    }

    public void ChangeToMenu()
    {
        if (ScenesManager.Instance == null)
        {
            Debug.LogError("ScenesManager.Instance is null! Ensure ScenesManager exists in the scene.");
            return;
        }

        ScenesManager.Instance.LoadScene("GameSelector");
        // Score.Instance.SetAddScore(false);
    }
}