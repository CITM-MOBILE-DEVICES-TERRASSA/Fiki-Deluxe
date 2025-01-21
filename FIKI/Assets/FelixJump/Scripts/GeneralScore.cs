using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralScore : MonoBehaviour
{
    public static GeneralScore Instance;

    [SerializeField] private TextMeshProUGUI highScoreText;

    private int totalScore;
    private int fikiScore;
    private int otherGameScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opcional, si quieres que persista entre escenas.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGlobalScore();
    }

    public void UpdateFikiScore(int score)
    {
        fikiScore = score;
        UpdateGlobalScore();
    }

    public void UpdateOtherGameScore(int score)
    {
        otherGameScore = score;
        UpdateGlobalScore();
    }

    private void UpdateGlobalScore()
    {
        totalScore = fikiScore + otherGameScore;

        if (highScoreText != null)
        {
            highScoreText.text = "Global HighScore: " + totalScore;
        }
    }
}
