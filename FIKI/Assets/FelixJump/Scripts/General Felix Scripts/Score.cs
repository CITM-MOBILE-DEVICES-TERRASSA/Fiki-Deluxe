using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private static Score instance;
    public static Score Instance => instance;

    [SerializeField] private int autumnScore;
    [SerializeField] private int halloweenScore;
    [SerializeField] private int springScore;
    [SerializeField] private int winterScore;
    [SerializeField] private int summerScore;

    [SerializeField] private int maxAutumnScore;
    [SerializeField] private int maxHalloweenScore;
    [SerializeField] private int maxSpringScore;
    [SerializeField] private int maxWinterScore;
    [SerializeField] private int maxSummerScore;

    [SerializeField] private int maxTotalScore;

    [SerializeField] private float scoreTimer = 0f;
    [SerializeField] private bool addScore = true;

    [SerializeField] private TextMeshProUGUI autumnMaxScoreText;
    [SerializeField] private TextMeshProUGUI halloweenMaxScoreText;
    [SerializeField] private TextMeshProUGUI springMaxScoreText;
    [SerializeField] private TextMeshProUGUI winterMaxScoreText;
    [SerializeField] private TextMeshProUGUI summerMaxScoreText;

    private void Awake()
    {

        maxAutumnScore = PlayerPrefs.GetInt("MaxAutumnScore", 0);
        maxHalloweenScore = PlayerPrefs.GetInt("MaxHalloweenScore", 0);
        maxSpringScore = PlayerPrefs.GetInt("MaxSpringScore", 0);
        maxWinterScore = PlayerPrefs.GetInt("MaxWinterScore", 0);
        maxSummerScore = PlayerPrefs.GetInt("MaxSummerScore", 0);
        maxTotalScore = PlayerPrefs.GetInt("MaxTotalScore", 0);

        UpdateMaxScoreTexts();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Opcional: si deseas mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "GameScene" && addScore)
        {
            scoreTimer += Time.deltaTime;
            if (scoreTimer >= 1f)
            {
                AddScore(1);
                scoreTimer = 0f;
            }
        }
        else
            scoreTimer = 0f;
    }

    public void AddScore(int value)
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        bool maxScoreUpdated = false;

        switch (currentScene)
        {
            case "AutumnLevel":
                autumnScore += value;
                if (autumnScore > maxAutumnScore)
                {
                    maxAutumnScore = autumnScore;
                    PlayerPrefs.SetInt("MaxAutumnScore", maxAutumnScore);
                    maxScoreUpdated = true;
                }
                break;
            case "HalloweenLevel":
                halloweenScore += value;
                if (halloweenScore > maxHalloweenScore)
                {
                    maxHalloweenScore = halloweenScore;
                    PlayerPrefs.SetInt("MaxHalloweenScore", maxHalloweenScore);
                    maxScoreUpdated = true;
                }
                break;
            case "SpringLevel":
                springScore += value;
                if (springScore > maxSpringScore)
                {
                    maxSpringScore = springScore;
                    PlayerPrefs.SetInt("MaxSpringScore", maxSpringScore);
                    maxScoreUpdated = true;
                }
                break;
            case "WinterLevel":
                winterScore += value;
                if (winterScore > maxWinterScore)
                {
                    maxWinterScore = winterScore;
                    PlayerPrefs.SetInt("MaxWinterScore", maxWinterScore);
                    maxScoreUpdated = true;
                }
                break;
            case "SummerLevel":
                summerScore += value;
                if (summerScore > maxSummerScore)
                {
                    maxSummerScore = summerScore;
                    PlayerPrefs.SetInt("MaxSummerScore", maxSummerScore);
                    maxScoreUpdated = true;
                }
                break;
        }

        if (maxScoreUpdated)
        {
            UpdateMaxTotalScore();
            UpdateMaxScoreTexts();
        }

        PlayerPrefs.Save();
    }


    private void UpdateMaxScoreTexts()
    {
        if (autumnMaxScoreText != null)
        {
            autumnMaxScoreText.text = "Max Score: " + maxAutumnScore;
        }
        if (halloweenMaxScoreText != null)
        {
            halloweenMaxScoreText.text = "Max Score: " + maxHalloweenScore;
        }
        if (springMaxScoreText != null)
        {
            springMaxScoreText.text = "Max Score: " + maxSpringScore;
        }
        if (winterMaxScoreText != null)
        {
            winterMaxScoreText.text = "Max Score: " + maxWinterScore;
        }
        if (summerMaxScoreText != null)
        {
            summerMaxScoreText.text = "Max Score: " + maxSummerScore;
        }
    }
    

    public void UpdateScoreWithBonus(string levelName, int bonusScore)
    {
        bool maxScoreUpdated = false;

        switch (levelName)
        {
            case "AutumnLevel":
                autumnScore += bonusScore;
                if (autumnScore > maxAutumnScore)
                {
                    maxAutumnScore = autumnScore;
                    PlayerPrefs.SetInt("MaxAutumnScore", maxAutumnScore);
                    maxScoreUpdated = true;
                }
                break;
            case "HalloweenLevel":
                halloweenScore += bonusScore;
                if (halloweenScore > maxHalloweenScore)
                {
                    maxHalloweenScore = halloweenScore;
                    PlayerPrefs.SetInt("MaxHalloweenScore", maxHalloweenScore);
                    maxScoreUpdated = true;
                }
                break;
            case "SpringLevel":
                springScore += bonusScore;
                if (springScore > maxSpringScore)
                {
                    maxSpringScore = springScore;
                    PlayerPrefs.SetInt("MaxSpringScore", maxSpringScore);
                    maxScoreUpdated = true;
                }
                break;
            case "WinterLevel":
                winterScore += bonusScore;
                if (winterScore > maxWinterScore)
                {
                    maxWinterScore = winterScore;
                    PlayerPrefs.SetInt("MaxWinterScore", maxWinterScore);
                    maxScoreUpdated = true;
                }
                break;
            case "SummerLevel":
                summerScore += bonusScore;
                if (summerScore > maxSummerScore)
                {
                    maxSummerScore = summerScore;
                    PlayerPrefs.SetInt("MaxSummerScore", maxSummerScore);
                    maxScoreUpdated = true;
                }
                break;
        }

        if (maxScoreUpdated)
        {
            UpdateMaxTotalScore();
            UpdateMaxScoreTexts();
        }

        PlayerPrefs.Save();
    }

    public void HandleLevelCompletion(string currentScene, float countdownTime)
    {
        int bonusScore = Mathf.CeilToInt(countdownTime) * 10;
        UpdateScoreWithBonus(currentScene, bonusScore);
        Debug.Log("Bonus Score: " + bonusScore);

        TextMeshProUGUI highScoreText = CylinderController.instance.endPanel.transform.Find("EndPanelHighScoreText").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currentScoreText = CylinderController.instance.endPanel.transform.Find("EndPanelCurrentScoreText").GetComponent<TextMeshProUGUI>();

        highScoreText.text = "High Score: " + GetHighScore(currentScene).ToString();
        currentScoreText.text = "Score: " + GetScore(currentScene).ToString();
    }

    private void UpdateMaxTotalScore()
    {
        maxTotalScore = maxAutumnScore + maxHalloweenScore + maxSpringScore + maxWinterScore + maxSummerScore;
        PlayerPrefs.SetInt("MaxTotalScore", maxTotalScore);
        GeneralScore.Instance.UpdateOtherGameScore(maxTotalScore);
    }

    public int GetScore(string levelName)
    {
        switch (levelName)
        {
            case "AutumnLevel":
                return autumnScore;
            case "HalloweenLevel":
                return halloweenScore;
            case "SpringLevel":
                return springScore;
            case "WinterLevel":
                return winterScore;
            case "SummerLevel":
                return summerScore;
            default:
                return 0;
        }
    }
    
    public int GetHighScore(string levelName)
    {
        switch (levelName)
        {
            case "AutumnLevel":
                return maxAutumnScore;
            case "HalloweenLevel":
                return maxHalloweenScore;
            case "SpringLevel":
                return maxSpringScore;
            case "WinterLevel":
                return maxWinterScore;
            case "SummerLevel":
                return maxSummerScore;
            default:
                return 0;
        }
    }
    public int GetTotalScore()
    {
        return maxTotalScore;
    }
}