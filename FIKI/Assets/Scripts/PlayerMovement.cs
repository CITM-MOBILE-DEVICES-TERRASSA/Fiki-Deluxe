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
    private Animator animator;

    public AudioClip soundClip; // Arrastra tu clip de audio aquí en el Inspector
    private AudioSource audioSource;

    public AudioClip winScreenFx;
    public AudioClip gameOverScreenFx;

    public AudioClip lifeLostFx;

    // Referencia al prefab de partículas
    [SerializeField] private GameObject particlesPrefab;

    void Start()
    {
        gridPosition = tilemap.WorldToCell(transform.position); // Calcula la posici�n inicial en la cuadr�cula
        AlignToGrid(); // Alinea al jugador al centro de la celda
        animator = GetComponent<Animator>();
        winScreen = GameObject.Find("winwin");
        gameOverScreen = GameObject.Find("gameOver");

        if (winScreen) winScreen.SetActive(false);
        if (gameOverScreen) gameOverScreen.SetActive(false);

        audioSource = GetComponent<AudioSource>();
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
                    transform.localScale = new Vector3(3, 3, 3);
                }
                else
                {
                    direction = Vector3Int.left;
                    transform.localScale = new Vector3(-3, 3, 3);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    direction = Vector3Int.up;
                }
                else
                {
                    direction = Vector3Int.down;
                }
            }

            // Calcula la nueva posici�n en la cuadr�cula
            Vector3Int newGridPosition = gridPosition + direction;

            if (!IsMovementBlocked(newGridPosition))
            {
                gridPosition = newGridPosition;
                targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                isMoving = true;
                animator.Play("Player_Walk");
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
            PlaySound();
            PlayParticles();
            animator.SetBool("Walking", false);
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
        if (tile != null && tile.name == "tileset_version1.1_93")
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        animator.Play("Player_Die");
        Manager.instance.lives--;
        if (Manager.instance.lives > 0)
        {
            AudioManager.instance.PlaySFX(lifeLostFx);
        }
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
        AudioManager.instance.PlaySFX(winScreenFx);
        Manager.instance.hasPrice = false;
        LevelTransitionController.instance.StartTransition(4, 2);
    }

    private void Lose()
    {
        Debug.Log("You lost");
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySFX(gameOverScreenFx);
        LevelTransitionController.instance.StartTransition(4, 2);
        Manager.instance.lives = 3;
        gridPosition = tilemap.WorldToCell(transform.position);
        AlignToGrid();
    }


    public void PlaySound()
    {
        if (soundClip != null)
        {
            // Asigna el clip al AudioSource y reprodúcelo
            audioSource.clip = soundClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioClip.");
        }
    }


    public void PlayParticles()
    {
        // Instancia partículas en la posición actual
        if (particlesPrefab != null)
        {
            GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity);

            // Obtén la duración del sistema de partículas y destruye el objeto después
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                Destroy(particles, particleSystem.main.duration + particleSystem.main.startLifetime.constant);
            }
            else
            {
                Debug.LogWarning("El prefab de partículas no tiene un sistema de partículas.");
            }
        }
        else
        {
            Debug.LogWarning("Prefab de partículas no asignado en el inspector.");
        }

    }



}
