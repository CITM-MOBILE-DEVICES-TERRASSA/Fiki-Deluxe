using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardScreenTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public Button playAgain;
    public Button returnMenu;

    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;
    [SerializeField] public TextMeshProUGUI coinsText;

    void Start()
    {
        Manager.instance.coins += Manager.instance.score;

        coinsText.text = Manager.instance.coins.ToString();
        scoreText.text = Manager.instance.score.ToString();
        highScoreText.text = Manager.instance.maxscore.ToString();

        PlayerPrefs.SetInt("FikiScore", Manager.instance.score);
        PlayerPrefs.SetInt("FikiHighScore", Manager.instance.maxscore);
        PlayerPrefs.SetFloat("FikiCoins", Manager.instance.coins);

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

    public void PlayAgain() {
       
        Manager.instance.score = 0;
        LevelTransitionController.instance.StartTransition(3, 0.5f);
    }

    public void RetunMenu() {
        
        Manager.instance.score = 0;
        LevelTransitionController.instance.StartTransition(2, 0.5f);
    }

}
