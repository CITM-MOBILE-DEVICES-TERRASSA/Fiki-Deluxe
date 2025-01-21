using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool attack = false;
    public bool defense = false;
    public AudioSource audiosource;
    ColorsMagic ColorsMagic;
    private void Awake()
    {
        ColorsMagic = FindAnyObjectByType<ColorsMagic>();
    }

    public void atackButton()
    {
        //audiosource.Play();
        attack = true;
        defense = false;
    }

    public void defenseButton()
    {
        //audiosource.Play();
        defense = true;
        attack = false;
    }

    public void mainMenu()
    {
        ColorsMagic.ChangeToScene("LevelSelector");
    }
}