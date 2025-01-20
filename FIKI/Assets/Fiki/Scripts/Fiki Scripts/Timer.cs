using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Header("Configuraci�n del Temporizador")]
    public float timeRemaining = 300f;
    public bool timerIsRunning = false;
    public PlayerMovement playerMovement;
    [Header("UI")]
    public TextMeshProUGUI timerText;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("No se asign� el TextMeshProUGUI en el campo 'Timer Text' del Inspector.");
            return;
        }

        timerIsRunning = true;
        UpdateTimerDisplay();
        playerMovement = GetComponent<PlayerMovement>();
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
        //Debug.Log($"Actualizando texto: {timeRemaining}");
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString(); 
        }
    }

    private void OnTimerEnd()
    {
        playerMovement.Lose();
        //Debug.Log("�Tiempo agotado!");
        
    }
}
