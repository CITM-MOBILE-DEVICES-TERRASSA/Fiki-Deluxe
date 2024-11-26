using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton para acceder globalmente

    [Header("Audio Sources")]
    public AudioSource musicSource; // Fuente para la música de fondo
    public AudioSource sfxSource;   // Fuente para efectos de sonido

    [Header("Music Clips")]
    public AudioClip backgroundMusic; // Clip de música de fondo (opcional)

    private void Awake()
    {
        // Configura el Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruye este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
        }
    }

    private void Start()
    {
        // Reproduce música de fondo si está configurada
        if (backgroundMusic != null)
        {
            PlayMusic(backgroundMusic);
        }
    }

    /// <summary>
    /// Reproduce música de fondo
    /// </summary>
    /// <param name="clip">Clip de audio a reproducir</param>
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true; // Asegura que la música se repita
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado una fuente de música.");
        }
    }

    /// <summary>
    /// Detiene la música actual
    /// </summary>
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    /// <summary>
    /// Reproduce un efecto de sonido
    /// </summary>
    /// <param name="clip">Clip de sonido a reproducir</param>
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null)
        {
            sfxSource.PlayOneShot(clip); // Reproduce sin interrumpir otros sonidos
        }
        else
        {
            Debug.LogWarning("No se ha asignado una fuente de efectos de sonido.");
        }
    }
}
