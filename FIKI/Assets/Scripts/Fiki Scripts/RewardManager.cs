using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardManager : MonoBehaviour
{
    public TextMeshProUGUI textScore, textMaxScore;
    
    // Start is called before the first frame update
    void Start()
    {
        textScore.text = "Score: " + Manager.instance.score.ToString();
        textMaxScore.text = "MaxScore: " + Manager.instance.maxscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
