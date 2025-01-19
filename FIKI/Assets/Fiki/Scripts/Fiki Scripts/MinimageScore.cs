using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimageScore : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI score;
    [SerializeField] public TextMeshProUGUI HighScore;


    void Start()
    {
        UpdateScores();
    }
    private void Update()
    {
        UpdateScores();
    }
    private void UpdateScores()
    {
        if (Manager.instance == null)
        {
            Debug.LogWarning("Manager instance is null!");
            return;
        }

        if (score != null)
            score.text = Manager.instance.score.ToString();

        if (HighScore != null)
            HighScore.text = Manager.instance.maxscore.ToString();

    }
}
