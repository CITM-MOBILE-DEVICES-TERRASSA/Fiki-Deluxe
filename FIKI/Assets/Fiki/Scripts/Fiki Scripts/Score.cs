using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        currentScoreText.text = Manager.instance.score.ToString();
        highScoreText.text = Manager.instance.maxscore.ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (Manager.instance.score > Manager.instance.maxscore)
        {
            Manager.instance.maxscore = Manager.instance.score;
            highScoreText.text = Manager.instance.maxscore.ToString();
        }
    }

    public void UpdateScore()
    {
        Manager.instance.score++;
        currentScoreText.text = Manager.instance.score.ToString();
        UpdateHighScore();
    }
}
