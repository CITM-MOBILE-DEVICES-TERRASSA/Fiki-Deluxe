using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Header("Configuración del Temporizador")]
    public float timeRemaining = 300f; // Tiempo inicial (editable en el Inspector)
    public bool timerIsRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText; // Referencia al texto del temporizador

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("No se asignó el TextMeshProUGUI en el campo 'Timer Text' del Inspector.");
            return;
        }

        timerIsRunning = true; // Inicia el temporizador
        UpdateTimerDisplay(); // Mostrar el tiempo inicial
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Decrementar tiempo
                UpdateTimerDisplay(); // Actualizar UI
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd();
            }
        }
    }

    private void UpdateTimerDisplay()
    {
        Debug.Log($"Actualizando texto: {timeRemaining}"); // Debug para verificar actualización
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString(); // Actualiza el texto en pantalla
        }
    }

    private void OnTimerEnd()
    {
        Debug.Log("¡Tiempo agotado!");
        // Lógica adicional al finalizar el temporizador
    }
}
