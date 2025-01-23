using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManagerLevel : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText; 
    public ScoreManagerOrig scoremanager;

    void Start()
    {
        
         scoremanager = FindObjectOfType<ScoreManagerOrig>();
         scoremanager = ScoreManagerOrig.Instance;
         scoremanager.score1 = 0;
         scoremanager.score2 = 0;
    }
    public void UpdateScoreLevel1(int points)
    {
        scoremanager.score1   += points;
        UpdateScoreText1();
    }

    private void UpdateScoreText1()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + scoremanager.score1;
            if(scoremanager.score1 > scoremanager.MaxScore1)
            {
                scoremanager.MaxScore1 = scoremanager.score1;
               
            }
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }

    public void UpdateScoreLevel2(int points)
    {
        scoremanager.score2 += points;
        UpdateScoreText2();
    }

    private void UpdateScoreText2()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + scoremanager.score2;
            if(scoremanager.score2 > scoremanager.MaxScore2)
            {
                scoremanager.MaxScore2 = scoremanager.score2;
            }
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }
}
