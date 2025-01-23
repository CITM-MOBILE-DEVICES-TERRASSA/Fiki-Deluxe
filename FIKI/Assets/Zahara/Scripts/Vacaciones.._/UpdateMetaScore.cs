using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateMetaScore : MonoBehaviour
{
    public TextMeshProUGUI metaScoreText;
    public TextMeshProUGUI Level1ScoreText;
    public TextMeshProUGUI Level2ScoreText;

    private ScoreManagerOrig scoreManagerOrig;

    private void Start()
    {

        scoreManagerOrig = ScoreManagerOrig.Instance;
        UpdateScoreTotal();
        
    }
    private void Update()
    {
        UpdateMetaScoreTotalText();
        UpdateMetaScoreLevel1Text();
        UpdateMetaScoreLevel2Text();
    }
    private void UpdateScoreTotal()
    {
        scoreManagerOrig.MaxTotalLevels = scoreManagerOrig.MaxScore1 + scoreManagerOrig.MaxScore2;
        scoreManagerOrig.MaxTotalGame = scoreManagerOrig.MaxTotalLevels;
    }
    private void UpdateMetaScoreTotalText()
    {
        if (metaScoreText != null)
        {
            metaScoreText.text = "Puntos: " + scoreManagerOrig.MaxTotalLevels;
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
        
    }
    private void UpdateMetaScoreLevel1Text()
    {
        if (Level1ScoreText != null)
        {
            Level1ScoreText.text = "Puntos: " + scoreManagerOrig.MaxScore1;
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
       
    }
    private void UpdateMetaScoreLevel2Text()
    {
        if (Level2ScoreText != null)
        {
            Level2ScoreText.text = "Puntos: " + scoreManagerOrig.MaxScore2;
        }
        else
        {
            Debug.LogError("Score Text no está asignado en el ScoreManager.");
        }
    }
}
