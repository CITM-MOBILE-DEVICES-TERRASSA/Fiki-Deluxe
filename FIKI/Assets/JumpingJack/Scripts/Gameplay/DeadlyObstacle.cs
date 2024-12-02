using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyObstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameplayManager.Instance.gameOverScreen.SetActive(true);
        }
    }
}
