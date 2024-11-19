using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Header("Configuración del Temporizador")]
    public float timeRemaining = 300f;
    public bool timerIsRunning = false;

    [Header("UI")]
    public TextMeshProUGUI timerText;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("No se asignó el TextMeshProUGUI en el campo 'Timer Text' del Inspector.");
            return;
        }

        timerIsRunning = true;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; 
                UpdateTimerDisplay();
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
        Debug.Log($"Actualizando texto: {timeRemaining}");
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString(); 
        }
    }

    private void OnTimerEnd()
    {
        Debug.Log("¡Tiempo agotado!");
        
    }
}
