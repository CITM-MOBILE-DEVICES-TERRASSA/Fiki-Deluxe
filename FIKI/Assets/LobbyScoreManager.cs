using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI FikiText;
    public TextMeshProUGUI JumpingJackText;
    public TextMeshProUGUI TotalScoreText;


    private ScoreManager scoreManager;

    //public static LobbyScoreManager Instance { get; private set; } mañana lo arreglo q me estoy durmiendo
    public int Game1Score { get; private set; } 
    public int Game2Score { get; private set; }

    void Start()
    {

        scoreManager = ScoreManager.Instance;
        //UpdateTotalGameScore();
        UpdateScoreTotal();
    }
    private void Update()
    {
        ShowScoreTexts();
    }
    public void UpdateTotalGameScore()
    {
        UpdateScoreTotal();
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels; // a?adir mas para mas juegos
    }

    private void ShowScoreTexts()
    {
        if (FikiText != null)
        {
            FikiText.text = "Fiki:" + scoreManager.MaxScoreFiki;
        }
        else
        {
            Debug.LogError("Score Text no est? asignado en el ScoreManager.");
        }

        if (JumpingJackText != null)
        {
            JumpingJackText.text = "JumpingJack:" + scoreManager.MaxScoreJumpingJAck;
        }
        else
        {
            Debug.LogError("Score Text no est? asignado en el ScoreManager.");
        }

        if (TotalScoreText != null)
        {
            TotalScoreText.text = "Total Score: " + scoreManager.MaxTotalLevels;
        }
        else
        {
            Debug.LogError("El texto TotalScore no está asignado.");
        }

    }
    private void UpdateScoreTotal()
    {
        scoreManager.MaxTotalLevels = scoreManager.MaxScoreJumpingJAck + scoreManager.MaxScoreFiki;
        scoreManager.MaxTotalGame = scoreManager.MaxTotalLevels;
    }

    public void UpdateGame1Score(int newScore)
    {
        Game1Score = newScore;
        Debug.Log("Puntuación de FIKI actualizada: " + Game1Score);
    }

}
