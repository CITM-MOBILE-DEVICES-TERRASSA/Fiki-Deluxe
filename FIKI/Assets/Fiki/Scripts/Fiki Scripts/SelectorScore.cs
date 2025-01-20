using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SelectorScore : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI FikiScore;
    [SerializeField] public TextMeshProUGUI JumpingJackScore;
    [SerializeField] public TextMeshProUGUI TotalScore;

    void Update()
    {
        if (Manager.instance == null)
        {
            Debug.LogWarning("Manager instance is null!");
            return;
        }
        if(FikiScore != null)
        {
            FikiScore.text = "Fiki: " + Manager.instance.coins.ToString();
        }
       if(TotalScore!= null)
        {
            TotalScore.text = "TotalScore:" + FikiScore.ToString();
        }
        
    }
}
