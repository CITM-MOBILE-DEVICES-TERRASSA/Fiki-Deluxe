using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed = 10.0f;

    private Vector3Int gridPosition;
    private Vector3 targetWorldPosition;
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;

    void Start()
    {
        gridPosition = tilemap.WorldToCell(transform.position);
        AlignToGrid();
    }

    public void SetGridPosition(Vector3Int newGridPosition)
    {
        gridPosition = newGridPosition;
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
                direction = swipeDelta.x > 0 ? Vector3Int.right : Vector3Int.left;
                transform.rotation = swipeDelta.x > 0 ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
            }
            else
            {
                direction = swipeDelta.y > 0 ? Vector3Int.up : Vector3Int.down;
                transform.rotation = swipeDelta.y > 0 ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 0, 180);
            }

            Vector3Int newGridPosition = gridPosition + direction;

          
            if (IsMovementBlocked(newGridPosition))
            {
                Debug.Log("Movimiento bloqueado: el destino es un tile prohibido.");
                return;
            }

       
            gridPosition = newGridPosition;
            targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
            isMoving = true;
        }
    }

    private bool IsMovementBlocked(Vector3Int newGridPosition)
    {
       
        if (!tilemap.HasTile(newGridPosition))
        {
            return true; 
        }

        TileBase targetTile = tilemap.GetTile(newGridPosition);

     
        return targetTile != null && targetTile.name == "WaterDark_0";
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
        targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
        transform.position = targetWorldPosition;
    }
}
