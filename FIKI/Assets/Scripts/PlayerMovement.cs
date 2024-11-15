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
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 90);
                    direction = Vector3Int.left;
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    direction = Vector3Int.up;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, 180);
                    direction = Vector3Int.down;
                }
            }

            Vector3Int newGridPosition = gridPosition + direction;

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
        targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
        transform.position = targetWorldPosition;
    }
}
