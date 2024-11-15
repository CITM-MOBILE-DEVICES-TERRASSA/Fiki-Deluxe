using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject Puntuacion_Canvas; 

    void Start()
    {

    }

    public void Minigame1()
    {
        LevelTransitionController.instance.StartTransition(2, 2);
    }

    public void Minigame2()
    {
        // Implementar lógica para Minigame2
    }

    public void Minigame3()
    {
        // Implementar lógica para Minigame3
    }

    public void Minigame4()
    {
        // Implementar lógica para Minigame4
    }

    public void Minigame5()
    {
        // Implementar lógica para Minigame5
    }

    public void Minigame6()
    {
        // Implementar lógica para Minigame6
    }

    public void Puntuacion()
    {
        
        if (Puntuacion_Canvas != null)
        {
            Puntuacion_Canvas.SetActive(true);
        }
    }

    public void Atras()
    {
        LevelTransitionController.instance.StartTransition(1, 2);
    }

    public void AtrasPuntuacion()
    {
        
        if (Puntuacion_Canvas != null)
        {
            Puntuacion_Canvas.SetActive(false);
        }
    }
}

