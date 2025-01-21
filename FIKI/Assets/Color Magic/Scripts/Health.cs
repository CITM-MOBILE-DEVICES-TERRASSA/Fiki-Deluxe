using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private float initialHealth = 300;
    private float currentHealth;
    ColorsMagic ColorsMagic;

    [SerializeField] private HealthBar healthBar;
    private Entity entity;
    public AudioSource winFx;
    public AudioSource loseFx;
    private void Awake()
    {
        ResetHealth();
    }

    private void Start()
    {
        entity = GameObject.FindObjectOfType<Entity>();
        if (entity.isEnemy)
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            initialHealth = initialHealth + (currentLevel * 25);
            ResetHealth();
        }
        ColorsMagic = FindAnyObjectByType<ColorsMagic>();
    }

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        float percentage = (currentHealth / initialHealth);
        Debug.Log(percentage);
        healthBar.SetBarValue(percentage);
        if(currentHealth <= 0)
        {
            End();
        }
    }

    private void ResetHealth()
    {
        currentHealth = initialHealth;
    }

    private void End()
    {
        if (entity.isEnemy)
        {
            loseFx.Play();
            ColorsMagic.ChangeToScene("Defeat");
        }
        else
        {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
            if (currentLevel >= levelsUnlocked)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", currentLevel + 1);
                PlayerPrefs.Save();
            }
            winFx.Play();
            ColorsMagic.ChangeToScene("Victory");
        }
    }
}