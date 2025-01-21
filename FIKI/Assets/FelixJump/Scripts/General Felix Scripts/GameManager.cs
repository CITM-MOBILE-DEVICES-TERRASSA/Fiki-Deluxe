using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        // Score.Instance.ResetScore();
        // Score.Instance.SetAddScore(true);
    }

    public void EndGame()
    {
        // Score.Instance.SetAddScore(false);
    }
    
    public void PauseGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is null! Ensure GameManager exists in the scene.");
            return;
        }

        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
