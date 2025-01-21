using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorsMagic : MonoBehaviour
{
    public static ColorsMagic Instance { get; private set; }
    public Image fadeImage; // Imagen usada para el efecto de fade (debe cubrir toda la pantalla).
    public GameObject fadePrefab; // Prefab de la imagen de fade, usado si no se encuentra uno en escena.
    public float fadeDuration = 1f; // Duración del fade.
    public AudioSource clicksound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste todo el GameObject, incluida la imagen.
        }
        else
        {
            Destroy(gameObject); // Evita duplicados.
            return;
        }

        // Inicializa el fadeImage si no está asignado.
        EnsureFadeImage();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Registra el evento para reasignar referencias en cada escena.
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desregistra el evento al desactivar el objeto.
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureFadeImage(); // Reasigna fadeImage si es necesario al cargar una nueva escena.
    }

    private void EnsureFadeImage()
    {
        if (fadeImage == null)
        {
            // Busca un Image en el hijo si existe.
            fadeImage = GetComponentInChildren<Image>();

            // Si no se encuentra, instancia el prefab y asigna la referencia.
            if (fadeImage == null && fadePrefab != null)
            {
                GameObject fadeInstance = Instantiate(fadePrefab, transform);
                fadeImage = fadeInstance.GetComponent<Image>();
            }

            // Si sigue siendo null, lanza una advertencia.
            if (fadeImage == null)
            {
                Debug.LogWarning("No se pudo encontrar o crear un objeto fadeImage.");
            }
        }
    }

    public void ChangeToScene(string sceneName)
    {
        clicksound.Play();
        if (fadeImage != null)
        {
            StartCoroutine(FadeOutAndChangeScene(sceneName));
        }
        else
        {
            Debug.LogWarning("fadeImage es null. Cambiando escena directamente.");
            SceneManager.LoadScene(sceneName);
        }
    }

    private IEnumerator FadeIn()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            float alpha = 1f;

            while (alpha > 0f)
            {
                alpha -= Time.deltaTime / fadeDuration;
                SetFadeAlpha(alpha);
                yield return null;
            }

            SetFadeAlpha(0f);
            fadeImage.gameObject.SetActive(false);
        }
    }

    private IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            float alpha = 0f;

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / fadeDuration;
                SetFadeAlpha(alpha);
                yield return null;
            }

            SetFadeAlpha(1f);
        }

        // Cambia de escena y luego realiza el FadeIn.
        SceneManager.LoadScene(sceneName);
        yield return null; // Espera un frame para que la escena cargue.
        StartCoroutine(FadeIn());
    }

    private void SetFadeAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = Mathf.Clamp01(alpha);
            fadeImage.color = color;
        }
    }
}
