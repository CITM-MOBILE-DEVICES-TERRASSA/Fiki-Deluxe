using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    #region Singleton
    private static GameplayManager _instance;
    public static GameplayManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    public GameObject gameOverScreen;

    int currentScore = 0;

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("Lobby");
        int totalScore = PlayerPrefs.GetInt("totalScore", 0);
        PlayerPrefs.SetInt("totalScore", totalScore + currentScore);
        Time.timeScale = 1.0f;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        ResetTimer();
    }

    public void LoadMetaLobby()
    {
        SceneManager.LoadScene("JumpingJack-Meta1");
        Time.timeScale = 1.0f;
    }

    public void LoadNextLevel()
    {
        // Guardamos el puntaje acumulado del nivel antes de cambiar de escena
        Score score = FindObjectOfType<Score>();
        if (score != null)
        {
            score.AddScoreToTotal();  // A�adimos la puntuaci�n del nivel al total
        }

        // Reiniciamos el temporizador para el siguiente nivel
        ResetTimer();

        // Cargar el siguiente nivel
        if (SceneManager.GetActiveScene().name == "JumpingJack-Level1")
        {
            SceneManager.LoadScene("JumpingJack-Level2");
            Time.timeScale = 1.0f;
        }
        else if (SceneManager.GetActiveScene().name == "JumpingJack-Level2")
        {
            SceneManager.LoadScene("JumpingJack-Level3");
            Time.timeScale = 1.0f;
        }
    }

    private void ResetTimer()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.ResetTimer(); // Reinicia el temporizador
        }
    }

    public void PauseTime()
    {
        Time.timeScale = 0.0f;
    }

    public void UnpauseTime()
    {
        Time.timeScale = 1.0f;
    }
}
