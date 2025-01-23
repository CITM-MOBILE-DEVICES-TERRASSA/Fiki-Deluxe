using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager
{
    public ScoreManagerLevel scoremanager;
    public static ScoreManager instance = new ScoreManager();

    public int score = 0;
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();

    private void Awake()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }

    public int GetScore() => score;
    public int SetScore(int newScore)
    {
        score += newScore;

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();

        OnScoreChanged?.Invoke(score);

        return score;
    }
}