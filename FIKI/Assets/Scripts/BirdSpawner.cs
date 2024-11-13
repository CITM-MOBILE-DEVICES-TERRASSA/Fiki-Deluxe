using System.Collections;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; // Prefab del pájaro
    public Transform player; // Transform del jugador
    public float spawnInterval = 3f; // Intervalo de spawn del pájaro
    public float speed = 5f; // Velocidad del pájaro
    public float diveDuration = 1.0f; // Duración de la animación de picado
    public float pushDistance = 1f; // Distancia de empuje del jugador al ser golpeado

    private void Start()
    {
        StartCoroutine(SpawnBird());
    }

    private IEnumerator SpawnBird()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Posición random fuera de la pantalla
            Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * 10f;
            GameObject bird = Instantiate(birdPrefab, spawnPos, Quaternion.identity);

            // Calcular ángulo hacia el jugador
            Vector2 direction = (player.position - bird.transform.position).normalized;
            bird.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Asignar al script de movimiento del pájaro
            bird.GetComponent<EnemyBird>().Initialize(player, speed, diveDuration, pushDistance);
        }
    }
}
