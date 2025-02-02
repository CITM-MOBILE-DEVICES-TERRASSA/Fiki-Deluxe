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
    public TextMeshProUGUI FelixJumpText;
    public TextMeshProUGUI ColorsMagicText;
    public TextMeshProUGUI VacacionesZaharaText;
    public TextMeshProUGUI TotalScoreText;

    [HideInInspector] public int Game1Score { get; private set; } //(Fiki)
    [HideInInspector] public int Game2Score { get; private set; } //(Jumping Jack)
    [HideInInspector] public int Game3Score { get; private set; } //(Felix Jump)
    [HideInInspector] public int Game4Score { get; private set; } //(Colors Magic)
    [HideInInspector] public int Game5Score { get; private set; } //(Vacaciones Zahara)

    private GameObject scoreCanvas;

    private void Awake()
    {
        //Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
        if (scene.name == "LobbyScene" || scene.name == "LevelSelector")
        {
            UpdateGame1Score(PlayerPrefs.GetInt("FikiScore", 0));
            UpdateGame2Score(PlayerPrefs.GetInt("JumpingJackScore", 0));
            UpdateGame3Score(PlayerPrefs.GetInt("FelixJumpScore", 0));
            UpdateGame4Score(PlayerPrefs.GetInt("ColorsMagicScore", 0));
            UpdateGame5Score(PlayerPrefs.GetInt("VacacionesZaharaScore", 0));

            AssignReferences();
            UpdateScoreTotal();

            if (scoreCanvas != null)
                scoreCanvas.SetActive(false);
        }
    }

    //private void Update()
    //{
    //    ShowScoreTexts();
    //}
        
    public void UpdateGame1Score(int newScore)
    {
        Game1Score = newScore;
        Debug.Log("Puntuacion de FIKI actualizada: " + Game1Score);
        UpdateScoreTotal();
    }
        
    public void UpdateGame2Score(int newScore)
    {
        Game2Score = newScore;
        Debug.Log("Puntuacion de Jumping Jack actualizada: " + Game2Score);
        UpdateScoreTotal();
    }
    public void UpdateGame3Score(int newScore)
    {
        Game3Score = newScore;
        Debug.Log("Puntuacion de Felix Jump actualizada: " + Game3Score);
        UpdateScoreTotal();
    }

    public void UpdateGame4Score(int newScore)
    {
        Game4Score = newScore;
        Debug.Log("Puntuacion de Colors Magic actualizada: " + Game4Score);
        UpdateScoreTotal();
    }

    public void UpdateGame5Score(int newScore)
    {
        Game5Score = newScore;
        Debug.Log("Puntuacion de Vacaciones Zahara actualizada: " + Game5Score);
        UpdateScoreTotal();
    }

    private void AssignReferences()
    {
        Dictionary<string, Action<TextMeshProUGUI>> uiElements = new Dictionary<string, Action<TextMeshProUGUI>>()
        {
            { "FikiScoreText", (component) => FikiText = component },
            { "JumpingJackScoreText", (component) => JumpingJackText = component },
            { "FelixJumpScoreText", (component) => FelixJumpText = component },
            { "ColorsMagicScoreText", (component) => FelixJumpText = component },
            { "VacacionesZaharaScoreText", (component) => FelixJumpText = component },
            { "TotalScoreText", (component) => TotalScoreText = component }
        };

        foreach (var element in uiElements)
        {
            GameObject uiObject = GameObject.Find(element.Key);
            if (uiObject != null)
            {
                element.Value(uiObject.GetComponent<TextMeshProUGUI>());
            }
            else
            {
                Debug.LogError($"No se encontro el objeto {element.Key} en la escena.");
            }
        }

        scoreCanvas = GameObject.Find("Puntuacion_Canvas");
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
        if (FelixJumpText != null)
        {
            FelixJumpText.text = "FelixJump: " + Game3Score;
        }
        if (ColorsMagicText != null)
        {
            ColorsMagicText.text = "Colors Magic: " + Game4Score;
        }
        if (VacacionesZaharaText != null)
        {
            VacacionesZaharaText.text = "Vacaciones Zahara: " + Game5Score;
        }
        if (TotalScoreText != null)
        {
            TotalScoreText.text = "Total Score: " + totalScore;
        }

        Debug.Log("Puntuacion total actualizada: " + totalScore);
    }
}

