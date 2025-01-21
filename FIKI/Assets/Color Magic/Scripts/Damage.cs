using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    [SerializeField] private Text textDamage;

    private float baseDamage = 50;
    private float totalDamage = 0;
    private float multiplier = 1;
    private ColorPrecision colorPrecision;
    private ButtonManager buttonManager;
    private EnemyTurn enemyTurn;

    private void Start()
    {
        colorPrecision = GameObject.FindObjectOfType<ColorPrecision>();
        buttonManager = GameObject.FindObjectOfType<ButtonManager>();
        enemyTurn = GameObject.FindObjectOfType<EnemyTurn>();
    }

    private float CalculateDamage(bool isEnemy) 
    {
        float precision;

        if (isEnemy)
            precision = enemyTurn.GetPrecision();
        else
            precision = colorPrecision.GetPrecision();

        ScoreManager.instance.SetScore((int)precision);

        if(buttonManager.attack || isEnemy)
        {
            float damage = 0;
            switch (precision)
            {
                case < 50:
                    damage = 0;
                    break;
                case < 60:
                    damage = (baseDamage - (baseDamage * 0.1f)) * multiplier;
                    break;
                case < 80:
                    damage = (baseDamage + (baseDamage * 0.15f)) * multiplier;
                    baseDamage += 5;
                    break;
                case < 98:
                    damage = (baseDamage + (baseDamage * 0.35f)) * multiplier;
                    baseDamage += 10;
                    break;
                case <= 100:
                    damage = (baseDamage + (baseDamage * 1.0f)) * multiplier;
                    baseDamage += 15;
                    break;
            }
            multiplier = 1;
            return damage;
        }
        if(buttonManager.defense)
        {
            switch (precision)
            {
                case < 60:
                    break;
                case < 80:
                    multiplier = 1.25f;
                    break;
                case < 98:
                    multiplier = 1.5f;
                    break;
                case <= 100:
                    multiplier = 2;
                    break;
            }
        }
       return 0;
    }

    public void SetDamage(bool isEnemy = false)
    {
        totalDamage = CalculateDamage(isEnemy);
        textDamage.text = "Damage: " + totalDamage.ToString();
    }

    public float GetTotalDamage() => totalDamage;
}