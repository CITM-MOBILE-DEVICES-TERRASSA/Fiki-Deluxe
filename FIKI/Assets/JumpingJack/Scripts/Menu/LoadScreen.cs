using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private AnimationClip loadingBarAnim;

    private void Start()
    {
        StartCoroutine(LoadLobbyScene());
    }

    private IEnumerator LoadLobbyScene()
    {
        yield return new WaitForSeconds(loadingBarAnim.length);

        SceneManager.LoadScene("Lobby");
    }
}
