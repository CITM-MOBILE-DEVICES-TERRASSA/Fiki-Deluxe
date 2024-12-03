using UnityEngine;
using TMPro;
using System.Diagnostics;

public class Score : MonoBehaviour
{
    [SerializeField] private string scoreTextObjectName = "Score";
    private TextMeshProUGUI scoreText;
    private Timer timer;
    private int levelScore;
    private int lastDisplayedScore = -1; // Para evitar actualizaciones innecesarias

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        InitializeScoreText();
        ResetScore();              
    }

    void Update()
    {
        if (timer != null)
        {
            UpdateLevelScore();
        }
    }

    private void UpdateLevelScore()
    {
        levelScore = (int)timer.totalTime;

        if (levelScore != lastDisplayedScore)
        {
            UpdateScoreDisplay();
            lastDisplayedScore = levelScore;
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {levelScore}";
        }
    }

    public void ResetScore()
    {
        levelScore = 0;
        lastDisplayedScore = -1;
        UpdateScoreDisplay();
    }

    public void AddScoreToTotal()
    {
        if (UpdateLobbyScore.Instance != null)
        {
            UpdateLobbyScore.Instance.UpdateGame2Score(levelScore);
        }
        else
        {
            UnityEngine.Debug.LogError("No se encontró una instancia de UpdateLobbyScore en la escena.");
        }

        int totalScore = PlayerPrefs.GetInt("totalScore", 0);
        totalScore += levelScore;
        // Guarda punt total actualizada
        PlayerPrefs.SetInt("totalScore", totalScore);
        PlayerPrefs.Save(); 
    }

    private void InitializeScoreText()
    {
        GameObject textObject = GameObject.Find(scoreTextObjectName);

        if (textObject != null)
        {
            scoreText = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            UnityEngine.Debug.LogWarning($"No se encontró el objeto TextMeshProUGUI con el nombre '{scoreTextObjectName}'.");
        }
    }
}
