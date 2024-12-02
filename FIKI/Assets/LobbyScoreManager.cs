using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI Fiki;
    public TextMeshProUGUI JumpingJack;


    private ScoreManager scoreManager;

    void Start()
    {

        scoreManager = ScoreManager.Instance;
        UpdateTotalGameScore();
    }
    private void Update()
    {
        ShowScoreText1();
    }
    public void UpdateTotalGameScore()
    {
        UpdateScoreTotal();
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels; // a?adir mas para mas juegos
    }
    private void ShowScoreText1()
    {
        if (Fiki != null)
        {
            Fiki.text = "Fiki:" + scoreManager.MaxScoreFiki;
        }
        else
        {
            Debug.LogError("Score Text no est? asignado en el ScoreManager.");
        }
        if (JumpingJack != null)
        {
            JumpingJack.text = "JumpingJack:" + scoreManager.MaxScoreJumpingJAck;
        }
        else
        {
            Debug.LogError("Score Text no est? asignado en el ScoreManager.");
        }

    }
    private void UpdateScoreTotal()
    {
        scoreManager.MaxTotalLevels = scoreManager.MaxScoreJumpingJAck + scoreManager.MaxScoreFiki;
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels;
    }
}
