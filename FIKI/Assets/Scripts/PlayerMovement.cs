using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;           // Tilemap que contiene todos los tiles
    public float moveSpeed = 10.0f;   // Velocidad del jugador

    private Vector3Int gridPosition;  // Posici�n actual del jugador en la cuadr�cula
    private Vector3 targetWorldPosition;
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;

    void Start()
    {
        gridPosition = tilemap.WorldToCell(transform.position); // Calcula la posici�n inicial en la cuadr�cula
        AlignToGrid(); // Alinea al jugador al centro de la celda
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

        CheckPlayerOnWater(); // Comprueba si el jugador est� en un tile de agua
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
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    direction = Vector3Int.left;
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    direction = Vector3Int.up;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    direction = Vector3Int.down;
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }

            // Calcula la nueva posici�n en la cuadr�cula
            Vector3Int newGridPosition = gridPosition + direction;

            if (!IsMovementBlocked(newGridPosition))
            {
                Debug.Log("Movimiento bloqueado: el destino es un tile prohibido.");
                gridPosition = newGridPosition;
                targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                isMoving = true;
                return;
            }
        }
    }

    private bool IsMovementBlocked(Vector3Int newGridPosition)
    {
       
        if (!tilemap.HasTile(newGridPosition))
        {
            return true; 
        }

        TileBase targetTile = tilemap.GetTile(newGridPosition);

        Debug.Log(targetTile.name);
     
        return targetTile != null && targetTile.name == "WaterDark_0";
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
        Debug.Log("�El jugador ha ca�do en el agua y ha muerto!");
        // Reinicia la escena actual
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}