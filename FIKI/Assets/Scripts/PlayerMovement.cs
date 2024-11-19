using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed = 10.0f;

    private Vector3Int gridPosition;
    private Vector3 targetWorldPosition;
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
 
    private GameObject winScreen;
    private GameObject gameOverScreen;

    void Start()
    {
        winScreen = GameObject.Find("winwin");
        gameOverScreen = GameObject.Find("gameOver");

        if (winScreen) winScreen.SetActive(false);
        if (gameOverScreen) gameOverScreen.SetActive(false);
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

        CheckPlayerOnWater();

        if (Manager.instance.lives <= 0)
        {
            Lose();
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
        if (!tilemap.cellBounds.Contains(newGridPosition))
        {
            return true;
        }

        if (!tilemap.HasTile(newGridPosition))
        {
            return true;
        }

        TileBase targetTile = tilemap.GetTile(newGridPosition);

        if (targetTile != null && (targetTile.name == "WaterDark_0" || targetTile.name == "SomeOtherBlockingTile"))
        {
            return true;
        }

        return false;
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

    private void CheckPlayerOnWater()
    {
        Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(playerCellPosition);
        if (tile != null && tile.name == "Water_0")
        {
            DieOnWater();
        }
    }

    private void DieOnWater()
    {
        Debug.Log("Player fell on water and died");
        Manager.instance.lives--;
        Manager.instance.hasPrice = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
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
