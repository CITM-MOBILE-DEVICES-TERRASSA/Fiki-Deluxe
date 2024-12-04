using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform point; // The spawn point transform
    }

    [Header("Snake Prefab")]
    public GameObject food;

    [Header("Spawn Settings")]
    public List<SpawnPoint> spawnPoints; // List of spawn points with their directions

    void Start()
    {
        SpawnFood();
    }

    void SpawnFood()
    {
        if (spawnPoints.Count == 0 || food == null)
        {
            Debug.LogWarning("No spawn points or prefab assigned!");
            return;
        }

        // Select a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Count);
        SpawnPoint selectedSpawn = spawnPoints[randomIndex];

        if (selectedSpawn.point == null)
        {
            Debug.LogWarning($"Spawn point at index {randomIndex} is not assigned!");
            return;
        }

        // Instantiate the snake at the selected spawn point
        GameObject snake = Instantiate(food, selectedSpawn.point.position, Quaternion.identity);
    }
}
