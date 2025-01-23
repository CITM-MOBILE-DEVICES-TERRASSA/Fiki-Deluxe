using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerOrig: MonoBehaviour
{
    #region Singleton
    private static ScoreManagerOrig _instance;
    public static ScoreManagerOrig Instance => _instance;

    private void Awake()
    {
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    #endregion


    private int score;
    private int totalScore;

    public int score1;
    public int score2;
    public int MaxScore1;
    public int MaxScore2;
    public int MaxTotalLevels;
    public int MaxTotalGame;
    

}
