using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameCompleted : MonoBehaviour
{
    void Start()
    {

    }

    public void LevelCompleted()
    {
        LevelTransitionController.instance.StartTransition(4, 2);
    }
}