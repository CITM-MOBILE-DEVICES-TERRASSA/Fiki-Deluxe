using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UpdateLobbyScore : MonoBehaviour
{
    public TextMeshProUGUI lobbyScore; 
    
    private ScoreManagerOrig scoreManagerOrig;

    void Start()
    {
        scoreManagerOrig = ScoreManagerOrig.Instance;
        UpdateTotalGameScore();
    }
    private void Update()
    {
        ShowScoreText1();
    }
    public void UpdateTotalGameScore()
    {
        UpdateScoreTotal();
        scoreManagerOrig.MaxTotalGame = scoreManagerOrig.MaxTotalLevels + ScoreManager.instance.score; // a�adir mas para mas juegos
    }
     private void ShowScoreText1()
    {
        if (lobbyScore != null)
        {
            lobbyScore.text = "Puntos" + scoreManagerOrig.MaxTotalGame;
        }
        else
        {
            Debug.LogError("Score Text no est� asignado en el ScoreManager.");
        }
        
    }
    private void UpdateScoreTotal()
    {
        scoreManagerOrig.MaxTotalLevels = scoreManagerOrig.MaxScore1 + scoreManagerOrig.MaxScore2;
        scoreManagerOrig.MaxTotalGame = scoreManagerOrig.MaxTotalLevels;
    }
}
