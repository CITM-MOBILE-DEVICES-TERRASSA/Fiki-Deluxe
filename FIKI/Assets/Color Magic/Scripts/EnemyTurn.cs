using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : MonoBehaviour
{
    [SerializeField] private float minChance = 0.4f;
    [SerializeField] private float maxChance = 0.6f;
    private float chance;

    public void CalculateChance()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        chance = Random.Range(
            minChance + (currentLevel == 1 ? 0 : currentLevel * 0.05f),
            maxChance + (currentLevel == 1 ? 0 : currentLevel * 0.05f)
        );
    }

    public float GetPrecision() => chance * 100;
    public float GetChance() => chance;
}
