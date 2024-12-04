using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyScreen : MonoBehaviour
{
    public Text scoreText;
    private int totalScore = 0;

    private void Start()
    {
        totalScore = PlayerPrefs.GetInt("totalScore", 0);
        UpdateScoreDisplay();
    }

    public void SelectMinigame(string minigameName)
    {
        SceneManager.LoadScene(minigameName);
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "TOTAL SCORE: " + totalScore;
    }

}
