using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject Puntuacion_Canvas;

    public AudioClip atrasFx;

    void Start()
    {

    }

    public void Minigame1()
    {
        LevelTransitionController.instance.StartTransition(2, 0.5f);
    }

    public void Minigame2()
    {
        LevelTransitionController.instance.StartTransition(5, 0.5f);
    }

    public void Minigame3()
    {
        LevelTransitionController.instance.StartTransition(9, 0.5f);
    }

    public void Minigame4()
    {
        LevelTransitionController.instance.StartTransition(11, 0.5f);
    }

    public void Minigame5()
    {
        // Implementar l�gica para Minigame5
    }

    public void Minigame6()
    {
        // Implementar l�gica para Minigame6
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
            AudioManager.instance.PlaySFX(atrasFx);
            Puntuacion_Canvas.SetActive(false);
        }
    }
}

