using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleVictory : MonoBehaviour
{
    [SerializeField]
    GameObject victoryScreen;

    [SerializeField]
    GameObject nextLevelButton;

    // Referencia al script Score
    private Score scoreScript;

    private void Start()
    {
        // Obtén la referencia al script Score
        scoreScript = FindObjectOfType<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("si");
            victoryScreen.SetActive(true);
            GameplayManager.Instance.PauseTime();

            // Llama a AddScoreToTotal para sumar la puntuación del nivel al total en el lobby
            if (scoreScript != null)
            {
                scoreScript.AddScoreToTotal();
            }

            // Muestra el botón para el siguiente nivel si no es el último nivel
            if (SceneManager.GetActiveScene().name != "Level3")
            {
                nextLevelButton.SetActive(true);
            }
            else
            {
                nextLevelButton.SetActive(false);
            }
        }
    }
}
