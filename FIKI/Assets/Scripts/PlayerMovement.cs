using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap del mapa
    public float moveSpeed = 10.0f;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private SpriteRenderer spriteRenderer;
    private Vector3Int gridPosition; // Posición del jugador en coordenadas de la grilla
    private Vector3 targetWorldPosition; // Posición en el mundo hacia la que se mueve
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializa la posición en la grilla basándose en la posición actual
        gridPosition = tilemap.WorldToCell(transform.position);
        AlignToGrid();
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
    }

    private void DetectSwipeDirection()
    {
        Vector2 swipeDelta = endMousePosition - startMousePosition;

        if (swipeDelta.magnitude > 50)
        {
            swipeDelta.Normalize();

            Vector3Int direction = Vector3Int.zero;

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    direction = Vector3Int.right;
                    spriteRenderer.sprite = spriteRight;
                }
                else
                {
                    direction = Vector3Int.left;
                    spriteRenderer.sprite = spriteLeft;
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    direction = Vector3Int.up;
                    spriteRenderer.sprite = spriteUp;
                }
                else
                {
                    direction = Vector3Int.down;
                    spriteRenderer.sprite = spriteDown;
                }
            }

            // Calcula la nueva posición de la grilla
            Vector3Int newGridPosition = gridPosition + direction;

            // Verifica si la nueva posición es válida (opcional: puedes agregar más lógica aquí)
            if (tilemap.HasTile(newGridPosition))
            {
                gridPosition = newGridPosition;
                targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                isMoving = true;
            }
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWorldPosition) < 0.01f)
        {
            transform.position = targetWorldPosition;
            isMoving = false;
        }
    }

    private void AlignToGrid()
    {
        // Alinea la posición del jugador al centro del tile en el que se encuentra
        targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
        transform.position = targetWorldPosition;
    }
}
