using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public Transform point; // The spawn point transform
        public Vector3 direction; // The direction specific to this spawn point
    }

    [Header("Snake Prefab")]
    public GameObject snakePrefab;

    [Header("Spawn Settings")]
    public List<SpawnPoint> spawnPoints; // List of spawn points with their directions
    public float spawnCooldown = 2f;

    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = spawnCooldown;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            SpawnSnake();
            cooldownTimer = spawnCooldown;
        }
    }

    void SpawnSnake()
    {
        if (spawnPoints.Count == 0 || snakePrefab == null)
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
        GameObject snake = Instantiate(snakePrefab, selectedSpawn.point.position, Quaternion.identity);

        // Assign the direction from the selected spawn point
        Snake snakeController = snake.GetComponent<Snake>();
        if (snakeController != null)
        {
            snakeController.SetDirection(selectedSpawn.direction);
        }
    }
}
