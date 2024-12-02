using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MetaScreen : MonoBehaviour
{
    public Text scoreText;
    private int totalScore = 0;

    //private ScoreManager scoremanager;

    private void Start()
    {
        //scoreManager = ScoreManager.Instance;
        UpdateScoreDisplay();
    }

    public void SelectLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "TOTAL SCORE: " + totalScore;
    }

    public void AddScore(int score)
    {
        totalScore += score;
        UpdateScoreDisplay();
    }
}
