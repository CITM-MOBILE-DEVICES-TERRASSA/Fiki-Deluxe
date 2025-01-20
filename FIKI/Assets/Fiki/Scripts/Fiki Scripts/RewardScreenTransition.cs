using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreenTransition : MonoBehaviour
{
    // Start is called before the first frame update
     public Button playAgain;
     public Button returnMenu;
    [SerializeField] private Lobby_Score lobby_Score;
    void Start()
    {
        
        if (Manager.instance.score > Manager.instance.maxscore)
        {
            Manager.instance.maxscore = Manager.instance.score;
            lobby_Score.HighScore.text = Manager.instance.maxscore.ToString();
        }

        Manager.instance.coins += Manager.instance.score;
        Debug.Log("Coins: " + Manager.instance.coins);
        Debug.Log("Coins: " + Manager.instance.score);
        UpdateLobbyScore.Instance.UpdateGame1Score(Manager.instance.score);

        if (playAgain != null)
            playAgain.onClick.AddListener(PlayAgain);
        else
            Debug.LogWarning("Play Button is not assigned!");

        if (returnMenu != null)
            returnMenu.onClick.AddListener(RetunMenu);
        else
            Debug.LogWarning("Return Button is not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain() {
       
        Manager.instance.score = 0;
        LevelTransitionController.instance.StartTransition(3, 2);
    }

    public void RetunMenu() {
        
        Manager.instance.score = 0;
        LevelTransitionController.instance.StartTransition(2, 2);
    }

}
