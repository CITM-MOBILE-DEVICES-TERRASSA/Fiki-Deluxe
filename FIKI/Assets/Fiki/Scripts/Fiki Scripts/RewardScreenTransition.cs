using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreenTransition : MonoBehaviour
{
    // Start is called before the first frame update
     public Button playAgain;
     public Button returnMenu;
    void Start()
    {
        if (playAgain != null)
            playAgain.onClick.AddListener(PlayAgain);
        else
            Debug.LogWarning("Play Button is not assigned!");

        if (returnMenu != null)
            returnMenu.onClick.AddListener(RetunMenu);
        else
            Debug.LogWarning("Return Button is not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain() {
        LevelTransitionController.instance.StartTransition(3, 2);
    }

    public void RetunMenu() {

        LevelTransitionController.instance.StartTransition(2, 2);
    }

}
