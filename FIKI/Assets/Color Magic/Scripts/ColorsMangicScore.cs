using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorsMangicScore : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreText;
    

    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.instance.OnScoreChanged.AddListener(UpdateScoreText);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScoreText(int score)
    {
        Debug.Log("Score: " + score);
        scoreText.text = score.ToString();
    }
}
