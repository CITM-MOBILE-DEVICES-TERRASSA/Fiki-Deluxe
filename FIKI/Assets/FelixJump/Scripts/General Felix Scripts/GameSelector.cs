using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSelector : MonoBehaviour
{
    [SerializeField] private Button[] gameButtons = new Button[5];
    [SerializeField] private bool[] availableGames = new bool[5];
    [SerializeField]private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        for (int i = 0; i < gameButtons.Length; i++)
        {
            gameButtons[i].interactable = availableGames[i];
        }
    }

    private void Start()
    {
        highScoreText.text = "High Score: " + Score.Instance.GetTotalScore().ToString();
    }

    public void SelectGame(string gameName)
    {
        ScenesManager.Instance.LoadScene(gameName);
    }
}