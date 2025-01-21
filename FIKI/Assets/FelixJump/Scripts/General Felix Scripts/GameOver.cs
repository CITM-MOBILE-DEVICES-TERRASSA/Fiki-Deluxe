using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverPanel;

    public void Lose()
    {
        text.text = "You Lose!";
        // scoreText.text = "Score: " + Score.Instance.GetScore().ToString();
        // highScoreText.text = "High Score: " + Score.Instance.GetHighScore().ToString();
        gameOverPanel.SetActive(true);
        GameManager.Instance.EndGame();
    }

    public void Win()
    {
        text.text = "You Win!";
        // scoreText.text = "Score: " + Score.Instance.GetScore().ToString();
        // highScoreText.text = "High Score: " + Score.Instance.GetHighScore().ToString();
        gameOverPanel.SetActive(true);
        GameManager.Instance.EndGame();
    }
}
