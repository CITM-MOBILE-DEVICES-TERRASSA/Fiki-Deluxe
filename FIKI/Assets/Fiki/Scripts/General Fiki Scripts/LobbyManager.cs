using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyManager : MonoBehaviour
{
    public TMP_Text levelText;
    public TMP_Text hungerText;
    public TMP_Text happinessText;
    public TMP_Text sleepText;
    public TMP_Text hygieneText;
    public TMP_Text coinsText;

    public int level = 0;
    public float hunger = 37f;
    public float happiness = 69f;
    public float sleep = 26f;
    public float hygiene = 13f;
    public float coins = 126.017f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        levelText.text = "Lvl " + level;
        hungerText.text = hunger + "%";
        happinessText.text = happiness + "%";
        sleepText.text = sleep + "%";
        hygieneText.text = hygiene + "%";
        coinsText.text = coins.ToString("F3");
    }


    public void PlayButton()
    {
        LevelTransitionController.instance.StartTransition(3, 2);
    }

}