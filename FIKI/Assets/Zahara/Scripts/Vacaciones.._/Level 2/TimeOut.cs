using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TimeOut : MonoBehaviour
{
    public Canvas endGameCanvas;
    public bool isPaused = false;
    public TextMeshProUGUI endScoreText;
    private float timer = 150f;

    private ScoreManagerLevel scoreManagerLevel;
    // Start is called before the first frame update
    void Start()
    {
        endGameCanvas.gameObject.SetActive(false);

        scoreManagerLevel = FindObjectOfType<ScoreManagerLevel>();

        Invoke("ShowEndGameCanvas", timer);
    }

    void ShowEndGameCanvas()
    {
        endGameCanvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        int currentScore = scoreManagerLevel.scoremanager.score2;

        endScoreText.text = currentScore.ToString();
    }

    public void ReturnToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneLoader.Instance.LoadMainLobby();
    }
}
