using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private string timeTextObjectName = "Time"; // Nombre del objeto del texto
    private Text timeText;
    public float totalTime = 90;
    public float totalTimeReset = 90;

    private void Start()
    {
        FindTimeText(); // Encuentra el objeto de texto al iniciar la escena
        ResetTimer();   // Reinicia el temporizador
    }

    private void Update()
    {
        if (timeText == null)
        {
            FindTimeText();
        }
        else { UpdateTimeDisplay(); }
    }

    private void UpdateTimeDisplay()
    {
        totalTime -= Time.deltaTime;

        if (totalTime < 0) {totalTime = 0;}
        timeText.text = totalTime.ToString("F2"); // 2 decimales
    }

    public void ResetTimer()
    {
        totalTime = totalTimeReset;
        if (timeText != null)
        {
            timeText.text = totalTime.ToString("F2");
        }
    }

    private void FindTimeText()
    {
        GameObject textObject = GameObject.Find(timeTextObjectName);

        if (textObject != null)
        {
            timeText = textObject.GetComponent<Text>();
        }
        else{Debug.LogWarning("No se pudo encontrar el objeto Text con el nombre: " + timeTextObjectName);}
    }
}
