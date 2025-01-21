using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{
    [SerializeField] private float timeleft = 15;
    [SerializeField] private Text timeText;
    [SerializeField] private Button skipButton; // Add SKIP button
    private float remainingTime;
    private bool timerIsRunning = true;

    [SerializeField] private List<Entity> entities = new List<Entity>(2);    
    
    private ColorGenerator colorGenerator;
    private ColorSliders colorSliders;
    private ButtonManager butonManager;

    public AudioSource skipButtonFx;
    private void Awake()
    {
        skipButton.gameObject.SetActive(false); // Initialize SKIP button and set it to inactive
    }

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        remainingTime = timeleft - (currentLevel - 1);
        butonManager = GameObject.FindObjectOfType<ButtonManager>();
        colorGenerator = GameObject.FindObjectOfType<ColorGenerator>();
        colorSliders = GameObject.FindObjectOfType<ColorSliders>();
        colorSliders.EnableSliders(false);
    }

    private void Update()
    {
        if (butonManager.attack || butonManager.defense)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                remainingTime = Mathf.Max(remainingTime, 0);
                
                colorSliders.EnableSliders(true);
                skipButton.gameObject.SetActive(true); 
            }
            else
            {
                if (timerIsRunning)
                {
                    timerIsRunning = false;
                }
            }

            if (!timerIsRunning)
            {
                foreach (Entity entity in entities)
                {
                    if (entity.isEnemy)
                        entity.GetDamage().SetDamage(true);
                    else
                        entity.GetDamage().SetDamage();

                    entity.Attack();
                }
                colorGenerator.GenerateRandomRGBColor();
                ResetTimer();
                timerIsRunning = true;
                colorSliders.EnableSliders(false);
                skipButton.gameObject.SetActive(false); 
            }
            timeText.text = remainingTime.ToString("F2");
        }
    }

    public void ResetTimer()
    {
        remainingTime = timeleft;
        butonManager.attack = false;
        butonManager.defense = false;
    }

    private void SkipTimer() 
    {
        skipButtonFx.Play();
        remainingTime = 0;
        timerIsRunning = false;
    }
}