using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyBird : MonoBehaviour
{
    private Vector3 playerPosition;
    private float speed;
    private float diveDuration;
    private float pushDistance;
    private bool diving = false;
    private Vector3 flyDirection;

    public void Initialize(Transform playerTarget, float birdSpeed, float diveTime, float pushDist)
    {
        playerPosition = playerTarget.position;
        speed = birdSpeed;
        diveDuration = diveTime;
        pushDistance = pushDist;

        // Calcular la direcci�n inicial hacia el jugador
        flyDirection = (playerPosition - transform.position).normalized;
    }

    private void Update()
    {
        // Movimiento hacia el jugador o en la direcci�n inicial
        if (!diving)
        {
            transform.position += flyDirection * speed * Time.deltaTime;

            // Despawnear el p�jaro si est� demasiado lejos del jugador (o de la escena)
            if (Vector3.Distance(transform.position, playerPosition) > 20f) // Cambia 20f seg�n tu escena
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {   
        if (other.gameObject.CompareTag("Player") && !diving)
        {
            diving = true;
            StartCoroutine(DiveAndPush(other.transform));
        }
    }

    private IEnumerator DiveAndPush(Transform player)
    {
        yield return new WaitForSeconds(diveDuration);

        // Obtener la referencia al Tilemap y su grilla
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Tilemap tilemap = playerMovement.tilemap;

        if (tilemap != null)
        {
            // Determinar la dirección del empuje
            Vector2 pushDirection = (player.position - transform.position).normalized;

            // Convertir la posición actual del jugador al espacio de la grilla
            Vector3Int playerGridPosition = tilemap.WorldToCell(player.position);

            // Calcular la nueva posición en la grilla basada en la dirección y distancia del empuje
            Vector3Int pushOffset = new Vector3Int(
                Mathf.RoundToInt(pushDirection.x * pushDistance),
                Mathf.RoundToInt(pushDirection.y * pushDistance),
                0
            );

            Vector3Int newGridPosition = playerGridPosition + pushOffset;

            // Verificar que el nuevo tile es válido antes de mover al jugador
            if (tilemap.HasTile(newGridPosition))
            {
                Vector3 targetWorldPosition = tilemap.GetCellCenterWorld(newGridPosition);
                player.position = targetWorldPosition;
            }
        }

        Destroy(gameObject);
    }
}