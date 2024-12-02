using UnityEngine;
using TMPro;

public class MetaSceneManager : MonoBehaviour
{

    public TextMeshProUGUI totalScoreText;

    private void Start()
    {
        // Llamamos al método para cargar y mostrar el total de puntos
        DisplayTotalScore();
    }

    // Método para cargar los puntos totales y actualizar el texto
    private void DisplayTotalScore()
    {
        // Obtener el total de puntos guardado en PlayerPrefs
        int totalScore = PlayerPrefs.GetInt("totalScore", 0);

        // Actualizar el texto con el total de puntos
        if (totalScoreText != null)
        {
            totalScoreText.text = "TOTAL SCORE: " + totalScore;
        }
        else
        {
            Debug.LogWarning("No se ha asignado el componente TextMeshProUGUI.");
        }
    }
}
