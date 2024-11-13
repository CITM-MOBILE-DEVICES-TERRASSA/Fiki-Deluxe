using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 1.0f; // Tamaño del bloque 
    public float moveSpeed = 10.0f;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;
    private SpriteRenderer spriteRenderer;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    void Start()
    {
        targetPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                if (swipeDelta.x > 0)
                {
                    targetPosition += Vector3.right * moveDistance;
                    spriteRenderer.sprite = spriteRight;
                }
                else
                {
                    targetPosition += Vector3.left * moveDistance;
                    spriteRenderer.sprite = spriteLeft;
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    targetPosition += Vector3.up * moveDistance;
                    spriteRenderer.sprite = spriteUp;
                }
                else
                {
                    targetPosition += Vector3.down * moveDistance;
                    spriteRenderer.sprite = spriteDown;
                }
            }
            isMoving = true;
        }
    }
    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }

    public void ApplyPush(Vector2 pushDirection, float pushDistance)
    {
        if (!isMoving)
        {
            // Establecer la posición objetivo en la dirección del empuje
            targetPosition += (Vector3)(pushDirection * pushDistance);

            // Cambiar la sprite del jugador según la dirección del empuje
            if (Mathf.Abs(pushDirection.x) > Mathf.Abs(pushDirection.y))
            {
                spriteRenderer.sprite = pushDirection.x > 0 ? spriteRight : spriteLeft;
            }
            else
            {
                spriteRenderer.sprite = pushDirection.y > 0 ? spriteUp : spriteDown;
            }

            isMoving = true;
        }
    }
}