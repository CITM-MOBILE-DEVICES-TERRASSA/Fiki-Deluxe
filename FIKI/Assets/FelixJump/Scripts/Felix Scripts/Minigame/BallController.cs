using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float bounceForce = 3f;
    private float originalBounceForce;
    private Rigidbody rb;
    private float lastTapTime = 0f;
    private float doubleTapDelay = 0.3f; // Tiempo para doble tap

    public GameObject ballPrefab;
    private bool bonusScoreAdded = false;

    private float checkerTimer = 0f;
    private float checkerTime = 0.5f;

    PlataformaController plataformaController;

    // Variables para sonidos
    public AudioSource jump1;
    public AudioSource jump2;
    public AudioSource jump3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalBounceForce = bounceForce;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Time.time - lastTapTime < doubleTapDelay)
            {
                bounceForce = originalBounceForce * 2;
            }
            lastTapTime = Time.time;
        }

        if (rb.velocity.magnitude < 0.01f)
        {
            checkerTimer += Time.deltaTime;
            if (checkerTimer >= checkerTime)
            {
                rb.AddForce(Vector3.up * originalBounceForce, ForceMode.Impulse);
                PlayRandomJumpSound();
                checkerTimer = 0f;
            }
        }
        else
        {
            checkerTimer = 0f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            PlayRandomJumpSound();
            bounceForce = originalBounceForce; // Restaura la fuerza
        }

        if (collision.gameObject.CompareTag("Trampa"))
        {
            transform.position = new Vector3(0, 0.4f, -0.7f);
            Debug.Log("Lost");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meta") && !bonusScoreAdded)
        {
            if (CylinderController.instance)
            {
                Debug.Log("Nivel Completado");
                CylinderController.instance.endPanel.gameObject.SetActive(true);

                PlataformaController.instance.timerRunning = false;

                string currentScene = SceneManager.GetActiveScene().name;
                Score.Instance.HandleLevelCompletion(currentScene, PlataformaController.instance.countdownTime);

                // Desbloquea el siguiente nivel
                LevelSelector levelSelector = FindObjectOfType<LevelSelector>();
                if (levelSelector != null)
                {
                    int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

                    levelSelector.availableLevels[currentLevelIndex - 3] = true;
                    Debug.Log($"Nivel {currentLevelIndex - 3} desbloqueado.");

                }
                else
                {
                    Debug.LogWarning("No se encontró un LevelSelector.");
                }

                bonusScoreAdded = true;
            }
        }
    }

    // Método para reproducir un sonido aleatorio
    private void PlayRandomJumpSound()
    {
        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                jump1.Play();
                break;
            case 1:
                jump2.Play();
                break;
            case 2:
                jump3.Play();
                break;
        }
    }
}
