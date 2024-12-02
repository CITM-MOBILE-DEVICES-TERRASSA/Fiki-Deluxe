using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class EnemyBird : MonoBehaviour
{
    private Vector3 playerPosition;
    private float speed;
    private float diveDuration;
    private float pushDistance;
    private bool diving;
    private Vector3 flyDirection;
    private Vector3 originalScale;
    private Animator animator;

    public void Initialize(Transform playerTarget, float birdSpeed, float diveTime, float pushDist)
    {
        playerPosition = playerTarget.position;
        speed = birdSpeed;
        diveDuration = diveTime;
        pushDistance = pushDist;
        originalScale = transform.localScale;
        flyDirection = (playerPosition - transform.position).normalized;
        animator = GetComponent<Animator>();

        if (flyDirection.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
    }

    private void Update()
    {
        if (!diving)
        {
            transform.position += flyDirection * speed * Time.deltaTime;
            
            if (Vector3.Distance(transform.position, playerPosition) > 20f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !diving)
        {
            Debug.Log("¡Colisión con jugador detectada!");
            diving = true;
            animator.Play("Bird_Attack");
            StartCoroutine(DiveAndPush(other.transform));
        }
    }

    private IEnumerator DiveAndPush(Transform player)
    {
        Debug.Log("Iniciando DiveAndPush");
        var playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("PlayerMovement encontrado");
            playerMovement.enabled = false;
        }

        // Guardamos la dirección de empuje ANTES del dive
        Vector2 pushDir = flyDirection;
        Debug.Log($"Dirección de empuje calculada: {pushDir}");

        // Calcular la dirección hacia el jugador antes de zambullirse
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z); // Mirar izquierda
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z); // Mirar derecha
        }

        // Fase 1: Zambullida
        Vector3 startPos = transform.position;
        Vector3 diveTarget = player.position;
        float elapsed = 0f;
        float diveTime = diveDuration * 0.3f;

        while (elapsed < diveTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / diveTime;
            transform.position = Vector3.Lerp(startPos, diveTarget, t);
            yield return null;
        }

        // Fase 2: Empuje
        Debug.Log("Iniciando empuje");
        if (playerMovement != null)
        {
            // Usamos la dirección guardada para calcular la posición objetivo
            Vector3 targetPosition = player.position + (Vector3)(pushDir * pushDistance * 2f);
            
            Debug.Log($"Posición actual del jugador: {player.position}");
            Debug.Log($"Dirección de empuje: {pushDir}");
            Debug.Log($"Distancia de empuje: {pushDistance}");
            Debug.Log($"Posición objetivo calculada: {targetPosition}");

            // Movimiento directo
            float pushDuration = 0.2f;
            float pushElapsed = 0f;
            Vector3 playerStartPos = player.position;

            while (pushElapsed < pushDuration)
            {
                pushElapsed += Time.deltaTime;
                float t = pushElapsed / pushDuration;
                player.position = Vector3.Lerp(playerStartPos, targetPosition, t);
                yield return null;
            }

            // Aseguramos la posición final
            player.position = targetPosition;

            // Si hay tilemap, actualizamos la posición en la grid
            if (playerMovement.tilemap != null)
            {
                Vector3Int newGridPos = playerMovement.tilemap.WorldToCell(targetPosition);
                playerMovement.SetGridPosition(newGridPos);
            }
        }

        yield return new WaitForSeconds(0.1f);

        // Fase 3: Recuperación
        elapsed = 0f;
        float recoveryTime = diveDuration * 0.3f;
        Vector3 recoveryPos = transform.position + flyDirection * pushDistance;

        while (elapsed < recoveryTime)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / recoveryTime;
            transform.position = Vector3.Lerp(transform.position, recoveryPos, t);
            animator.SetBool("Attacking", false);
            yield return null;
        }

        if (playerMovement != null)
        {
            yield return new WaitForFixedUpdate();
            playerMovement.enabled = true;
            Debug.Log("PlayerMovement reactivado");
        }

        if (flyDirection.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        diving = false;
        GetComponent<Collider2D>().enabled = false;
    }
}