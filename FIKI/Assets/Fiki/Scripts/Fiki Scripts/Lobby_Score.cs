using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_Score : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI score;
    [SerializeField] public TextMeshProUGUI HighScore;
    [SerializeField] public TextMeshProUGUI Coins;
    
    void Start()
    {
        //UpdateScores();
    }

    private void UpdateScores()
    {
        Debug.LogWarning("update scores");
        if (Manager.instance == null)
        {
            Debug.LogWarning("Manager instance is null!");
            return;
        }

        if (score != null)
            score.text = Manager.instance.score.ToString();

        if (HighScore != null)
            HighScore.text = Manager.instance.maxscore.ToString();

        if (Coins != null)
        {
            Coins.text = Manager.instance.coins.ToString();
            
        }
    }
}
