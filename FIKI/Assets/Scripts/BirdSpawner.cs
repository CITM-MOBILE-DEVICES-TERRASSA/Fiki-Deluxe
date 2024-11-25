using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; 
    public Transform player; 
    public float spawnInterval = 3f;
    public float speed = 5f; 
    public float diveDuration = 0.2f; 
    public float pushDistance = 1f; 

    private void Start()
    {
        StartCoroutine(SpawnBird());
    }

    private IEnumerator SpawnBird()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * 10f;
            GameObject bird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);

            Vector2 direction = (player.position - bird.transform.position).normalized;
            //bird.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            bird.GetComponent<EnemyBird>().Initialize(player, speed, diveDuration, pushDistance);
        }
    }
}