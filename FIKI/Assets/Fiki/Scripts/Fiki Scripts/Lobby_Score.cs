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
    private bool coinsUpdated = false;
    void Start()
    {
        score.text = Manager.instance.score.ToString();
        HighScore.text = Manager.instance.maxscore.ToString();
        coinsUpdated = false;
    }
    void Update()
    {
        score.text = Manager.instance.score.ToString();
        score.text = Manager.instance.maxscore.ToString();
        if(!coinsUpdated)
        {
            Coins.text += Manager.instance.coins.ToString();
            coinsUpdated = true;
        }
        
    }
}
