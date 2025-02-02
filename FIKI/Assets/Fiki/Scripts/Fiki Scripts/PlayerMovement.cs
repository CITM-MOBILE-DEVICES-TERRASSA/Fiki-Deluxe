using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Tilemap tilemap;           // Tilemap que contiene todos los tiles
    public float moveSpeed = 10.0f;   // Velocidad del jugador

    private bool hasPowerUp = false;
    private float powerUpTimeRemaining = 0f;

    private Vector3Int gridPosition;  // Posici�n actual del jugador en la cuadr�cula
    private Vector3 targetWorldPosition;
    private bool isMoving = false;
    private Vector2 startMousePosition;
    private Vector2 endMousePosition;
    private Vector2 startPosition;
    private GameObject winScreen;
    private GameObject gameOverScreen;
    private Animator animator;

    public AudioClip soundClip; // Arrastra tu clip de audio aquí en el Inspector
    private AudioSource audioSource;

    public AudioClip winScreenFx;
    public AudioClip gameOverScreenFx;

    public AudioClip lifeLostFx;

    // Referencia al prefab de partículas
    [SerializeField] private GameObject walkParticlesPrefab;
    
    [SerializeField] private GameObject waterParticlesPrefab;
    private bool waterPlayedParticles = false; // Controla si las partículas ya se reprodujeron

    private bool isDead = false;

    [SerializeField] private TimerScript timer;
    void Start()
    {
        startPosition = new Vector2(transform.position.x, transform.position.y);
        gridPosition = tilemap.WorldToCell(transform.position); // Calcula la posici�n inicial en la cuadr�cula
        AlignToGrid(); // Alinea al jugador al centro de la celda
        animator = GetComponent<Animator>();
        winScreen = GameObject.Find("winwin");
        gameOverScreen = GameObject.Find("gameOver");

        if (winScreen) winScreen.SetActive(false);
        if (gameOverScreen) gameOverScreen.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        timer = FindObjectOfType<TimerScript>();
    }

    public void SetGridPosition(Vector3Int newGridPosition)
    {
        gridPosition = newGridPosition;
        AlignToGrid(); // Asegura que el jugador se alinee correctamente
    }

    void Update()
    {
        if (isDead) return;

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
                    direction = Vector3Int.right * (hasPowerUp ? 2 : 1);
                    transform.localScale = new Vector3(3, 3, 3);
                }
                else
                {
                    direction = Vector3Int.left * (hasPowerUp ? 2 : 1);
                    transform.localScale = new Vector3(-3, 3, 3);
                }
            }
            else
            {
                if (swipeDelta.y > 0)
                {
                    direction = Vector3Int.up * (hasPowerUp ? 2 : 1);
                }
                else
                {
                    direction = Vector3Int.down * (hasPowerUp ? 2 : 1);
                }
            }

            // Calcula la nueva posici�n en la cuadr�cula
            Vector3Int intermediatePosition = gridPosition + (direction / 2);
            Vector3Int newGridPosition = gridPosition + direction;

            if (hasPowerUp)
            {
                if (!IsMovementBlocked(intermediatePosition) && !IsMovementBlocked(newGridPosition))
                {
                    gridPosition = newGridPosition;
                    targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                    isMoving = true;
                    animator.Play("Player_Walk");
                }
            }
            else
            {
                if (!IsMovementBlocked(newGridPosition))
                {
                    gridPosition = newGridPosition;
                    targetWorldPosition = tilemap.GetCellCenterWorld(gridPosition);
                    isMoving = true;
                    animator.Play("Player_Walk");
                }
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
            PlayWalkParticles();
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
        // Obtiene la posición del jugador en coordenadas de celda del Tilemap
        Vector3Int playerCellPosition = tilemap.WorldToCell(transform.position);

        // Obtiene el tile en esa posición
        TileBase tile = tilemap.GetTile(playerCellPosition);
        
        // array que detecta todos los tiles que permiten muerte
        string[] waterTiles = new string[] {
        "Outdoor Tile Spritesheet_12",
        "Outdoor Tile Spritesheet_13",
        "Outdoor Tile Spritesheet_14",
        "Outdoor Tile Spritesheet_23",
        "Outdoor Tile Spritesheet_24",
        "Outdoor Tile Spritesheet_25",
        "Outdoor Tile Spritesheet_32",
        "Outdoor Tile Spritesheet_33",
        "Outdoor Tile Spritesheet_34"
    };
        Debug.Log(tile.name);
        // Verifica si el tile es "Water_0" y aún no ha jugado partículas
        if (tile != null && System.Array.Exists(waterTiles, tileName => tile.name == tileName) && !waterPlayedParticles)
        {
            waterPlayedParticles = true; // Marca que las partículas ya se están reproduciendo
            PlayWaterParticles(); // Reproduce partículas y llama a Die después
        }
    }

    public void Die()
    {
        Debug.Log("¡El jugador ha muerto!");
        animator.Play("Player_Die");
        isDead = true;
        Manager.instance.lives--;
        if (Manager.instance.lives > 0)
        {
            AudioManager.instance.PlaySFX(lifeLostFx);
            Manager.instance.hasPrice = false;
            transform.position = startPosition;
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
        else if (Manager.instance.lives <= 0)
        {
            Lose();
        }
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
        Manager.instance.score += 100 + 2 * (int)timer.timeRemaining;
        if (Manager.instance.score > Manager.instance.maxscore)
        {
            Manager.instance.maxscore = Manager.instance.score;
        }
        LevelTransitionController.instance.StartTransition(4, 2);
        isDead = false;
    }

    public void Lose()
    {
        Debug.Log("You lost");
        gameOverScreen.SetActive(true);
        AudioManager.instance.PlaySFX(gameOverScreenFx);
        LevelTransitionController.instance.StartTransition(4, 2);
        Manager.instance.lives = 3;
        gridPosition = tilemap.WorldToCell(transform.position);
        AlignToGrid();
        isDead = false;
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


 
    private IEnumerator WaitAndDie(float duration)
    {
        isDead = true;
        yield return new WaitForSeconds(duration); // Espera el tiempo especificado
        Die(); // Llama a Die después de la espera
    }


    public void PlayWaterParticles()
    {
        // Instancia partículas en la posición actual
        if (waterParticlesPrefab != null)
        {
            GameObject particles = Instantiate(waterParticlesPrefab, transform.position, Quaternion.identity);

            // Obtén la duración del sistema de partículas y destruye el objeto después
            ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                float totalDuration = particleSystem.main.duration + particleSystem.main.startLifetime.constant;

                // Espera la duración del sistema de partículas antes de llamar a Die()
                StartCoroutine(WaitAndDie(totalDuration));

                Destroy(particles, totalDuration); // Destruye las partículas después de que terminen
            }
            else
            {
                Debug.LogWarning("El prefab de partículas no tiene un sistema de partículas.");
                Die(); // Llama a Die inmediatamente si no hay partículas válidas
            }
        }
        else
        {
            Debug.LogWarning("Prefab de partículas no asignado en el inspector.");
            Die(); // Llama a Die inmediatamente si no hay prefab
        }
    }
    public void PlayWalkParticles()
    {
        // Instancia partículas en la posición actual
        if (walkParticlesPrefab != null)
        {
            GameObject particles = Instantiate(walkParticlesPrefab, transform.position, Quaternion.identity);

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

    public void ActivateDoubleJump(float duration)
    {
        hasPowerUp = true;
        powerUpTimeRemaining = duration;
        StartCoroutine(PowerUpCountdown());
    }
    private IEnumerator PowerUpCountdown()
    {
        while (powerUpTimeRemaining > 0)
        {
            powerUpTimeRemaining -= Time.deltaTime;
            yield return null;
        }
        hasPowerUp = false;
    }

}
