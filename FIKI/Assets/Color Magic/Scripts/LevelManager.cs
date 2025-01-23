using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Button[] levelButtons;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    ColorsMagic ColorsMagic;
    
    void Start()
    {
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelsUnlocked)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                levelButtons[i].interactable = true;
            }
        }

        levelText.text = "Current Level " + PlayerPrefs.GetInt("LevelsUnlocked", 1);

        ColorsMagic = FindAnyObjectByType<ColorsMagic>();
    }

    public void StartLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameP");
       // ColorsMagic.ChangeToScene("GameP");
    }
    public void BackButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}