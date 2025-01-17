using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionController : MonoBehaviour
{
    public static LevelTransitionController instance;

    public Image fadeImage;

    private bool isTransitioning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void StartTransition(int sceneLevel, float time)
    {
        if (!isTransitioning)
        {
            StartCoroutine(Transition(sceneLevel, time));
        }
    }

    private IEnumerator Transition(int sceneLevel, float time)
    {
        isTransitioning = true;

        // Fundido a negro
        yield return Fade(1, time);

        // Carga la siguiente escena
        SceneManager.LoadScene(sceneLevel);

        // Fundido a transparente
        yield return Fade(0, time);

        isTransitioning = false;
    }

    private IEnumerator Fade(float targetAlpha, float time)
    {
        float startAlpha = fadeImage.color.a;
        float rate = 1.0f / time;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            progress += Time.deltaTime * rate;
            Color color = fadeImage.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, progress);
            fadeImage.color = color;
            yield return null;
        }
    }
}
