using System.Collections;
using UnityEngine;

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
        // Activar la animaci�n de picado (si existe)
        yield return new WaitForSeconds(diveDuration);

        // Determinar la direcci�n de empuje
        Vector2 pushDirection = (player.position - transform.position).normalized;

        // Llamar al m�todo `ApplyPush` en el jugador para actualizar su posici�n objetivo
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ApplyPush(pushDirection, pushDistance);
        }

        Destroy(gameObject);
    }
}