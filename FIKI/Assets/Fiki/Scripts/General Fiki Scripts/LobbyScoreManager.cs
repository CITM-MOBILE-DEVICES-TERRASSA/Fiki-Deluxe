using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UpdateLobbyScore : MonoBehaviour
{
    // Singleton Instance
    public static UpdateLobbyScore Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI FikiText;          
    public TextMeshProUGUI JumpingJackText;  
    public TextMeshProUGUI TotalScoreText;

    [HideInInspector] public int Game1Score { get; private set; } //(Fiki)
    [HideInInspector] public int Game2Score { get; private set; } //(Jumping Jack)

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);//si quisieramos q algo persista entre escenas modificar o eliminar esto
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AssignReferences();
        UpdateScoreTotal();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LobbyScene")
        {
            AssignReferences();
        }
    }

    //private void Update()
    //{
    //    ShowScoreTexts();
    //}
        
    public void UpdateGame1Score(int newScore)
    {
        Game1Score = newScore;
        Debug.Log("Puntuación de FIKI actualizada: " + Game1Score);
        UpdateScoreTotal();
    }
        
    public void UpdateGame2Score(int newScore)
    {
        Game2Score = newScore;
        Debug.Log("Puntuación de Jumping Jack actualizada: " + Game2Score);
        UpdateScoreTotal();
    }

    private void AssignReferences()
    {
        GameObject fikiTextObject = GameObject.Find("FikiText");
        if (fikiTextObject != null)
        {
            FikiText = fikiTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto FikiText en la escena.");
        }

        GameObject jumpingJackTextObject = GameObject.Find("JumpingJackText");
        if (jumpingJackTextObject != null)
        {
            JumpingJackText = jumpingJackTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto JumpingJackText en la escena.");
        }

        GameObject totalScoreTextObject = GameObject.Find("TotalScoreText");
        if (totalScoreTextObject != null)
        {
            TotalScoreText = totalScoreTextObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto TotalScoreText en la escena.");
        }
    }

    private void UpdateScoreTotal()
    {
        int totalScore = Game1Score + Game2Score;

        if (FikiText != null)
        {
            FikiText.text = "Fiki: " + Game1Score;
        }
        if (JumpingJackText != null)
        {
            JumpingJackText.text = "JumpingJack: " + Game2Score;
        }
        if (TotalScoreText != null)
        {
            TotalScoreText.text = "Total Score: " + totalScore;
        }

        Debug.Log("Puntuación total actualizada: " + totalScore);
    }
}

