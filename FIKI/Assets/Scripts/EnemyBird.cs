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

        flyDirection = (playerPosition - transform.position).normalized;
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

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        Tilemap tilemap = playerMovement.tilemap;

        if (tilemap != null)
        {
            Vector2 pushDirection = (player.position - transform.position).normalized;

            Vector3Int playerGridPosition = tilemap.WorldToCell(player.position);

            Vector3Int pushOffset = new Vector3Int(
                Mathf.RoundToInt(pushDirection.x * pushDistance),
                Mathf.RoundToInt(pushDirection.y * pushDistance),
                0
            );

            Vector3Int newGridPosition = playerGridPosition + pushOffset;

            if (tilemap.HasTile(newGridPosition))
            {
                Vector3 targetWorldPosition = tilemap.GetCellCenterWorld(newGridPosition);
                player.position = targetWorldPosition;
            }
        }

        Destroy(gameObject);
    }
}