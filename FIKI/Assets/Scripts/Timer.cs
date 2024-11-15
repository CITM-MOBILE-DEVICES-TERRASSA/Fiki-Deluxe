using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esto si usas UI
using TMPro;
public class TimerScript : MonoBehaviour
{
    private float timeRemaining = 0f; // Tiempo total del temporizador
    public bool timerIsRunning = false;
    [SerializeField] private TextMeshProUGUI timerText; // Arrastra un Text de UI aquí en el Inspector

    void Start()
    {
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                // Aquí puedes añadir cualquier acción que quieras realizar cuando el tiempo se agote
                Debug.Log("¡Tiempo agotado!");
            }
        }
        if (timerText)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
        }
        // Actualiza el texto del temporizador
        
    }
}
