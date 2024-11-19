using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;           // Tilemap que contiene todos los tiles
    public float moveSpeed = 10.0f;   // Velocidad del jugador

    private Vector3Int gridPosition;  // Posici�n actual del jugador en la cuadr�cula
    private Vector3 targetWorldPosition;
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    private GameObject winScreen;
    private GameObject gameOverScreen;

    void Start()
    {
        gridPosition = tilemap.WorldToCell(transform.position); // Calcula la posici�n inicial en la cuadr�cula
        AlignToGrid(); // Alinea al jugador al centro de la celda
        winScreen = GameObject.Find("winwin");
        gameOverScreen = GameObject.Find("gameOver");

        if (winScreen) winScreen.SetActive(false);
        if (gameOverScreen) gameOverScreen.SetActive(false);
    }

    public void SetGridPosition(Vector3Int newGridPosition)
    {
        gridPosition = newGridPosition;
        AlignToGrid(); // Asegura que el jugador se alinee correctamente
    }

    void Update()
    {
        if (!isMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                endMousePosition = Input.mousePosition;
                DetectSwipeDirection();
            }
        }

        if (isMoving)
        {
            MovePlayer();
        }

        CheckPlayerOnWater();

        if (Manager.instance.lives <= 0)
        {
            Lose();
        }    
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDelta = endMousePosition - startMousePosition;

        // Detecta solo si el movimiento es lo suficientemente largo
        if (swipeDelta.magnitude > 50)
        {
            swipeDelta.Normalize();

            Vector3Int direction = Vector3Int.zero;

            // Determina la direcci�n seg�n el swipe
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    direction = Vector3Int.right;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    transform.localScale = new Vector3(3, -3, 3);
                }
                else
                {
                    direction = Vector3Int.left;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    transform.localScale = new Vector3(3, 3, 3);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    direction = Vector3Int.up;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.localScale = new Vector3(3, 3, 3);
                }
                else
                {
                    direction = Vector3Int.down;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    transform.localScale = new Vector3(3, 3, 3);
                }
            }

            // Calcula la nueva posici�n en la cuadr�cula
            Vector3Int newGridPosition = gridPosition + direction;

            if (!IsMovementBlocked(newGridPosition))
            {
                gridPosition = newGridPosition;
                targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                isMoving = true;
                return;
            }
        }
    }

    private bool IsMovementBlocked(Vector3Int newGridPosition)
    {
        // Check if the new position is within the bounds of the tilemap
        if (!tilemap.cellBounds.Contains(newGridPosition))
        {
            return true;
        }

        // Check if the new position has a tile
        if (!tilemap.HasTile(newGridPosition))
        {
            return true;
        }

        TileBase targetTile = tilemap.GetTile(newGridPosition);

        // Check if the target tile is a specific type that blocks movement
        if (targetTile != null && (targetTile.name == "WaterDark_0" || targetTile.name == "SomeOtherBlockingTile"))
        {
            return true;
        }

        return false;
    }

    private void MovePlayer()
    {
        // Mueve al jugador hacia la posici�n objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, moveSpeed * Time.deltaTime);

        // Cuando llega al objetivo, detiene el movimiento
        if (Vector3.Distance(transform.position, targetWorldPosition) < 0.01f)
        {
            transform.position = targetWorldPosition;
            isMoving = false;
        }
    }

    private void AlignToGrid()
    {
        // Alinea al jugador al centro de la celda
        targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
        transform.position = targetWorldPosition;
    }

    private void CheckPlayerOnWater()
    {
        // Obtiene la posici�n del jugador en coordenadas de celda del Tilemap
        Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);

        // Obtiene el tile en esa posici�n
        TileBase tile = tilemap.GetTile(playerCellPosition);

        // Verifica si el tile es "Water_0"
        if (tile != null && tile.name == "Water_0")
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        Manager.instance.lives--;
        Manager.instance.hasPrice = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

private void OnCollisionEnter2D(Collision2D collision)
{
    // Check if the player collided with a snake
    if (collision.gameObject.CompareTag("Snake"))
    {
        Die();
    }
}
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpawnZone") && Manager.instance.hasPrice) Win(); 
    }

    private void Win()
    {
        Debug.Log("You won");
        winScreen.SetActive(true);
        Manager.instance.hasPrice = false;
        LevelTransitionController.instance.StartTransition(4, 2);
    }

    private void Lose()
    {
        Debug.Log("You lost");
        gameOverScreen.SetActive(true);
        LevelTransitionController.instance.StartTransition(4, 2);
        Manager.instance.lives = 3;
        gridPosition = tilemap.WorldToCell(transform.position);
        AlignToGrid();
    }
}
