using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public int lives = 3;
    public int score;
    public int maxscore = 0;
    public int coins = 0;
    public int currentLevel;
    public bool hasPrice=false;

    public GameObject pauseCanvas;

    public AudioClip atrasFx;

    public bool isPaused = false;

    [SerializeField] private TextMeshProUGUI currentLevelText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        pauseCanvas.SetActive(false);
        currentLevelText.text = currentLevel.ToString();
    }
    private void Update()
    {
        if(score >maxscore)
        {
            maxscore = score;
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
        if (pauseCanvas == null) pauseCanvas = GameObject.Find("PauseCanvas");
        pauseCanvas?.SetActive(false);

        if (currentLevelText == null) currentLevelText = GameObject.Find("CurrentLevelText")?.GetComponent<TextMeshProUGUI>();
        if (currentLevelText != null) currentLevelText.text = currentLevel.ToString();
    }

    public void PauseisActive()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PauseisNotActive()
    {
        AudioManager.instance.PlaySFX(atrasFx);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void LevelCompleted()
    {
        Save();
        LevelTransitionController.instance.StartTransition(4,2);
    }
    public void LevelSelectorButton()
    {
        Time.timeScale = 1;
        LevelTransitionController.instance.StartTransition(2, 2);
    }
    public void Save()
    {
        Debug.Log("GAME SAVED");
        PlayerPrefs.SetFloat("HighScore", maxscore);
        PlayerPrefs.SetFloat("Score", score);
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.SetInt("Lives", lives);
    }
    public void Load()
    {
        Debug.Log("GAME LOAD");
        maxscore = PlayerPrefs.GetInt("HighScore", 0);
        score = PlayerPrefs.GetInt("Score", 0);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        lives = PlayerPrefs.GetInt("Lives", 0);

        Debug.Log("HighScore " + maxscore);
        Debug.Log("Score " + score);
        Debug.Log("Lives " + lives);
        Debug.Log("Level " + currentLevel);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        Save(); // revisqar com posar player`perfs o q 
        SceneManager.LoadScene("LevelSelector");
    }

}
